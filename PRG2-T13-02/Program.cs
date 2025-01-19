﻿using PRG2_T13_02;

// Open file: Please fetch and pull via Git
// Close file: Make sure to commit changes if ready
// DON'T EDIT MASTER BRANCH

internal class Program
{
    private static void Main(string[] args)
    {
        Dictionary<string, Airline> airlines = new Dictionary<string, Airline>();
        LoadAirlineFile(airlines);
        Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();
        LoadBoardingGateFile(boardingGates);
        Dictionary<string, Flight> flightDict = LoadFlights();
        Console.WriteLine("\n\n\n");


        int menuInput = DisplayMenu();
        if (menuInput == 1)
        {
            ListAllFlights(airlines, flightDict);
        }
        else if (menuInput == 3)
        {
            AssignBoardingGateToFlight(flightDict, boardingGates);
        }
    }
    
    private static int DisplayMenu()
    {
        while (true)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Welcome to Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine("1. List All Flights");
            Console.WriteLine("2. List Boarding Gates");
            Console.WriteLine("3. Assign a Boarding Gate to a Flight");
            Console.WriteLine("4. Create Flight");
            Console.WriteLine("5. Display Airline Flights");
            Console.WriteLine("6. Modify Flight Details");
            Console.WriteLine("7. Display Flight Schedule");
            Console.WriteLine("0. Exit");

            try
            {
                Console.WriteLine("\nPlease select your option:");
                int userOption = Convert.ToInt32(Console.ReadLine());
                if (userOption >= 0 && userOption <= 7)
                {
                    return userOption;
                }
                Console.WriteLine("Please pick an option in the menu (0 to 7). Please try again.\n");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Only numerical values are accepted. Please try again.\n");
            }
        }
    }

    private static Dictionary<string, Flight> LoadFlights()
    {
        Console.WriteLine("Loading Flights...");
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
                newFlight = new CFFTFlight(flightData[0], flightData[1], flightData[2], Convert.ToDateTime(flightData[3]), "On Time");
            }
            else if (specialReqCode == "DDJB")
            {
                newFlight = new DDJBFlight(flightData[0], flightData[1], flightData[2], Convert.ToDateTime(flightData[3]), "On Time");
            }
            else if (specialReqCode == "LWTT")
            {
                newFlight = new LWTTFlight(flightData[0], flightData[1], flightData[2], Convert.ToDateTime(flightData[3]), "On Time");
            }
            else
            {
                newFlight = new NORMFlight(flightData[0], flightData[1], flightData[2], Convert.ToDateTime(flightData[3]), "On Time");
            }

            // Add this new object to the dictionary with flight number as key.
            flightDict.Add(newFlight.FlightNumber, newFlight);
        }
        Console.WriteLine($"{flightDict.Count} Flights Loaded!");
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

    public static void AssignBoardingGateToFlight(Dictionary<string, Flight> flightDict, Dictionary<string, BoardingGate> boardingGates)
    {
        // Headers.
        Console.WriteLine("=============================================");
        Console.WriteLine("Assign a Boarding Gate to a Flight");
        Console.WriteLine("=============================================");

        // Initialise the flight and boarding gate objects for later.
        Flight flightObj = null;
        BoardingGate boardingGateObj = null;

        // Keep looping through until we get a valid flight object.
        while (true)
        {
            // Prompt for flight number. Search for corresponding flight object.
            Console.WriteLine("Enter Flight Number:");
            string flightNo = Console.ReadLine();
            foreach (Flight fl in flightDict.Values)
            {
                if (fl.FlightNumber == flightNo)
                {
                    flightObj = fl;
                    break;
                }
            }
            if (flightObj == null)
            {
                // No corresponding object found.
                Console.WriteLine("Invalid flight number, please try again.\n");
                continue;
            }
            break;
        }

        // Keep looping through until we get a valid boardingGate object.
        while (true)
        {
            // Prompt for boarding gate. Search for corresponding boarding gate object.
            Console.WriteLine("Enter Boarding Gate Name:");
            string boardingGate = Console.ReadLine();
            foreach (BoardingGate bg in boardingGates.Values)
            {
                if (bg.GateName == boardingGate)
                {
                    boardingGateObj = bg;
                    break;
                }
            }
            if (boardingGateObj == null)
            {
                // No corresponding object found.
                Console.WriteLine("Invalid boarding gate, please try again.\n");
                continue;
            }

            if (boardingGateObj.Flight != null)
            {
                // Boarding gate is used.
                Console.WriteLine($"Boarding Gate is used by Flight {boardingGateObj.Flight.FlightNumber}. Please try again.\n");
                continue;
            }
            break;
        }

        // Display basic flight information and the special request code (if any).
        Console.WriteLine($"Flight Number: {flightObj.FlightNumber}");
        Console.WriteLine($"Origin: {flightObj.Origin}");
        Console.WriteLine($"Destination: {flightObj.Destination}");
        Console.WriteLine($"Expected Time: {flightObj.ExpectedTime}");
        if (flightObj is DDJBFlight)
        {
            Console.WriteLine("Special Request Code: DDJB");
        }
        else if (flightObj is CFFTFlight)
        {
            Console.WriteLine("Special Request Code: CFFT");
        }
        else if (flightObj is LWTTFlight)
        {
            Console.WriteLine("Special Request Code: LWTT");
        }
        else
        {
            Console.WriteLine("Special Request Code: None");
        }

        // Display boarding gate information.
        Console.WriteLine($"Boarding Gate Name: {boardingGateObj.GateName}");
        Console.WriteLine($"Supports DDJB: {boardingGateObj.SupportsDDJB}");
        Console.WriteLine($"Supports CFFT: {boardingGateObj.SupportsCFFT}");
        Console.WriteLine($"Supports LWTT: {boardingGateObj.SupportsLWTT}");

        // Assign the flight object to this boading gate object.
        boardingGateObj.Flight = flightObj;

        // Prompt for user whether they wish to update flight status.
        Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
        string userResponse = Console.ReadLine().ToUpper();
        if (userResponse == "Y")
        {
            // Keep looping through until we successfully update the status.
            while (true)
            {
                Console.WriteLine("1. Delayed");
                Console.WriteLine("2. Boarding");
                Console.WriteLine("3. On Time");
                Console.WriteLine("Please select the new status of the flight:");
                try
                {
                    // Try to convert input to integer, then update the object's status attribute based on input.
                    int userInput = Convert.ToInt32(Console.ReadLine());
                    if (userInput == 1)
                    {
                        flightObj.Status = "Delayed";
                    }
                    else if (userInput == 2)
                    {
                        flightObj.Status = "Boarding";
                    }
                    else if (userInput == 3)
                    {
                        flightObj.Status = "On Time";
                    }
                    else
                    {
                        // Input was integer but not one of the 3 stated options.
                        Console.WriteLine("Invalid status option, please try again.\n");
                        continue;
                    }
                    break;
                }
                catch (FormatException ex)
                {
                    // Input was not of integer type.
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Only numerical values are accepted. Please try again.\n");
                    continue;
                }
            }
        }

        // Display successful update message, then break out of this loop and method.
        Console.WriteLine($"Flight {flightObj.FlightNumber} has been assigned to Boarding Gate {boardingGateObj.GateName}!");
        }
    }