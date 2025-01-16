using PRG2_T13_02;

internal class Program
{
    private static void Main(string[] args)
    {
        // Open file: Please fetch and pull via Git
        // Close file: Make sure to commit changes if ready
        // DON'T EDIT MASTER BRANCH
        Console.WriteLine("hi");
        Dictionary<string, Flight> flightDict = LoadFlights();
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
}