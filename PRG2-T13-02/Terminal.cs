//==========================================================
// Student Number : S10266694J
// Student Name	  : Tang Wei Zheng Caden
// Partner Name	  : Sim Wen Jye Timothy
//==========================================================

namespace PRG2_T13_02
{
    public class Terminal
    {
        // Properties and Attributes.
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }
        public Dictionary<string, BoardingGate> BoardingGates { get; set; }
        public Dictionary<string, double> GateFees { get; set; }

        // Parameterized and Default Constructors.
        public Terminal() { }
        public Terminal(string terminalName, Dictionary<string, Airline> airlines, Dictionary<string, Flight> flights, Dictionary<string, BoardingGate> boardingGates, Dictionary<string, double> gateFees)
        {
            TerminalName = terminalName;
            Airlines = airlines;
            Flights = flights;
            BoardingGates = boardingGates;
            GateFees = gateFees;
        }

        // AddAirline() adds an Airline object to the Airlines dictionary.
        public bool AddAirline(Airline al)
        {
            return(Airlines.TryAdd(al.Code, al));
        }

        // AddBoardingGate() adds a BoardingGate object to the BoardingGates dictionary.
        public bool AddBoardingGate(BoardingGate bg)
        {
            return(BoardingGates.TryAdd(bg.GateName, bg));
        }

        // GetAirlineFromFlight() returns the Airline object that holds the Flight object in it's dictionary.
        public Airline GetAirlineFromFlight(Flight Flight)
        {
            foreach (KeyValuePair<string, Airline> kvp in Airlines)
            {
                if (kvp.Value.Flights.ContainsValue(Flight))
                {
                    return kvp.Value;
                }
            }
            return null;
        }

        // PrintAirlineFees() is part of advanced feature B. It displays the fees the Airline needs to pay.
        public void PrintAirlineFees()
        {
            foreach (KeyValuePair<string, Airline> kvp in Airlines)
            {
                Console.WriteLine($"Airline: {kvp.Key}, Fees: {kvp.Value.CalculateFees()}");
            }
        }

        // ToString() displays all relavent infromation realted to the Terminal.
        public override string ToString()
        {
            string output = $"Terminal Name: {TerminalName}";
            output += "\n\n\nList of airlines:";
            foreach (Airline al in Airlines.Values)
            {
                output += $"\n\n{al.ToString()}";
            }
            output += "\n\n\nList of flights:";
            foreach (Flight fl in Flights.Values)
            {
                output += $"\n{fl.ToString()}";
            }
            output += "\n\n\nList of boarding gates:";
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
