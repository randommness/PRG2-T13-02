using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_02
{
    internal abstract class Flight
    {
        public string FlightNumber { get; set; }
        public string origin { get; set; }
        public string Destination { get; set; }
        public DateTime ETD { get; set; }
        public DateTime ETA { get; set; }
        public Flight(string flightNumber, string destination, DateTime etd, DateTime eta)
        {
            FlightNumber = flightNumber;
            Destination = destination;
            ETD = etd;
            ETA = eta;
        }
        public abstract double CalculatePrice();
        public abstract string ToString();
    }
}
