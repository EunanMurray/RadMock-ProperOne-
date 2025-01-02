using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace MockClassLibrary.Models
{
    public class Bookings
    {
        public TicketType TicketType { get; set; }
        public double TicketCost { get; set; }
        public double BaggageCharge { get; set;}

        public int FlightID { get; set; }
        public virtual Flights Flight { get; set; }

        public int PassengerID { get; set; }
        public virtual Passengers Passenger { get; set; }
    }

    public enum TicketType
    {
        Economy,
        FirstClass
    }
}

