using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MockClassLibrary.Models;
using System.Threading.Tasks;

namespace MockExamConsoleApp.Data
{
    public class FlightContext : DbContext
    {
        public DbSet<Flights> Flights { get; set; }
        public DbSet<Passengers> Passengers { get; set; }
        public DbSet<Bookings> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MockExamFlightDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flights>()
                .HasKey(f => f.FlightID);

            modelBuilder.Entity<Passengers>()
                .HasKey(p => p.PassengerID);

            modelBuilder.Entity<Bookings>()
                .HasKey(b => new { b.PassengerID, b.FlightID });

            modelBuilder.Entity<Flights>()
                .HasMany(f => f.Bookings)
                .WithOne(b => b.Flight)
                .HasForeignKey(b => b.FlightID);

            modelBuilder.Entity<Passengers>()
                .HasMany(p => p.Bookings)
                .WithOne(b => b.Passenger)
                .HasForeignKey(b => b.PassengerID);

            modelBuilder.Entity<Bookings>()
                .Property(b => b.TicketCost)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Flights>().HasData(
                new Flights { FlightID = 1, FlightNumber = "IT-001", DepartureDate = new DateTime(2025, 12, 1, 22, 00, 00), Origin = "Dublin", Destination = "Rome", Country = "Italy", MaxSeats = 110 },
                new Flights { FlightID = 2, FlightNumber = "EN-002", DepartureDate = new DateTime(2025, 12, 1, 22, 00, 00), Origin = "Dublin", Destination = "London", Country = "England", MaxSeats = 110 },
                new Flights { FlightID = 3, FlightNumber = "FR-001", DepartureDate = new DateTime(2025, 12, 1, 22, 00, 00), Origin = "Dublin", Destination = "Paris", Country = "France", MaxSeats = 120 },
                new Flights { FlightID = 4, FlightNumber = "BE-001", DepartureDate = new DateTime(2025, 12, 1, 22, 00, 00), Origin = "Dublin", Destination = "Brussels", Country = "Belgium", MaxSeats = 88 },
                new Flights { FlightID = 5, FlightNumber = "DU-001", DepartureDate = new DateTime(2025, 12, 1, 22, 00, 00), Origin = "London", Destination = "Dublin", Country = "Ireland", MaxSeats = 110 }
                );

            modelBuilder.Entity<Passengers>().HasData(
                new Passengers { PassengerID = 1, Name = "Fred Farnell", PassportNumber = "P010203" },
                new Passengers { PassengerID = 2, Name = "Tom McManus", PassportNumber = "P896745" },
                new Passengers { PassengerID = 3, Name = "Bill Trimble", PassportNumber = "P231425" },
                new Passengers { PassengerID = 4, Name = "Freda McDonald", PassportNumber = "P235678" },
                new Passengers { PassengerID = 5, Name = "Mary Malone", PassportNumber = "P214587" },
                new Passengers { PassengerID = 6, Name = "Tom McManus", PassportNumber = "P893482" }

                );

            modelBuilder.Entity<Bookings>().HasData(
                new Bookings { PassengerID = 1, FlightID = 2, TicketType = TicketType.Economy, TicketCost = 51.83, BaggageCharge = 30.00 },
                new Bookings { PassengerID = 2, FlightID = 2, TicketType = TicketType.FirstClass, TicketCost = 127.00, BaggageCharge = 10.00 },
                new Bookings { PassengerID = 3, FlightID = 3, TicketType = TicketType.FirstClass, TicketCost = 140.00, BaggageCharge = 10.00 },
                new Bookings { PassengerID = 4, FlightID = 4, TicketType = TicketType.Economy, TicketCost = 50.00, BaggageCharge = 15.00 },
                new Bookings { PassengerID = 5, FlightID = 1, TicketType = TicketType.Economy, TicketCost = 69.00, BaggageCharge = 15.00 },
                new Bookings { PassengerID = 6, FlightID = 5, TicketType = TicketType.FirstClass, TicketCost = 127.00, BaggageCharge = 10.00 }
                );


        }
        public FlightContext()
        {

        }

        public FlightContext(DbContextOptions<FlightContext> options) : base(options)
        {
        }
    }
}
