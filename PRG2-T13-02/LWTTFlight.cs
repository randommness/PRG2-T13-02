using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_02
{
    public class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }
        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }
        public override double CalculateFees()
        {
            return 500 + RequestFee; // Base gate fee + special request fee
        }
    }
}
