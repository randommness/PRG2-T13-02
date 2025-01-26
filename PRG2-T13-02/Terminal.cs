//==========================================================
// Student Number : S10266694J
// Student Name	  : Tang Wei Zheng Caden
// Partner Name	  : Sim Wen Jye Timothy
//==========================================================

namespace PRG2_T13_02
{
    public class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }
        public Dictionary<string, BoardingGate> BoardingGates { get; set; }
        public Dictionary<string, double> GateFees { get; set; }

        public Terminal() { }
        public Terminal(string terminalName, Dictionary<string, Airline> airlines, Dictionary<string, Flight> flights, Dictionary<string, BoardingGate> boardingGates, Dictionary<string, double> gateFees)
        {
            TerminalName = terminalName;
            Airlines = airlines;
            Flights = flights;
            BoardingGates = boardingGates;
            GateFees = gateFees;
        }

        public bool AddAirline(Airline al)
        {
            return(Airlines.TryAdd(al.Code, al));
        }

        public bool AddBoardingGate(BoardingGate bg)
        {
            return(BoardingGates.TryAdd(bg.GateName, bg));
        }

        public Airline GetAirlineFromFlight(Flight Flight)
        {
            foreach (KeyValuePair<string, Airline> kvp in Airlines)
            {
                foreach (KeyValuePair<string, Flight> a in kvp.Value.Flights)
                {
                    if (a.Value == Flight)
                    {
                        return kvp.Value;
                    }
                }
            }
            return null;
        }

        public void PrintAirlineFees()
        {
            foreach (KeyValuePair<string, Airline> kvp in Airlines)
            {
                Console.WriteLine($"Airline: {kvp.Key}, Fees: {kvp.Value.CalculateFees()}");
            }
        }

        public override string ToString()
        {
            string output = $"Terminal Name: {TerminalName}";
            output += "\nList of airlines:";
            foreach (Airline al in Airlines.Values)
            {
                output += $"\n{al.ToString()}";
            }
            output += "\nList of flights:";
            foreach (Flight fl in Flights.Values)
            {
                output += $"\n{fl.ToString()}";
            }
            output += "\nList of boarding gates:";
            foreach (BoardingGate bg in BoardingGates.Values)
            {
                output += $"\n{bg.ToString()}";
            }
            output += "\nList of gate fees:";
            foreach (KeyValuePair<string, double> kvp in GateFees)
            {
                output += $"\n{kvp.Key}: ${kvp.Value}";
            }
            return output;
        }
    }
}
