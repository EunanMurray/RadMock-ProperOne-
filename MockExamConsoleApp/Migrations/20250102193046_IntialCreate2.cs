using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MockExamConsoleApp.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightID);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassengerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.PassengerID);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    FlightID = table.Column<int>(type: "int", nullable: false),
                    PassengerID = table.Column<int>(type: "int", nullable: false),
                    TicketType = table.Column<int>(type: "int", nullable: false),
                    TicketCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaggageCharge = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => new { x.PassengerID, x.FlightID });
                    table.ForeignKey(
                        name: "FK_Bookings_Flights_FlightID",
                        column: x => x.FlightID,
                        principalTable: "Flights",
                        principalColumn: "FlightID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Passengers_PassengerID",
                        column: x => x.PassengerID,
                        principalTable: "Passengers",
                        principalColumn: "PassengerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightID", "Country", "DepartureDate", "Destination", "FlightNumber", "MaxSeats", "Origin" },
                values: new object[,]
                {
                    { 1, "Italy", new DateTime(2025, 12, 1, 22, 0, 0, 0, DateTimeKind.Unspecified), "Rome", "IT-001", 110, "Dublin" },
                    { 2, "England", new DateTime(2025, 12, 1, 22, 0, 0, 0, DateTimeKind.Unspecified), "London", "EN-002", 110, "Dublin" },
                    { 3, "France", new DateTime(2025, 12, 1, 22, 0, 0, 0, DateTimeKind.Unspecified), "Paris", "FR-001", 120, "Dublin" },
                    { 4, "Belgium", new DateTime(2025, 12, 1, 22, 0, 0, 0, DateTimeKind.Unspecified), "Brussels", "BE-001", 88, "Dublin" },
                    { 5, "Ireland", new DateTime(2025, 12, 1, 22, 0, 0, 0, DateTimeKind.Unspecified), "Dublin", "DU-001", 110, "London" }
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "PassengerID", "Name", "PassportNumber" },
                values: new object[,]
                {
                    { 1, "Fred Farnell", "P010203" },
                    { 2, "Tom McManus", "P896745" },
                    { 3, "Bill Trimble", "P231425" },
                    { 4, "Freda McDonald", "P235678" },
                    { 5, "Mary Malone", "P214587" },
                    { 6, "Tom McManus", "P893482" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "FlightID", "PassengerID", "BaggageCharge", "TicketCost", "TicketType" },
                values: new object[,]
                {
                    { 2, 1, 30.0, 51.83m, 0 },
                    { 2, 2, 10.0, 127m, 1 },
                    { 3, 3, 10.0, 140m, 1 },
                    { 4, 4, 15.0, 50m, 0 },
                    { 1, 5, 15.0, 69m, 0 },
                    { 5, 6, 10.0, 127m, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FlightID",
                table: "Bookings",
                column: "FlightID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Passengers");
        }
    }
}
