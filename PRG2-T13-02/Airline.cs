namespace PRG2_T13_02
{
    public class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }

        public Airline() { }
        public Airline(string name, string code, Dictionary<string, Flight> flights)
        {
            Name = name;
            Code = code;
            Flights = flights;
        }

        // Methods: AddFlight(), CalculateFees(), RemoveFlight(), ToString()
    }
}
