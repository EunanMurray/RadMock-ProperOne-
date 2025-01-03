﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockClassLibrary.Models
{
    public class Flights
    {
        public int FlightID { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Country { get; set; }
        public int MaxSeats { get; set; }

        public ICollection<Bookings> Bookings { get; set; }

    }
}
