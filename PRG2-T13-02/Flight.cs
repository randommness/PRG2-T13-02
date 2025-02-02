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
        // Properties and Attributes.
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }

        // Default and Parameterized Constructors.
        public Flight() { }
        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = status;
        }

        // CalculateFees() returns only the arrival/departure fees. It must be overridden by child classes.
        public virtual double CalculateFees()
        {
            if (Origin == "Singapore(SIN)")
            {
                return 800;
            }
            else
            {
                return 500;
            }
        }

        // ToString() returns flight information.
        public override string ToString()
        {
            return $"Flight number: {FlightNumber}, Origin: {Origin}, Destination: {Destination}, Expected Time: {ExpectedTime}, Status: {Status}";
        }

        // CompareTo() is part of IComparable interface. It compares DateTime of 2 Flight objects, for basic feature 9.
        public int CompareTo(Flight other)
        {
            return ExpectedTime.CompareTo(other.ExpectedTime);
        }
    }
}
