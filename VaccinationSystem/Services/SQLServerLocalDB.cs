using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;
using VaccinationSystem.Data;
using Microsoft.EntityFrameworkCore;
using VaccinationSystem.DTOs;


namespace VaccinationSystem.Services
{
    public class SQLServerLocalDB : IDatabase
    {
        private AppDBContext dbContext;
        public SQLServerLocalDB(AppDBContext context)
        {
            dbContext = context;
        }
        public async Task<PatientResponse> GetPatient(Guid id)
        {
            var patient = await dbContext.Patients.FindAsync(id);
            if (patient == null)
                return null;
            else
                return new PatientResponse()
                {
                    id = patient.id,
                    PESEL = patient.pesel,
                    firstName = patient.firstName,
                    lastName = patient.lastName,
                    dateOfBirth = patient.dateOfBirth.ToString("dd-MM-yyyy"),
                    mail = patient.mail,
                    phoneNumber = patient.phoneNumber,
                    active = patient.active,
                };
        }
        public async Task<List<PatientResponse>> GetPatients()
        {
            var patients = await dbContext.Patients.ToListAsync();
            var patientsResponse = new List<PatientResponse>();
            foreach (var patient in patients)
            {
                var pR = new PatientResponse()
                {
                    id = patient.id,
                    PESEL = patient.pesel,
                    firstName = patient.firstName,
                    lastName = patient.lastName,
                    dateOfBirth = patient.dateOfBirth.ToString("dd-MM-yyyy"),
                    mail = patient.mail,
                    phoneNumber = patient.phoneNumber,
                    active = patient.active,
                };
                patientsResponse.Add(pR);
            }
            return patientsResponse;
        }
        public async Task<List<DoctorResponse>> GetDoctors()
        {
            var doctors = dbContext.Doctors.Include(d => d.vaccinationCenter).Include(d=>d.patientAccount).ToList();
            var doctorsResponse = new List<DoctorResponse>();
            VaccinationCenter center;
            Patient patientAcc;
            DoctorResponse dR;
            foreach (var doctor in doctors)
            {
                center = await dbContext.VaccinationCenters.FindAsync(doctor.vaccinationCenter.id);
                patientAcc = await dbContext.Patients.FindAsync(doctor.patientAccount.id);
                dR = new DoctorResponse()
                {
                    id = doctor.doctorId,
                    PESEL = patientAcc.pesel,
                    firstName = patientAcc.firstName,
                    lastName = patientAcc.lastName,
                    dateOfBirth = patientAcc.dateOfBirth.ToString("dd-MM-yyyy"),
                    mail = patientAcc.mail,
                    phoneNumber = patientAcc.phoneNumber,
                    active = doctor.active,
                    vaccinationCenterId = doctor.vaccinationCenter.id,
                    city = center.city,
                    name = center.name,
                    street = center.address,
                };
                doctorsResponse.Add(dR);
            }
            return doctorsResponse;
        }

        public void AddPatient(RegisteringPatient patient)
        {
            Patient p = new Patient
            {
                pesel = patient.PESEL,
                firstName = patient.firstName,
                lastName = patient.lastName,
                mail = patient.mail,
                password = patient.password,
                phoneNumber = patient.phoneNumber,
                active = true,
                certificates = { },
                vaccinationHistory = { },
                futureVaccinations = { },
                dateOfBirth = DateTime.ParseExact(patient.dateOfBirth, "dd-MM-yyyy", null)
            };


            dbContext.Patients.Add(p);
            dbContext.SaveChanges();

        }

        public LoginResponse AreCredentialsValid(Login login)
        {
            var doctor = dbContext.Doctors.Include(d=>d.patientAccount).Where(d => d.patientAccount.mail.CompareTo(login.mail) == 0).FirstOrDefault();
            if (doctor != null && doctor.patientAccount.password.CompareTo(login.password) == 0)
            {
                return new LoginResponse()
                {
                    userId = doctor.doctorId,
                    userType = "doctor"
                };
            }
            else
            {
                var patient = dbContext.Patients.Where(p => p.mail.CompareTo(login.mail) == 0).FirstOrDefault();
                if (patient != null && patient.password.CompareTo(login.password) == 0)
                {
                    return new LoginResponse()
                    {
                        userId = patient.id,
                        userType = "patient"
                    };
                }
                else
                {
                    var admin = dbContext.Admins.Where(a => a.mail.CompareTo(login.mail) == 0).FirstOrDefault();
                    if (admin != null && admin.password.CompareTo(login.password) == 0)
                        return new LoginResponse()
                        {
                            userId = admin.id,
                            userType = "admin"
                        };
                }
            }

            return new LoginResponse()
            {
                userId = Guid.Empty,
                userType = ""
            };
        }

