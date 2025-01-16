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

        ListAllFlights(airlines, flightDict);
    }
    
    private static Dictionary<string, Flight> LoadFlights()
    {
        // Initialise a flight dictionary, and create string array of records in flights.csv.
        Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();
        string[] allFlightsData = File.ReadAllLines("flights.csv");

        for (int i = 1; i < allFlightsData.Length; i++)
        {
            // Split the line of data, and also get the special request code (if any).
            string[] flightData = allFlightsData[i].Split(",");
            string specialReqCode = flightData[4];
            Flight newFlight;

            // Depending on the special request code, create the corresponding flight object.
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

            // Add this new object to the dictionary with flight number as key.
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

    private static void ListAllFlights(Dictionary<string, Airline> airlines, Dictionary<string, Flight> flightDict)
    {
        // Display headers.
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Flights for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");
        
        // Loop through each pair in flight dictionary.
        foreach (Flight fl in flightDict.Values)
        {
            // Search for the tied airline. 
            string airline = null;
            foreach (Airline al in airlines.Values)
            {
                // Loop through each airline's flight dictionary to find which airline has this flight number.
                foreach (string flightNo in al.Flights.Keys)
                {
                    if (flightNo == fl.FlightNumber)
                    {
                        airline = al.Name;
                        break;
                    }
                }
                if (airline != null) // Found, can break from loop.
                {
                    break;
                }
            }
            // Display flight information, formatted.
            Console.WriteLine($"{fl.FlightNumber,-16}{airline,-23}{fl.Origin,-23}{fl.Destination,-23}{fl.ExpectedTime}");
        }
    }
}