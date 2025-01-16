using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_02
{
    public class NORMFlight : Flight
    {
        public NORMFlight() : base() { }
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime) : base(flightNumber, origin, destination, expectedTime)
        {
        }
        public override double CalculateFees()
        {
            if (Origin == "Singapore(SIN)")
            {
                return 800; // Base gate fee + special request fee
            }
            else
            {
                return 500; // Base gate fee + special request fee
            }
        }
        public override string ToString()
        {
            return string.Format("{0,-10} {1,-10} {2,-10} {3,-10} {4,-10} {5,-10}", FlightNumber, Origin, Destination, ExpectedTime, Status, CalculateFees());
        }
    }
}
