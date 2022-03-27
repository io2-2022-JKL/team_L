using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VaccinationSystem.Migrations
{
    public partial class createdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pesel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OpeningHours",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    from = table.Column<TimeSpan>(type: "time", nullable: false),
                    to = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHours", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    pesel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "VaccinationCenters",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patientid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.id);
                    table.ForeignKey(
                        name: "FK_Certificates_Patients_Patientid",
                        column: x => x.Patientid,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VaccinationCounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    virus = table.Column<int>(type: "int", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    patientid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinationCounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_VaccinationCounts_Patients_patientid",
                        column: x => x.patientid,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vaccinationCenterid = table.Column<int>(type: "int", nullable: true),
                    patientAccountid = table.Column<int>(type: "int", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    pesel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.id);
                    table.ForeignKey(
                        name: "FK_Doctors_Patients_patientAccountid",
                        column: x => x.patientAccountid,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctors_VaccinationCenters_vaccinationCenterid",
                        column: x => x.vaccinationCenterid,
                        principalTable: "VaccinationCenters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numberOfDoses = table.Column<int>(type: "int", nullable: false),
                    minDaysBetweenDoses = table.Column<int>(type: "int", nullable: false),
                    maxDaysBetweenDoses = table.Column<int>(type: "int", nullable: false),
                    virus = table.Column<int>(type: "int", nullable: false),
                    minPatientAge = table.Column<int>(type: "int", nullable: false),
                    maxPatientAge = table.Column<int>(type: "int", nullable: false),
                    used = table.Column<bool>(type: "bit", nullable: false),
                    VaccinationCenterid = table.Column<int>(type: "int", nullable: true)
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
                name: "TimeSlot",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    from = table.Column<DateTime>(type: "datetime2", nullable: false),
                    to = table.Column<DateTime>(type: "datetime2", nullable: false),
                    doctorid = table.Column<int>(type: "int", nullable: true),
                    isFree = table.Column<bool>(type: "bit", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlot", x => x.id);
                    table.ForeignKey(
                        name: "FK_TimeSlot_Doctors_doctorid",
                        column: x => x.doctorid,
                        principalTable: "Doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    whichDose = table.Column<int>(type: "int", nullable: false),
                    timeSlotid = table.Column<int>(type: "int", nullable: true),
                    patientid = table.Column<int>(type: "int", nullable: true),
                    vaccineid = table.Column<int>(type: "int", nullable: true),
                    state = table.Column<int>(type: "int", nullable: false),
                    vaccineBatchNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctorid = table.Column<int>(type: "int", nullable: true),
                    //Doctorid1 = table.Column<int>(type: "int", nullable: true),
                    //Patientid = table.Column<int>(type: "int", nullable: true),
                    //Patientid1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_Doctorid",
                        column: x => x.Doctorid,
                        principalTable: "Doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    /*table.ForeignKey(
                        name: "FK_Appointments_Doctors_Doctorid1",
                        column: x => x.Doctorid1,
                        principalTable: "Doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);*/
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_patientid",
                        column: x => x.patientid,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    /*table.ForeignKey(
                        name: "FK_Appointments_Patients_Patientid",
                        column: x => x.Patientid,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);*/
                    /*table.ForeignKey(
                        name: "FK_Appointments_Patients_Patientid1",
                        column: x => x.Patientid1,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);*/
                    table.ForeignKey(
                        name: "FK_Appointments_TimeSlot_timeSlotid",
                        column: x => x.timeSlotid,
                        principalTable: "TimeSlot",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Vaccines_vaccineid",
                        column: x => x.vaccineid,
                        principalTable: "Vaccines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Doctorid",
                table: "Appointments",
                column: "Doctorid");

            /*migrationBuilder.CreateIndex(
                name: "IX_Appointments_Doctorid1",
                table: "Appointments",
                column: "Doctorid1");*/

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_patientid",
                table: "Appointments",
                column: "patientid");

            /*migrationBuilder.CreateIndex(
                name: "IX_Appointments_Patientid",
                table: "Appointments",
                column: "Patientid");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Patientid1",
                table: "Appointments",
                column: "Patientid1");*/

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_timeSlotid",
                table: "Appointments",
                column: "timeSlotid");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_vaccineid",
                table: "Appointments",
                column: "vaccineid");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_Patientid",
                table: "Certificates",
                column: "Patientid");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_patientAccountid",
                table: "Doctors",
                column: "patientAccountid");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_vaccinationCenterid",
                table: "Doctors",
                column: "vaccinationCenterid");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlot_doctorid",
                table: "TimeSlot",
                column: "doctorid");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationCounts_patientid",
                table: "VaccinationCounts",
                column: "patientid");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_VaccinationCenterid",
                table: "Vaccines",
                column: "VaccinationCenterid");
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
                name: "TimeSlot");

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
