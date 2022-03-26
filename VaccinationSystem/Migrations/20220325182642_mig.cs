using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VaccinationSystem.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Certificates",
                columns: new[] { "id", "Patientid", "url" },
                values: new object[] { 1, null, "placeholder" });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "id", "active", "dateOfBirth", "firstName", "lastName", "mail", "password", "patientAccountid", "pesel", "phoneNumber", "vaccinationCenterid" },
                values: new object[] { 1, true, new DateTime(1970, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Janina", "Kowalska", "kowalskaj@mail.com", "password-456", null, "70120319293", "+48444567333", null });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "id", "active", "dateOfBirth", "firstName", "lastName", "mail", "password", "pesel", "phoneNumber" },
                values: new object[] { 1, true, new DateTime(1985, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jan", "Kowalski", "kowalskij@mail.com", "password#123", "85020319293", "+48444222333" });

            migrationBuilder.InsertData(
                table: "TimeSlot",
                columns: new[] { "id", "active", "doctorid", "from", "isFree", "to" },
                values: new object[] { 1, false, null, new DateTime(2022, 4, 13, 12, 30, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2022, 4, 13, 13, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "VaccinationCenters",
                columns: new[] { "id", "active", "address", "city", "name" },
                values: new object[] { 1, true, "ul. Chmielna 15/43", "Warszawa", "Fajny punkt szczepien" });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "id", "VaccinationCenterid", "company", "maxDaysBetweenDoses", "maxPatientAge", "minDaysBetweenDoses", "minPatientAge", "name", "numberOfDoses", "used", "virus" },
                values: new object[] { 1, null, "Pfeizer", 90, 99, 30, 12, "Pfeizer vaccine", 2, true, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Certificates",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TimeSlot",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VaccinationCenters",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
