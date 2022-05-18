using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VaccinationSystem.Migrations
{
    public partial class updateappointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pesel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    pesel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "VaccinationCenters",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinationCenters", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vaccineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    patientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.id);
                    table.ForeignKey(
                        name: "FK_Certificates_Patients_patientId",
                        column: x => x.patientId,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VaccinationCounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    virus = table.Column<int>(type: "int", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    patientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinationCounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_VaccinationCounts_Patients_patientId",
                        column: x => x.patientId,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    doctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    vaccinationCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    patientAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.doctorId);
                    table.ForeignKey(
                        name: "FK_Doctors_Patients_patientAccountId",
                        column: x => x.patientAccountId,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctors_VaccinationCenters_vaccinationCenterId",
                        column: x => x.vaccinationCenterId,
                        principalTable: "VaccinationCenters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpeningHours",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    from = table.Column<TimeSpan>(type: "time", nullable: false),
                    to = table.Column<TimeSpan>(type: "time", nullable: false),
                    vaccinationCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    day = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHours", x => x.id);
                    table.ForeignKey(
                        name: "FK_OpeningHours_VaccinationCenters_vaccinationCenterId",
                        column: x => x.vaccinationCenterId,
                        principalTable: "VaccinationCenters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numberOfDoses = table.Column<int>(type: "int", nullable: false),
                    minDaysBetweenDoses = table.Column<int>(type: "int", nullable: false),
                    maxDaysBetweenDoses = table.Column<int>(type: "int", nullable: false),
                    virus = table.Column<int>(type: "int", nullable: false),
                    minPatientAge = table.Column<int>(type: "int", nullable: false),
                    maxPatientAge = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    VaccinationCenterid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccines", x => x.id);
                    table.ForeignKey(
                        name: "FK_Vaccines_VaccinationCenters_VaccinationCenterid",
                        column: x => x.VaccinationCenterid,
                        principalTable: "VaccinationCenters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    from = table.Column<DateTime>(type: "datetime2", nullable: false),
                    to = table.Column<DateTime>(type: "datetime2", nullable: false),
                    doctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isFree = table.Column<bool>(type: "bit", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.id);
                    table.ForeignKey(
                        name: "FK_TimeSlots_Doctors_doctorId",
                        column: x => x.doctorId,
                        principalTable: "Doctors",
                        principalColumn: "doctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VaccinesInCenters",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    vaccineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    vaccineCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinesInCenters", x => x.id);
                    table.ForeignKey(
                        name: "FK_VaccinesInCenters_VaccinationCenters_vaccineCenterId",
                        column: x => x.vaccineCenterId,
                        principalTable: "VaccinationCenters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccinesInCenters_Vaccines_vaccineId",
                        column: x => x.vaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    whichDose = table.Column<int>(type: "int", nullable: false),
                    timeSlotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    patientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    vaccineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    state = table.Column<int>(type: "int", nullable: false),
                    vaccineBatchNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    certifyState = table.Column<int>(type: "int", nullable: false),
                    Patientid1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    doctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    doctorId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_doctorId",
                        column: x => x.doctorId,
                        principalTable: "Doctors",
                        principalColumn: "doctorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_doctorId1",
                        column: x => x.doctorId1,
                        principalTable: "Doctors",
                        principalColumn: "doctorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_patientId",
                        column: x => x.patientId,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_Patientid1",
                        column: x => x.Patientid1,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_TimeSlots_timeSlotId",
                        column: x => x.timeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Vaccines_vaccineId",
                        column: x => x.vaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_doctorId",
                table: "Appointments",
                column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_doctorId1",
                table: "Appointments",
                column: "doctorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_patientId",
                table: "Appointments",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Patientid1",
                table: "Appointments",
                column: "Patientid1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_timeSlotId",
                table: "Appointments",
                column: "timeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_vaccineId",
                table: "Appointments",
                column: "vaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_patientId",
                table: "Certificates",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_patientAccountId",
                table: "Doctors",
                column: "patientAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_vaccinationCenterId",
                table: "Doctors",
                column: "vaccinationCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_vaccinationCenterId",
                table: "OpeningHours",
                column: "vaccinationCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_doctorId",
                table: "TimeSlots",
                column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationCounts_patientId",
                table: "VaccinationCounts",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_VaccinationCenterid",
                table: "Vaccines",
                column: "VaccinationCenterid");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinesInCenters_vaccineCenterId",
                table: "VaccinesInCenters",
                column: "vaccineCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinesInCenters_vaccineId",
                table: "VaccinesInCenters",
                column: "vaccineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "OpeningHours");

            migrationBuilder.DropTable(
                name: "VaccinationCounts");

            migrationBuilder.DropTable(
                name: "VaccinesInCenters");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Vaccines");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "VaccinationCenters");
        }
    }
}
