//==========================================================
// Student Number : S10266694J
// Student Name	  : Tang Wei Zheng Caden
// Partner Name	  : Sim Wen Jye Timothy
//==========================================================

namespace PRG2_T13_02
{
    public class Airline
    {
        // Properties and Attributes.
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }

        // Parameterized and Default Constructors
        public Airline() { }
        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
            Flights = new Dictionary<string, Flight>();
        }

        // AddFlight() method adds a Flight object to the Airline's Flights dict.
        public bool AddFlight(Flight fl)
        {
            return (Flights.TryAdd(fl.FlightNumber, fl));
        }

        // CalculateFees() is for advanced feature B.
        public double CalculateFees()
        {
            return 0;
        }

        // RemoveFlight() method removes a Flight object from the Airline's Flights dict.
        public bool RemoveFlight(Flight fl)
        {
            return (Flights.Remove(fl.FlightNumber));
        }

        // ToString() method returns the details (including flights) of the airline.
        public override string ToString()
        {
            string output = $"Name: {Name}, Code: {Code}, Flights:";
            foreach (KeyValuePair<string, Flight> kvp in Flights)
            {
                output += $"\n{kvp.Value}";
            }
            return output;
        }
    }
}
