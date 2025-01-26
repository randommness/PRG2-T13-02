//==========================================================
// Student Number : S10240903H
// Student Name	  : Sim Wen Jye Timothy
// Partner Name	  : Tang Wei Zheng Caden
//==========================================================

namespace PRG2_T13_02
{
    public class NORMFlight : Flight
    {
        public NORMFlight() : base() { }
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status) : base(flightNumber, origin, destination, expectedTime, status)
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
