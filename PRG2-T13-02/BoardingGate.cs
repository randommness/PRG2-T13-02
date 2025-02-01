//==========================================================
// Student Number : S10266694J
// Student Name	  : Tang Wei Zheng Caden
// Partner Name	  : Sim Wen Jye Timothy
//==========================================================

namespace PRG2_T13_02
{
    public class BoardingGate
    {
        // Properties and Attributes.
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        // Parameterized and Default Constructors.
        public BoardingGate() { }
        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT, Flight flight)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
            Flight = flight;
        }

        // CalculateFees() returns the total fee (excl. discounts) for that flight.
        public double CalculateFees()
        {
            return Flight.CalculateFees() + 300;
        }

        // ToString() methos returns information about the boarding gate.
        public override string ToString()
        {
            return $"Boarding Gate Name: {GateName}, Supports DDJB: {SupportsDDJB}, " +
                   $"Supports CFFT: {SupportsCFFT}, Supports LWTT: {SupportsLWTT}, Flight: {Flight}";
        }
    }
}
