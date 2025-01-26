//==========================================================
// Student Number : S10240903H
// Student Name	  : Sim Wen Jye Timothy
// Partner Name	  : Tang Wei Zheng Caden
//==========================================================

using System;

namespace PRG2_T13_02
{
    public abstract class Flight : IComparable<Flight>
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
        public Flight() { }
        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = status;
        }
        public abstract double CalculateFees();
        public override string ToString()
        {
            return string.Format("{0,-10} {1,-10} {2,-10} {3,-10} {4,-10}", FlightNumber, Origin, Destination, ExpectedTime, Status);
        }

        public int CompareTo(Flight other)
        {
            return ExpectedTime.CompareTo(other.ExpectedTime);
        }
    }
}
