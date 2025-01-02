using Microsoft.EntityFrameworkCore;
using MockClassLibrary.Models;
using MockExamConsoleApp.Data;

class Program
{
    static void Main(string[] args)
    {
        int answer;
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n----------------\n");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("\n 1: List all passengers on a flight, \n 2: List the revenue for a flight \n 3: Exit");

            answer = Convert.ToInt32(Console.ReadLine());

            switch (answer)
            {
                case 1:
                    Console.WriteLine("Enter the Flight ID: ");
                    int FlightID1 = Convert.ToInt32(Console.ReadLine());
                    list_passengers(FlightID1);
                    break;

                case 2:
                    Console.WriteLine("Enter the Flight ID: ");
                    int FlightID2 = Convert.ToInt32(Console.ReadLine());
                    list_revenue(FlightID2);
                    break;

                case 3:
                    exit = true;
                    break;

            }
        }
    }

    static void list_passengers(int FlightID)
    {
        using (var context = new FlightContext())
        {
            var passengers = context.Bookings
                .Include(b => b.Passenger)
                .Include(b => b.Flight)
                .Where(b => b.FlightID == FlightID)
                .ToList();

            foreach (var booking in passengers)
            {
                Console.WriteLine($"Passenger Name: {booking.Passenger.Name}, Ticket Type: {booking.TicketType}, Destination: {booking.Flight.Destination}");
            }
        }
    }

    static void list_revenue(int FlightID)
    {
        using (var context = new FlightContext())
        {
            var passengers = context.Bookings
                .Where(p => p.FlightID == FlightID)
                .ToList();

            double total = 0;

            foreach (var p in passengers)
            {
                total += p.TicketCost + p.BaggageCharge;
            }
            Console.WriteLine($"Total Revenue for Flight {FlightID}: {total}");
        }
    }
}