        public bool IsUserInDatabase(string email)
        {
            int emailOccurance = dbContext.Patients.Where(p => p.mail.CompareTo(email) == 0).Count();

            if (emailOccurance > 0)
                return true;

            return false;

        }

        public async Task<List<VaccinationCenterResponse>> GetVaccinationCenters()
        {
            var centers = await dbContext.VaccinationCenters.ToListAsync();
            var centersToReturn = new List<VaccinationCenterResponse>();

            foreach (var center in centers)
            {
                var vaccines = await GetVaccinesFromVaccinationCenter(center.id);
                var hours = await GetOpeningHoursFromVaccinationCenter(center.id);

                var vC = new VaccinationCenterResponse()
                {
                    id = center.id,
                    name = center.name,
                    city = center.city,
                    street = center.address,
                    vaccines = vaccines,
                    active = center.active,
                    openingHoursDays = hours.ToArray(),
                };

                centersToReturn.Add(vC);

            }

            return centersToReturn;
        }

        public async Task<bool> EditVaccinationCenter(EditedVaccinationCenter center)
        {

            var dbCenter = await dbContext.VaccinationCenters.SingleOrDefaultAsync(c => c.id == center.id);

            if (dbCenter != null)
            {
                dbCenter.name = center.name;
                dbCenter.city = center.city;
                dbCenter.address = center.street;
                dbCenter.active = center.active;

                var vaccines = dbContext.VaccinesInCenters.Where(w => w.vaccinationCenter.id == dbCenter.id);
                dbContext.VaccinesInCenters.RemoveRange(vaccines);

                foreach (var vId in center.vaccineIds)
                {
                    Vaccine vaccine = await GetVaccine(vId);
                    dbContext.VaccinesInCenters.Add(new VaccinesInCenters
                    {
                        vaccine = vaccine,
                        vaccinationCenter = dbCenter,
                    });
                }

                var hours = dbContext.OpeningHours.Where(h => h.vaccinationCenter.id == dbCenter.id);
                foreach (var h in hours.ToList())
                {
                    dbContext.OpeningHours.Remove(h);
                }

                int dayOfWeek = 0;
                foreach (var h in center.openingHoursDays.ToList())
                {
                    dbContext.OpeningHours.Add(new OpeningHours()
                    {
                        from = TimeSpan.Parse(h.from),
                        to = TimeSpan.Parse(h.to),
                        vaccinationCenter = dbCenter,
                        day = (WeekDay)dayOfWeek
                    });

                    dayOfWeek++;
                }

                await dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteVaccinationCenter(Guid vaccinationCenterId)
        {
            Console.WriteLine(vaccinationCenterId);
            var dbCenter = await dbContext.VaccinationCenters.SingleAsync(c => c.id == vaccinationCenterId);

            if (dbCenter != null)
            {
                var vaccines = dbContext.VaccinesInCenters.Where(w => w.vaccinationCenter.id == dbCenter.id);
                dbContext.VaccinesInCenters.RemoveRange(vaccines);

                var hours = dbContext.OpeningHours.Where(h => h.vaccinationCenter.id == dbCenter.id);
                dbContext.OpeningHours.RemoveRange(hours);

                await dbContext.Doctors.Where(d => d.vaccinationCenter.id == dbCenter.id)
                    .ForEachAsync(d => d.vaccinationCenter = null);

                dbContext.VaccinationCenters.Remove(dbCenter);

                await dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<Vaccine> GetVaccine(Guid vaccineId)
        {
            var vaccine = await dbContext.Vaccines.SingleAsync(v => v.id == vaccineId);

            return vaccine;
        }

        public async Task<List<Vaccine>> GetVaccinesFromVaccinationCenter(Guid vaccinationCenterId)
        {
            var vaccines = dbContext.VaccinesInCenters.Include(v => v.vaccinationCenter).Include(v => v.vaccine).Where(vic => vic.vaccinationCenter.id == vaccinationCenterId);

            return await vaccines.Select(v => v.vaccine).ToListAsync();
        }

        public async Task<List<OpeningHoursDays>> GetOpeningHoursFromVaccinationCenter(Guid vaccinationCenterId)
        {
            var hours = await dbContext.OpeningHours.Where(h => h.vaccinationCenter.id == vaccinationCenterId)
                .Select(h => new OpeningHoursDays()
                {
                    from = $"{h.from.Hours}:{h.from.Minutes}",
                    to = $"{h.to.Hours}:{h.to.Minutes}"
                }).ToListAsync();

            return hours;
        }

        public async Task<List<Doctor>> GetDoctorsFromVaccinationCenter(Guid vaccinationCenterId)
        {
            var doctors = await dbContext.Doctors.Where(d => d.vaccinationCenter.id == vaccinationCenterId).ToListAsync();

            return doctors;
        }

        public async Task AddVaccinationCenter(AddVaccinationCenterRequest center)
        {
            var vC = new VaccinationCenter
            {
                name = center.name,
                city = center.city,
                address = center.street,
                active = true
            };

            var hours = new OpeningHours[7];

            int dayOfWeek = 0;
            foreach (var h in center.openingHoursDays)
            {
                dbContext.OpeningHours.Add(new OpeningHours()
                {
                    from = TimeSpan.Parse(h.from),
                    to = TimeSpan.Parse(h.to),
                    vaccinationCenter = vC,
                    day = (WeekDay)dayOfWeek
                });

                dayOfWeek++;

            }


            foreach (var vId in center.vaccineIds)
            {
                Vaccine vaccine = await GetVaccine(vId);
                dbContext.VaccinesInCenters.Add(new VaccinesInCenters
                {
                    vaccine = vaccine,
                    vaccinationCenter = vC,
                });
            }

            dbContext.VaccinationCenters.Add(vC);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> EditPatient(EditedPatient patient)
        {
            var dbPatient = await dbContext.Patients.SingleAsync(pat => pat.id == patient.id);
            if (dbPatient != null)
            {
                dbPatient.dateOfBirth = DateTime.ParseExact(patient.dateOfBirth, "dd-MM-yyyy", null);
                dbPatient.firstName = patient.firstName;
                dbPatient.lastName = patient.lastName;
                dbPatient.mail = patient.mail;
                dbPatient.pesel = patient.PESEL;
                dbPatient.phoneNumber = patient.phoneNumber;
                dbPatient.active = patient.active;

                var doctor = await dbContext.Doctors.Include(d => d.patientAccount).SingleOrDefaultAsync(d => d.patientAccount.id == dbPatient.id);
                if (doctor != null)
                {
                    if (!patient.active)
                        doctor.active = patient.active;
                }

                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeletePatient(Guid patientId)
        {
            var dbPatient = await dbContext.Patients.SingleAsync(patient => patient.id == patientId);
            if (dbPatient != null)
            {
                var counts = dbContext.VaccinationCounts.Where(c => c.patient.id == patientId);
                dbContext.VaccinationCounts.RemoveRange(counts);
                var appointments = dbContext.Appointments.Where(c => c.patient.id == patientId);
                dbContext.Appointments.RemoveRange(appointments);
                var certificates = dbContext.Certificates.Where(c => c.patientId == patientId);
                dbContext.Certificates.RemoveRange(certificates);
                dbContext.Patients.Remove(dbPatient);
                await dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> AddDoctor(RegisteringDoctor doctor)
        {
            var patient = await dbContext.Patients.SingleAsync(patient => patient.pesel == doctor.PESEL);
            var center = await dbContext.VaccinationCenters.SingleAsync(center => center.id == doctor.vaccinationCenterId);
            Doctor doc = new Doctor
            {
                vaccinationCenter = center,
                active = true,
                vaccinationsArchive = { },
                futureVaccinations = { },
                patientAccount = patient
            };

            dbContext.Doctors.Add(doc);
            var saved = dbContext.SaveChanges();
            if (saved > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> EditDoctor(EditedDoctor doctor)
        {
            var dbDoctor = await dbContext.Doctors.Include(d => d.patientAccount).SingleAsync(doc => doc.doctorId == doctor.id);
            var center = await dbContext.VaccinationCenters.SingleAsync(center => center.id == doctor.vaccinationCenterId);
            if (dbDoctor != null)
            {
                dbDoctor.vaccinationCenter = center;

                var patient = await dbContext.Patients.SingleOrDefaultAsync(p => p.id == dbDoctor.patientAccount.id);
                if (patient != null)
                {
                    patient.pesel = doctor.PESEL;
                    patient.firstName = doctor.firstName;
                    patient.lastName = doctor.lastName;
                    patient.dateOfBirth = DateTime.ParseExact(doctor.dateOfBirth, "dd-MM-yyyy", null);
                    patient.mail = doctor.mail;
                    patient.phoneNumber = doctor.phoneNumber;
                }

                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteDoctor(Guid doctorId)
        {
            var dbDoctor = await dbContext.Doctors.SingleAsync(doc => doc.doctorId == doctorId);
            if (dbDoctor != null)
            {
                var appointments = dbContext.Appointments.Where(d => d.timeSlot.doctor.doctorId == doctorId);
                dbContext.Appointments.RemoveRange(appointments);
                var times = dbContext.TimeSlots.Where(t => t.doctor.doctorId == doctorId);
                dbContext.TimeSlots.RemoveRange(times);
                //var center = dbContext.VaccinationCenters.Where(c => c.id == dbDoctor.vaccinationCenter.id);

                dbContext.Doctors.Remove(dbDoctor);
                await dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public Task<List<TimeSlotsResponse>> GetTimeSlots(Guid doctorId)
        {
            var slots = dbContext.TimeSlots.Where(s => s.doctor.doctorId == doctorId)
                .Select(s => new TimeSlotsResponse
                {
                    id = s.id,
                    from = s.from.ToString("dd-MM-yyyy HH:mm"),
                    to = s.to.ToString("dd-MM-yyyy HH:mm"),
                    isFree = s.isFree
                }).ToListAsync();

            return slots;
        }

        public async Task CreateTimeSlots(Guid doctorId, CreateNewVisitRequest visitRequest)
        {
            DateTime date = DateTime.ParseExact(visitRequest.windowBegin, "dd-MM-yyyy HH:mm", null);
            DateTime dateTo = DateTime.ParseExact(visitRequest.windowEnd, "dd-MM-yyyy HH:mm", null);


            Doctor doctor = await dbContext.Doctors.SingleOrDefaultAsync(d => d.doctorId == doctorId);
            if (doctor == null)
                throw new ArgumentException();

            while (date.AddMinutes(visitRequest.timeSlotDurationInMinutes) <= dateTo)
            {
                await dbContext.TimeSlots.AddAsync(new TimeSlot
                {
                    from = date,
                    to = date.AddMinutes(visitRequest.timeSlotDurationInMinutes),
                    doctor = doctor,
                    active = true,
                    isFree = true
                });

                date = date.AddMinutes(visitRequest.timeSlotDurationInMinutes);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> EditTimeSlot(Guid doctorId, Guid slotId, EditedTimeSlot timeSlot)
        {
            var slot = await dbContext.TimeSlots.Include(s => s.doctor).SingleOrDefaultAsync(s => s.id == slotId);
            if (slot == null)
                return false;

            if (slot.doctor.doctorId != doctorId)
                throw new ArgumentException();

            slot.from = DateTime.ParseExact(timeSlot.timeFrom, "dd-MM-yyyy HH:mm", null);
            slot.to = DateTime.ParseExact(timeSlot.timeTo, "dd-MM-yyyy HH:mm", null);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteTimeSlots(Guid doctorId, List<DeleteTimeSlot> timeSlotsIds)
        {
            foreach (var slotId in timeSlotsIds)
            {
                var slot = await dbContext.TimeSlots.Include(s => s.doctor).SingleOrDefaultAsync(s => s.id == slotId.id);
                if (slot == null)
                    return false;

                if (slot.doctor.doctorId != doctorId)
                {
                    throw new ArgumentException();
                }

                dbContext.TimeSlots.Remove(slot);
            }

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<TimeSlot> GetTimeSlot(Guid timeSlotId)
        {
            var timeSlot = await dbContext.TimeSlots.SingleOrDefaultAsync(t => t.id == timeSlotId);

            return timeSlot;
        }

        public async Task<bool> MakeAppointment(Guid patientId, Guid timeSlotId, Guid vaccineID)
        {
            var timeSlot = await GetTimeSlot(timeSlotId);
            var vaccine = await GetVaccine(vaccineID);
            var patient = await dbContext.Patients.SingleOrDefaultAsync(p => p.id == patientId);

            if (timeSlot == null || vaccine == null || patient == null)
                return false;

            await dbContext.Database.BeginTransactionAsync();
            if (!timeSlot.isFree)
            {
                await dbContext.Database.RollbackTransactionAsync();
                throw new ArgumentException();
            }

            timeSlot.isFree = false;
            var vaccineCount = await dbContext.VaccinationCounts.SingleOrDefaultAsync(c => c.patient.id == patientId && c.virus == vaccine.virus);
            var appointment = new Appointment()
            {
                patient = patient,
                timeSlot = timeSlot,
                vaccine = vaccine,
                state = AppointmentState.Planned,
                whichDose = vaccineCount == null ? 1 : (vaccineCount.count+1),
                certifyState = vaccine.numberOfDoses == (vaccineCount.count+1) ? CertificateState.LastNotCertified : CertificateState.NotLast
            };

            dbContext.Appointments.Add(appointment);


            await dbContext.SaveChangesAsync();
            await dbContext.Database.CommitTransactionAsync();

            return true;
        }

        public async Task<List<FilterTimeSlotResponse>> GetTimeSlotsWithFiltration(TimeSlotsFilter filter)
        {
            var timeSlots = new List<FilterTimeSlotResponse>();
            foreach (var tS in dbContext.TimeSlots.Include(tS => tS.doctor).Include(ts => ts.doctor.vaccinationCenter).Include(ts=>ts.doctor.patientAccount).ToList())
            {
                if (tS.from < DateTime.ParseExact(filter.dateFrom, "dd-MM-yyyy", null) || tS.to > DateTime.ParseExact(filter.dateTo, "dd-MM-yyyy", null) || !tS.isFree || !tS.active)
                    continue;

                var doctor = tS.doctor;
                if (doctor == null)
                    continue;

                var vC = doctor.vaccinationCenter;

                if (vC.city != filter.city)
                    continue;

                //return null;
                var vaccs = await GetVaccinesFromVaccinationCenter(vC.id);

                //prev
                var vaccies = vaccs.Where(v => (Virus)Enum.Parse(typeof(Virus), filter.virus) == v.virus)
                    .Select(v =>
                new SimplifiedVaccine()
                {
                    vaccineId = v.id,
                    maxDaysBetweenDoses = v.maxDaysBetweenDoses,
                    company = v.company,
                    maxPatientAge = v.maxPatientAge,
                    minPatientAge = v.minPatientAge,
                    minDaysBetweenDoses = v.minDaysBetweenDoses,
                    name = v.name,
                    numberOfDoses = v.numberOfDoses,
                    virus = v.virus.ToString()
                }).ToList();

                if (vaccies == null || vaccies.Count() == 0)
                    continue;

                timeSlots.Add(new FilterTimeSlotResponse()
                {
                    timeSlotId = tS.id,
                    @from = tS.from.ToString("dd-MM-yyyy HH:mm"),
                    to = tS.to.ToString("dd-MM-yyyy HH:mm"),
                    vaccinationCenterName = vC.name,
                    vaccinationCenterCity = vC.city,
                    vaccinationCenterStreet = vC.address,
                    openingHours = GetOpeningHoursFromVaccinationCenter(vC.id).Result,
                    availableVaccines = vaccies,
                    doctorFirstName = doctor.patientAccount.firstName,
                    doctorLastName = doctor.patientAccount.lastName,
                });
            }

            return timeSlots;
        }
        public Task<List<CertificatesResponse>> GetCertificates(Guid patientId)
        {
            var certs = dbContext.Certificates.Where(c => c.patientId == patientId).Select(c => new CertificatesResponse
            {
                url = c.url,
                vaccineCompany = dbContext.Vaccines.Where(v => v.id == c.vaccineId).Select(v => v.company).First(),
                vaccineName = dbContext.Vaccines.Where(v => v.id == c.vaccineId).Select(v => v.name).First(),
                virusType = dbContext.Vaccines.Where(v => v.id == c.vaccineId).Select(v => v.virus).First().ToString()
            }).ToListAsync();

            return certs;
        }
        public async Task<List<IncomingAppointmentResponse>> GetIncomingAppointments(Guid patientId)
        {
            var apps = dbContext.Appointments.Include(a => a.patient).Where(a => a.patient.id == patientId)
                .Where(a => a.state == AppointmentState.Planned).Include(a => a.vaccine).Include(a => a.timeSlot)
                .Include(a => a.timeSlot.doctor).Include(a => a.timeSlot.doctor.vaccinationCenter). Include(a=>a.timeSlot.doctor.patientAccount).ToList();
            var incApps = new List<IncomingAppointmentResponse>();
            IncomingAppointmentResponse incAppointment;
            foreach (var app in apps.ToList())
            {
                incAppointment = new IncomingAppointmentResponse()
                {
                    vaccineName = app.vaccine.name,
                    vaccineCompany = app.vaccine.company,
                    vaccineVirus = app.vaccine.virus.ToString(),
                    whichVaccineDose = app.whichDose,
                    appointmentId = app.id,
                    windowBegin = app.timeSlot.from.ToString("dd-MM-yyyy HH:mm"),
                    windowEnd = app.timeSlot.to.ToString("dd-MM-yyyy HH:mm"),
                    vaccinationCenterName = app.timeSlot.doctor.vaccinationCenter.name,
                    vaccinationCenterCity = app.timeSlot.doctor.vaccinationCenter.city,
                    vaccinationCenterStreet = app.timeSlot.doctor.vaccinationCenter.address,
                    doctorFirstName = app.timeSlot.doctor.patientAccount.firstName,
                    doctorLastName = app.timeSlot.doctor.patientAccount.lastName,
                };
                incApps.Add(incAppointment);
            }
            return incApps;
        }
        public async Task<List<FormerAppointmentResponse>> GetFormerAppointments(Guid patientId)
        {
            var apps = dbContext.Appointments.Include(a => a.patient).Where(a => a.patient.id == patientId)
               .Where(a => a.state == AppointmentState.Finished || a.state == AppointmentState.Cancelled)
               .Include(a => a.vaccine).Include(a => a.timeSlot)
               .Include(a => a.timeSlot.doctor).Include(a => a.timeSlot.doctor.vaccinationCenter).Include(a=>a.timeSlot.doctor.patientAccount).ToList();
            var formerApps = new List<FormerAppointmentResponse>();
            FormerAppointmentResponse formAppointment;
            foreach (var app in apps.ToList())
            {
                formAppointment = new FormerAppointmentResponse()
                {
                    vaccineName = app.vaccine.name,
                    vaccineCompany = app.vaccine.company,
                    vaccineVirus = app.vaccine.virus.ToString(),
                    whichVaccineDose = app.whichDose,
                    appointmentId = app.id,
                    windowBegin = app.timeSlot.from.ToString("dd-MM-yyyy HH:mm"),
                    windowEnd = app.timeSlot.to.ToString("dd-MM-yyyy HH:mm"),
                    vaccinationCenterName = app.timeSlot.doctor.vaccinationCenter.name,
                    vaccinationCenterCity = app.timeSlot.doctor.vaccinationCenter.city,
                    vaccinationCenterStreet = app.timeSlot.doctor.vaccinationCenter.address,
                    doctorFirstName = app.timeSlot.doctor.patientAccount.firstName,
                    doctorLastName = app.timeSlot.doctor.patientAccount.lastName,
                    visitState = app.state.ToString(),
                };
                formerApps.Add(formAppointment);
            }
            return formerApps;
        }
        public async Task<bool> CancelIncomingAppointment(Guid patientId, Guid appointmentId)
        {
            var dbAppointment = await dbContext.Appointments.Include(a => a.patient).Include(a => a.timeSlot)
                .Include(a => a.vaccine).SingleAsync(a => a.id == appointmentId && a.patient.id == patientId);
            if (dbAppointment != null)
            {
                dbAppointment.state = AppointmentState.Cancelled;
                if (dbAppointment.timeSlot != null)
                {
                    var timeslot = await dbContext.TimeSlots.SingleAsync(t => t.id == dbAppointment.timeSlot.id);
                    timeslot.isFree = true;
                }
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
       public async Task<bool> CreateCertificate(Guid doctorId, Guid appointmentId, string url)
        {
            var doctor = await dbContext.Doctors.Include(d=>d.vaccinationCenter).SingleOrDefaultAsync(d => d.doctorId == doctorId);
            var appointment = await dbContext.Appointments.Include(a => a.patient).Include(a => a.vaccine).SingleOrDefaultAsync(a => a.id == appointmentId);
            if (appointment.state != AppointmentState.Finished && appointment.certifyState != CertificateState.LastNotCertified)
                return false;

            var certificate = new Certificate()
            {
                patientId = appointment.patient.id,
                vaccineId = appointment.vaccine.id,
                url = url
            };
            appointment.certifyState = CertificateState.Certified;

            await dbContext.Certificates.AddAsync(certificate);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Appointment> GetAppointment(Guid appointmentId)
        {
            var appointment = await dbContext.Appointments.Include(a => a.patient).Include(a => a.vaccine).SingleOrDefaultAsync(a => a.id == appointmentId);

            return appointment;
        }

        public Task<Doctor> GetDoctor(Guid doctorId)
        {
            var doctor = dbContext.Doctors.Include(d=>d.patientAccount).Include(d => d.vaccinationCenter).SingleOrDefaultAsync(d => d.doctorId == doctorId);

            return doctor;
        }
        public async Task<PatientInfoResponse> GetPatientInfo(Guid patientId)
        {
            var patient = await dbContext.Patients.SingleOrDefaultAsync(p => p.id == patientId);
            PatientInfoResponse info = null;
            if (patient != null)
            {
                info = new PatientInfoResponse()
                {
                    firstName = patient.firstName,
                    lastName = patient.lastName,
                    PESEL = patient.pesel,
                    dateOfBirth = patient.dateOfBirth.ToString("dd-MM-yyyy"),
                    mail = patient.mail,
                    phoneNumber = patient.phoneNumber,
                };
            }
            return info;
        }
        public async Task<DoctorInfoResponse> GetDoctorInfo(Guid doctorId)
        {
            var doctor = await dbContext.Doctors.Include(d => d.patientAccount)
                .Include(d => d.vaccinationCenter).SingleOrDefaultAsync(d => d.doctorId == doctorId);
            DoctorInfoResponse info = null;
            if (doctor != null)
            {
                info = new DoctorInfoResponse()
                {
                    vaccinationCenterId = doctor.vaccinationCenter.id,
                    vaccinationCenterName = doctor.vaccinationCenter.name,
                    vaccinationCenterCity = doctor.vaccinationCenter.city,
                    vaccinationCenterStreet = doctor.vaccinationCenter.address,
                    patientAccountId = doctor.patientAccount.id,
                };
            }
            return info;
        }
        public async Task<List<DoctorIncomingAppResponse>> GetDoctorIncomingAppointments(Guid doctorId)
        {
            var apps = dbContext.Appointments.Where(a => a.state == AppointmentState.Planned)
                .Include(a => a.timeSlot).Include(a => a.timeSlot.doctor)
                .Where(a => a.timeSlot.doctor.doctorId == doctorId).Include(a => a.patient)
                .Include(a => a.vaccine).ToList();
            var incApps = new List<DoctorIncomingAppResponse>();
            DoctorIncomingAppResponse incAppointment;
            foreach (var app in apps.ToList())
            {
                incAppointment = new DoctorIncomingAppResponse()
                {
                    vaccineName = app.vaccine.name,
                    vaccineCompany = app.vaccine.company,
                    vaccineVirus = app.vaccine.virus.ToString(),
                    whichVaccineDose = app.whichDose,
                    appointmentId = app.id,
                    from = app.timeSlot.from.ToString("dd-MM-yyyy HH:mm"),
                    to = app.timeSlot.to.ToString("dd-MM-yyyy HH:mm"),
                    patientFirstName = app.patient.firstName,
                    patientLastName = app.patient.lastName,
                };
                incApps.Add(incAppointment);
            }
            return incApps;
        }
        public async Task<List<DoctorFormerAppResponse>> GetDoctorFormerAppointments(Guid doctorId)
        {
            var apps = dbContext.Appointments.Where(a => a.state == AppointmentState.Finished
                                                || a.state == AppointmentState.Cancelled
                                                // || a.state == AppointmentState.Expired
                                                )
                .Include(a => a.timeSlot).Include(a => a.timeSlot.doctor)
                .Where(a => a.timeSlot.doctor.doctorId == doctorId).Include(a => a.patient)
                .Include(a => a.vaccine).ToList();
            var formerApps = new List<DoctorFormerAppResponse>();
            DoctorFormerAppResponse formerAppointment;
            foreach (var app in apps.ToList())
            {
                formerAppointment = new DoctorFormerAppResponse()
                {
                    vaccineName = app.vaccine.name,
                    vaccineCompany = app.vaccine.company,
                    vaccineVirus = app.vaccine.virus.ToString(),
                    whichVaccineDose = app.whichDose,
                    appointmentId = app.id,
                    from = app.timeSlot.from.ToString("dd-MM-yyyy HH:mm"),
                    to = app.timeSlot.to.ToString("dd-MM-yyyy HH:mm"),
                    patientFirstName = app.patient.firstName,
                    patientLastName = app.patient.lastName,
                    batchNumber = app.vaccineBatchNumber,
                    PESEL = app.patient.pesel,
                    state = app.state.ToString(),
                };
                formerApps.Add(formerAppointment);
            }
            return formerApps;
        }

        public async Task<(bool,bool)> UpdateVaccinationCount(Guid doctorId, Guid appointmentId)
        {
            var appointment = await dbContext.Appointments.Include(a=>a.vaccine).Include(a => a.timeSlot)
                .Include(a => a.timeSlot.doctor).Include(a=>a.patient).SingleAsync(a => a.id == appointmentId);
            var doctor = await dbContext.Doctors.SingleAsync(d => d.doctorId == doctorId);
            if (appointment == null)
                return (false,false);
            if (doctor.active == false)
                return (false,false);
            if (appointment.timeSlot.doctor.doctorId != doctorId)
                throw new ArgumentException();

            appointment.state = AppointmentState.Finished;
            if (appointment.whichDose == 1)
            {
                //await dbContext.Database.BeginTransactionAsync();
                var newCount = new VaccinationCount()
                {
                    virus = appointment.vaccine.virus,
                    count = 1,
                    patient = appointment.patient
                };
                dbContext.VaccinationCounts.Add(newCount);
                //await dbContext.Database.CommitTransactionAsync();
                await dbContext.SaveChangesAsync();
            }
            else
            {
                var count = await dbContext.VaccinationCounts.Include(c=>c.patient)
                    .SingleAsync(c => c.patient == appointment.patient && c.virus == appointment.vaccine.virus);
                if (count == null)
                    return (false,false);
                count.count++;
            }
            if (appointment.whichDose == appointment.vaccine.numberOfDoses)
                appointment.certifyState = CertificateState.LastNotCertified;


            await dbContext.SaveChangesAsync();
            bool canCertify = (appointment.certifyState == CertificateState.LastNotCertified) ? true : false;
           
            return (true,canCertify);
        }

        public async Task<bool> UpdateBatchInAppointment(Guid doctorId, Guid appointmentId, string batchId)
        {
            var appointment = await dbContext.Appointments.Include(a => a.timeSlot)
                .Include(a => a.timeSlot.doctor).SingleAsync(a => a.id == appointmentId);
            var doctor = await dbContext.Doctors.SingleAsync(d => d.doctorId == doctorId);
            if (appointment == null)
                return false;
            if (doctor.active == false)
                return false;
            if (appointment.timeSlot.doctor.doctorId != doctorId)
                throw new ArgumentException();

            appointment.vaccineBatchNumber = batchId;

            await dbContext.SaveChangesAsync();
            //await dbContext.Database.CommitTransactionAsync();
            return true;
        }
        public async Task<StartVaccinationResponse> GetStartedAppointmentInfo(Guid doctorId, Guid appointmentId)
        {
            var appointment = dbContext.Appointments
                .Include(a => a.timeSlot).Include(a => a.timeSlot.doctor).Include(a => a.patient)
                .Include(a => a.vaccine).SingleOrDefault(a=>a.id == appointmentId);
            if(appointment.state!= AppointmentState.Planned || appointment.timeSlot.doctor.doctorId!=doctorId)
                throw new ArgumentException();
            StartVaccinationResponse response = null;
            if(appointment!=null)
            {
                response = new StartVaccinationResponse()
                {
                    vaccineName = appointment.vaccine.name,
                    vaccineCompany = appointment.vaccine.company,
                    numberOfDoses = appointment.vaccine.numberOfDoses,
                    minDaysBetweenDoses = appointment.vaccine.minDaysBetweenDoses,
                    maxDaysBetweenDoses = appointment.vaccine.maxDaysBetweenDoses,
                    virusName = appointment.vaccine.virus.ToString(),
                    minPatientAge = appointment.vaccine.minPatientAge,
                    maxPatientAge = appointment.vaccine.maxPatientAge,
                    patientFirstName = appointment.patient.firstName,
                    patientLastName = appointment.patient.lastName,
                    PESEL = appointment.patient.pesel,
                    dateOfBirth = appointment.patient.dateOfBirth.ToString("dd-MM-yyyy"),
                    from = appointment.timeSlot.from.ToString("dd-MM-yyyy HH:mm"),
                    to = appointment.timeSlot.to.ToString("dd-MM-yyyy HH:mm"),
                };
            }
            return response;
        }
        public async Task<List<CityResponse>> GetCities()
        {
            var cityNames = await dbContext.VaccinationCenters.Select(vC => vC.city)
                .Distinct().Select(city => new CityResponse() { city = city }).ToListAsync();

            return cityNames;
        }

        public async Task<List<VirusResponse>> GetViruses()
        {
            var virusesNames = await dbContext.Vaccines.Select(v => v.virus.ToString()).Distinct().Select(v => new VirusResponse() { virus = v }).ToListAsync();

            return virusesNames;
        }

        public async Task<bool> UpdateAppointmentVaccinationDidNotHappen(Guid doctorId, Guid appointmentId)
        {
            var appointment = await dbContext.Appointments.Include(a => a.timeSlot).Include(a => a.timeSlot.doctor).SingleAsync(a => a.id == appointmentId);
            var doctor = await dbContext.Doctors.SingleAsync(d => d.doctorId == doctorId);
            if (appointment == null)
                return false;
            if (doctor.active == false)
                return false;
            if (appointment.timeSlot.doctor.doctorId != doctorId)
                throw new ArgumentException();

            appointment.certifyState = CertificateState.NotLast;
            appointment.state = AppointmentState.Cancelled;
            appointment.vaccineBatchNumber = "";

            await dbContext.SaveChangesAsync();
            //await dbContext.Database.CommitTransactionAsync();
            return true;
        }

        public async Task<bool> EditVaccine(EditVaccine vaccine)
        {
            var dbVaccine = await dbContext.Vaccines.SingleOrDefaultAsync(v => v.id == vaccine.vaccineId);

            if (dbVaccine == null)
                return false;

            dbVaccine.name = vaccine.name;
            dbVaccine.company = vaccine.company;
            dbVaccine.minDaysBetweenDoses = vaccine.minDaysBetweenDoses;
            dbVaccine.maxDaysBetweenDoses = vaccine.maxDaysBetweenDoses;
            dbVaccine.minPatientAge = vaccine.minPatientAge;
            dbVaccine.maxPatientAge = vaccine.maxPatientAge;
            dbVaccine.virus = (Virus)Enum.Parse(typeof(Virus), vaccine.virus);
            dbVaccine.active = vaccine.active;
            dbVaccine.numberOfDoses = vaccine.numberOfDoses;

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
