using PRG2_T13_02;

internal class Program
{
    private static void Main(string[] args)
    {
        // Open file: Please fetch and pull via Git
        // Close file: Make sure to commit changes if ready
        // DON'T EDIT MASTER BRANCH
        Dictionary<string, Airline> airlines = new Dictionary<string, Airline>();
        Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();
        Dictionary<string, Flight> flightDict = LoadFlights();
        LoadAirlineFile(airlines);
        Console.WriteLine(airlines);
        LoadBoardingGateFile(boardingGates);
        Console.WriteLine(boardingGates);
    }
    
    private static Dictionary<string, Flight> LoadFlights()
    {
        Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();
        string[] allFlightsData = File.ReadAllLines("flights.csv");
        for (int i = 1; i < allFlightsData.Length; i++)
        {
            // Flight Number,Origin,Destination,Expected Departure/Arrival,Special Request Code
            string[] flightData = allFlightsData[i].Split(",");
            string specialReqCode = flightData[4];
            Flight newFlight;
            if (specialReqCode == "CFFT")
            {
                newFlight = new CFFTFlight(flightData[0], flightData[1], flightData[2], Convert.ToDateTime(flightData[3]));
            }
            else if (specialReqCode == "DDJB")
            {
                newFlight = new DDJBFlight(flightData[0], flightData[1], flightData[2], Convert.ToDateTime(flightData[3]));
            }
            else if (specialReqCode == "LWTT")
            {
                newFlight = new LWTTFlight(flightData[0], flightData[1], flightData[2], Convert.ToDateTime(flightData[3]));
            }
            else
            {
                newFlight = new NORMFlight(flightData[0], flightData[1], flightData[2], Convert.ToDateTime(flightData[3]));
            }

            flightDict.Add(newFlight.FlightNumber, newFlight);
        }
        return flightDict;
    }
  
    private static void LoadAirlineFile(Dictionary<string, Airline> air)
    {
        string[] lines = File.ReadAllLines("airlines.csv");
        for(int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            //create the Airline objects based on the data loaded
            Airline airline = new Airline(parts[0], parts[1], null);
            //add the Airlines objects into an Airline Dictionary
            air.Add(parts[1], airline);
        }
    }
  
    private static void LoadBoardingGateFile(Dictionary<string, BoardingGate> bg)
    {
        string[] lines2 = File.ReadAllLines("boardingGates.csv");
        for (int i = 1; i < lines2.Length; i++)
        {
            string[] parts = lines2[i].Split(',');
            //create the BoardingGate objects based on the data loaded
            BoardingGate boardingGate = new BoardingGate(parts[0], Convert.ToBoolean(parts[1]), Convert.ToBoolean(parts[2]), Convert.ToBoolean(parts[3]), null);
            //add the BoardingGate objects into a BoardingGate Dictionary
            bg.Add(parts[0], boardingGate);
        }
    }
}