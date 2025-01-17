﻿//==========================================================
// Student Number : 
// Student Name	  : 
// Partner Name	  : Tang Wei Zheng Caden
//==========================================================

namespace PRG2_T13_02
{
    public class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }
        public DDJBFlight() : base() { }
        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime) : base(flightNumber, origin, destination, expectedTime)
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
        public override string ToString()
        {
            return string.Format("{0,-10} {1,-10} {2,-10} {3,-10} {4,-10} {5,-10}", FlightNumber, Origin, Destination, ExpectedTime, Status, CalculateFees());
        }
    }
}
