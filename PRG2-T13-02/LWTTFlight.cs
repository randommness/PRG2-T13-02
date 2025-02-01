//==========================================================
// Student Number : S10240903H
// Student Name	  : Sim Wen Jye Timothy
// Partner Name	  : Tang Wei Zheng Caden
//==========================================================

namespace PRG2_T13_02
{
    public class LWTTFlight : Flight
    {
        // Properties and Attributes.
        public double RequestFee { get; set; }

        // Parameterized and Default Constructors.
        public LWTTFlight() : base() { }
        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = 500;
        }

        // CalculateFees() returns the fees (excl. discounts) for request code + arrival/departure base fee.
        public override double CalculateFees()
        {
            return base.CalculateFees() + RequestFee;
        }

        // ToString() returns flight information.
        public override string ToString()
        {
            return string.Format("{0,-10} {1,-10} {2,-10} {3,-10} {4,-10} {5,-10}", FlightNumber, Origin, Destination, ExpectedTime, Status, CalculateFees());
        }
    }
}
