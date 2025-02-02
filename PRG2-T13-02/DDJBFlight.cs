//==========================================================
// Student Number : S10240903H
// Student Name	  : Sim Wen Jye Timothy
// Partner Name	  : Tang Wei Zheng Caden
//==========================================================

namespace PRG2_T13_02
{
    public class DDJBFlight : Flight
    {
        // Properties and Attributes.
        public double RequestFee { get; set; }

        // Default and Parameterized Constructors.
        public DDJBFlight() : base() { }
        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = 300;
        }

        // CalculateFees() returns the fees (excl. discounts) for request code + arrival/departure base fee.
        public override double CalculateFees()
        {
            return base.CalculateFees() + RequestFee;
        }

        // ToString() returns flight information.
        public override string ToString()
        {
            return $"{base.ToString()}, Fees: {CalculateFees()}";
        }
    }
}
