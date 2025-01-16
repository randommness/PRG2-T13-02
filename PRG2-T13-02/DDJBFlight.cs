using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_02
{
    public class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }
        public DDJBFlight() : base() { }
        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = 500;
        }
        public override double CalculateFees()
        {
            if (Origin == "Singapore(SIN)")
            {
                return 800 + RequestFee; // Base gate fee + special request fee
            }
            else
            {
                return 500 + RequestFee; // Base gate fee + special request fee
            }
        }
    }
}
