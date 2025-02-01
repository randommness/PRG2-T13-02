//==========================================================
// Student Number : S10240903H
// Student Name	  : Sim Wen Jye Timothy
// Partner Name	  : Tang Wei Zheng Caden
//==========================================================

namespace PRG2_T13_02
{
    public class NORMFlight : Flight
    {
        // Parameterized and Default Constructors.
        public NORMFlight() : base() { }
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status) : base(flightNumber, origin, destination, expectedTime, status) { }

        // CalculateFees() returns the fees (excl. discounts) for arrival/departure base fee (no request fee).
        public override double CalculateFees()
        {
            return base.CalculateFees();
        }

        // ToString() returns flight information.
        public override string ToString()
        {
            return string.Format("{0,-10} {1,-10} {2,-10} {3,-10} {4,-10} {5,-10}", FlightNumber, Origin, Destination, ExpectedTime, Status, CalculateFees());
        }
    }
}
