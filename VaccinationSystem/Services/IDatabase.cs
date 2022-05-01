using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;
using VaccinationSystem.Data;
using VaccinationSystem.DTOs;

namespace VaccinationSystem.Services
{
    public interface IDatabase
    {
        public Task<List<PatientResponse>> GetPatients();
        public Task<PatientResponse> GetPatient(Guid id);
        public Task<List<DoctorResponse>> GetDoctors();
        public void AddPatient(RegisteringPatient patient);
        public bool IsUserInDatabase(string email);
        public LoginResponse AreCredentialsValid(Login login);
        public Task<List<VaccinationCenterResponse>> GetVaccinationCenters();
        public Task<bool> EditVaccinationCenter(EditedVaccinationCenter center);
        public Task<bool> DeleteVaccinationCenter(Guid vaccinationCenterId);
        public Task<Vaccine> GetVaccine(Guid vaccineId);
        public Task<List<Vaccine>> GetVaccinesFromVaccinationCenter(Guid vaccinationCenterId);
        public Task<List<OpeningHoursDays>> GetOpeningHoursFromVaccinationCenter(Guid vaccinationCenterId);
        public Task<List<Doctor>> GetDoctorsFromVaccinationCenter(Guid vaccinationCenterId);
        public Task AddVaccinationCenter(AddVaccinationCenterRequest center);
        public Task<bool> EditPatient(EditedPatient patient);
        public Task<bool> DeletePatient(Guid patientId);
        public Task<bool> AddDoctor(RegisteringDoctor doctor);
        public Task<bool> EditDoctor(EditedDoctor doctor);
        public Task<bool> DeleteDoctor(Guid doctorId);
        public Task<List<TimeSlotsResponse>> GetTimeSlots(Guid doctorId);
        public Task CreateTimeSlots(Guid doctorId, CreateNewVisitRequest visitRequest);
        public Task<bool> EditTimeSlot(Guid doctorId, Guid slotId, EditedTimeSlot timeSlot);
        public Task<bool> DeleteTimeSlots(Guid doctorId, List<DeleteTimeSlot> timeSlotsIds);
        public Task<TimeSlot> GetTimeSlot(Guid timeSlotId);
        public Task<bool> MakeAppointment(Guid patientId, Guid timeSlotId, Guid vaccineID);
        public Task<List<FilterTimeSlotResponse>> GetTimeSlotsWithFiltration(TimeSlotsFilter filter);
        public Task<List<CertificatesResponse>> GetCertificates(Guid patientId);
        public Task<List<FormerAppointmentResponse>> GetFormerAppointments(Guid patientId);
        public Task<List<IncomingAppointmentResponse>> GetIncomingAppointments(Guid patientId);
        public Task<bool> CancelIncomingAppointment(Guid patientId, Guid appointmentId);
        public Task<bool> CreateCertificate(Guid doctorId, Guid appointmentId, string url);
        public Task<Appointment> GetAppointment(Guid appointmentId);
        public Task<Doctor> GetDoctor(Guid doctorId);
        public Task<PatientInfoResponse> GetPatientInfo(Guid patientId);
        public Task<DoctorInfoResponse> GetDoctorInfo(Guid doctorId);
    }
}
