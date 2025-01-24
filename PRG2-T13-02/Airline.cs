//==========================================================
// Student Number : S10266694
// Student Name	  : Tang Wei Zheng Caden
// Partner Name	  : Sim Wen Jye Timothy
//==========================================================

namespace PRG2_T13_02
{
    public class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }

        public Airline() { }
        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
            Flights = new Dictionary<string, Flight>();
        }

        public bool AddFlight(Flight fl)
        {
            return (Flights.TryAdd(fl.FlightNumber, fl));
        }

        public double CalculateFees()
        {
            return 0; // ADVANCED FEATURE, edit later
        }

        public bool RemoveFlight(Flight fl)
        {
            return (Flights.Remove(fl.FlightNumber));
        }

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
