//========================================================================
// Student Numbers : S10266694J, S10240903H
// Student Names   : Tang Wei Zheng Caden, Sim Wen Jye Timothy
//
// Basic Features 2,3,5,6,9 done by : Tang Wei Zheng Caden (S10266694J)
// Basic Features 1,4,7,8 done by   : Sim Wen Jye Timothy (S10240903H)
//
// Advanced Feature A done by       : Tang Wei Zheng Caden (S10266694J)
// Advanced Feature B done by       : Sim Wen Jye Timothy (S10240903H)
//========================================================================

using System;
using PRG2_T13_02;

internal class Program
{
    private static void Main(string[] args)
    {
        // Initialise 3 dictionaries based on the 3 csv files by calling the corresponding methods.
        Dictionary<string, Airline> airlines = new Dictionary<string, Airline>();
        LoadAirlineFile(airlines);
        Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();
        LoadBoardingGateFile(boardingGates);
        Dictionary<string, Flight> flightDict = LoadFlights(airlines);
        Dictionary<string, double> gateFees = new Dictionary<string, double>();
        Terminal term5 = new Terminal("Terminal 5", airlines, flightDict, boardingGates, gateFees);
        Console.WriteLine("\n\n\n");

        // Loop keeps going until user manually exits.
        while (true)
        {
            int menuInput = DisplayMenu();
            // Call the correct method based on user input.
            if (menuInput == 1)
            {
                ListAllFlights(airlines, flightDict, term5);
            }
            else if (menuInput == 2)
            {
                ListAllBoardingGates(boardingGates);
            }
            else if (menuInput == 3)
            {
                AssignBoardingGateToFlight(flightDict, boardingGates);
            }
            else if (menuInput == 4)
            {
                CreateFlight(flightDict, airlines);
            }
            else if (menuInput == 5)
            {
                DisplayAirlineFlights(airlines, boardingGates);
            }
            else if (menuInput == 6)
            {
                ModifyFlightDetails(airlines, flightDict, boardingGates);
            }
            else if (menuInput == 7)
            {
                DisplayScheduledFlights(airlines, flightDict, boardingGates, term5);
            }
            else if (menuInput == 8)
            {
                ProcessAllUnassignedFlights(flightDict, boardingGates, airlines, term5);
            }
            else if (menuInput == 9)
            {
                DisplayTotalFeePerAirline(term5.Airlines, term5.BoardingGates, term5);
            }
            else
            {
                Console.WriteLine("Goodbye!");
                break;
            }
            Console.WriteLine("\n\n\n");
        }
    }

    // DisplayMenu() displays the menu and returns the user's input.
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
            Console.WriteLine("8. Process all unassigned flights");
            Console.WriteLine("9. Display Total Fee per Airline for the Day");
            Console.WriteLine("0. Exit\n");

            try
            {
                Console.WriteLine("Please select your option:");
                int userOption = Convert.ToInt32(Console.ReadLine());
                if (userOption >= 0 && userOption <= 9)
                {
                    return userOption;
                }
                Console.WriteLine("Please pick an option in the menu (0 to 8). Please try again.\n");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Only numerical values are accepted. Please try again.\n");
            }
        }
    }

    // LoadFlights() method creates the Flight objects from the flights.csv file (basic feature 2).
    private static Dictionary<string, Flight> LoadFlights(Dictionary<string, Airline> airlines)
    {
        Console.WriteLine("Loading Flights...");
        // Initialise a flight dictionary, and create string array of records in flights.csv.
        Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();
        string[] allFlightsData = File.ReadAllLines("flights.csv");

        for (int i = 1; i < allFlightsData.Length; i++)
        {
            Flight newFlight = null;
            // Handle exceptions - if the CSV file line contains irregular data / invalid flight data, skip over that line and Flight obj is not created.
            try
            {
                string specialReqCode;
                string[] flightData = allFlightsData[i].Split(",");
                specialReqCode = flightData[4];

                // Depending on the special request code, create the corresponding flight object.
                if (specialReqCode == "CFFT")
                {
                    newFlight = new CFFTFlight(flightData[0], flightData[1], flightData[2], Convert.ToDateTime(flightData[3]), "Scheduled");
                }
                else if (specialReqCode == "DDJB")
                {
                    newFlight = new DDJBFlight(flightData[0], flightData[1], flightData[2], Convert.ToDateTime(flightData[3]), "Scheduled");
                }
                else if (specialReqCode == "LWTT")
                {
                    newFlight = new LWTTFlight(flightData[0], flightData[1], flightData[2], Convert.ToDateTime(flightData[3]), "Scheduled");
                }
                else
                {
                    newFlight = new NORMFlight(flightData[0], flightData[1], flightData[2], Convert.ToDateTime(flightData[3]), "Scheduled");
                }
            }
            catch (Exception)
            {
                continue;
            }
            // Add this new object to the dictionary with flight number as key.
            flightDict.Add(newFlight.FlightNumber, newFlight);

            // Assign the flight to an airline, adding this Flight object into the airline's flight dictionary.
            FlightAirlineAssignment(newFlight, airlines);
        }
        // Display successful load message and return the flight dictionary.
        Console.WriteLine($"{flightDict.Count} Flights Loaded!");
        return flightDict;
    }

    private static Dictionary<string, Airline> LoadAirlineFile(Dictionary<string, Airline> air)
    {
        Console.WriteLine("Loading Airlines...");
        string[] lines = File.ReadAllLines("airlines.csv");
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            //create the Airline objects based on the data loaded
            Airline airline = new Airline(parts[0], parts[1]);
            //add the Airlines objects into an Airline Dictionary
            air.Add(parts[1], airline);
        }
        Console.WriteLine($"{air.Count} Airlines Loaded!");
        return air;
    }

    private static Dictionary<string, BoardingGate> LoadBoardingGateFile(Dictionary<string, BoardingGate> bg)
    {
        Console.WriteLine("Loading Boarding Gates...");
        string[] lines2 = File.ReadAllLines("boardingGates.csv");
        for (int i = 1; i < lines2.Length; i++)
        {
            string[] parts = lines2[i].Split(',');
            //create the BoardingGate objects based on the data loaded
            BoardingGate boardingGate = new BoardingGate(parts[0], Convert.ToBoolean(parts[2]), Convert.ToBoolean(parts[1]), Convert.ToBoolean(parts[3]), null);
            //add the BoardingGate objects into a BoardingGate Dictionary
            bg.Add(parts[0], boardingGate);
        }
        Console.WriteLine($"{bg.Count} Boarding Gates Loaded!");
        return bg;
    }

    //  FlightAirlineAssignment() method assigns a Flight object to the corresponding Airline's flight dictionary.
    private static bool FlightAirlineAssignment(Flight fl, Dictionary<string, Airline> airlines)
    {
        // Search through each Airline object in the airline dictionary.
        string flCode = fl.FlightNumber.Split(" ")[0];
        foreach (Airline searchAirline in airlines.Values)
        {
            if (searchAirline.Code == flCode)
            {
                searchAirline.AddFlight(fl);
                return true;
            }
        }
        return false;
    }

    // FindSpecialRequestCode() method returns the special request code (if any) based on the Flight object
    public static string FindSpecialRequestCode(Flight fl)
    {
        if (fl is CFFTFlight)
        {
            return "CFFT";
        }
        else if (fl is DDJBFlight)
        {
            return "DDJB";
        }
        else if (fl is LWTTFlight)
        {
            return "LWTT";
        }
        return "None";
    }


    // ListAllFlights() is menu option 1, basic feature 3. It displays all flights and their information.
    private static void ListAllFlights(Dictionary<string, Airline> airlines, Dictionary<string, Flight> flightDict, Terminal term5)
    {
        // Display headers.
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Flights for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");

        // Loop through each pair in flight dictionary.
        foreach (Flight fl in flightDict.Values)
        {
            // Display flight information, formatted.
            if (term5.GetAirlineFromFlight(fl) != null)
            {
                Console.WriteLine($"{fl.FlightNumber,-16}{term5.GetAirlineFromFlight(fl).Name,-23}{fl.Origin,-23}{fl.Destination,-23}{fl.ExpectedTime}");
            }
            else
            {
                Console.WriteLine($"{fl.FlightNumber,-16}{"-",-23}{fl.Origin,-23}{fl.Destination,-23}{fl.ExpectedTime}");
            }
        }
    }

    // ListAllBoardingGates() is menu option 2, basic feature 4. It displays all boarding gates and their information.
    private static void ListAllBoardingGates(Dictionary<string, BoardingGate> boardingGates)
    {
        // Display headers.
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("Gate Name        DDJB                   CFFT                   LWTT");

        // Loop through each boarding gate in the dictionary.
        foreach (BoardingGate gate in boardingGates.Values)
        {
            // Display gate information, formatted.
            Console.WriteLine($"{gate.GateName,-16} {gate.SupportsDDJB,-22} {gate.SupportsCFFT,-22} {gate.SupportsLWTT,-22}");
        }
    }

    // AssignBoardingGateToFlight() is menu option 3, basic feature 5. It assigns a boarding gate to a flight based on user input.
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

            if (!flightDict.TryGetValue(flightNo, out flightObj))
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

            if (!boardingGates.TryGetValue(boardingGate, out boardingGateObj))
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
        Console.WriteLine($"Special Request Code: {FindSpecialRequestCode(flightObj)}");

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
        else
        {
            flightObj.Status = "On Time";
        }

        // Display successful update message, then break out of this loop and method.
        Console.WriteLine($"Flight {flightObj.FlightNumber} has been assigned to Boarding Gate {boardingGateObj.GateName}!");
    }

    // CreateFlight() is menu option 4, basic feature 6. It creates a new Flight object based on user input, appending to flightDict and flights.csv.
    public static void CreateFlight(Dictionary<string, Flight> flightDict, Dictionary<string, Airline> airlines)
    {
        // Keep looping until user is done with creating flight(s).
        while (true)
        {
            // Prompt for necessary flight info.
            // Input validation - flight number must be at least 1 char long and cannot already exist.
            Console.Write("Enter Flight Number: ");
            string flightNo = Console.ReadLine();
            if (flightNo == "")
            {
                Console.WriteLine("Flight Number cannot be empty. Try again.\n");
                continue;
            }
            if (flightDict.ContainsKey(flightNo))
            {
                Console.WriteLine($"Flight {flightNo} already exists. Try again.\n");
                continue;
            }

            // Input validation - both the origin and destination must be at least 1 char long.
            string origin;
            string destination;
            while (true)
            {
                Console.Write("Enter Origin: ");
                origin = Console.ReadLine();
                if (origin == "")
                {
                    Console.WriteLine("Origin cannot be empty. Try again.\n");
                    continue;
                }
                break;
            }
            while (true)
            {
                Console.Write("Enter Destination: ");
                destination = Console.ReadLine();
                if (destination == "")
                {
                    Console.WriteLine("Destination cannot be empty. Try again.\n");
                    continue;
                }
                break;
            }

            // Input validation - If the expected time is not of DateTime format, catch exception and repeat.
            DateTime expectedTime;
            while (true)
            {
                try
                {
                    Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
                    expectedTime = Convert.ToDateTime(Console.ReadLine());
                    break;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Wrong format. Try again.\n");
                }
            }

            // Data validation - Special Request Code input must be valid.
            Flight newFlight;
            string reqCode;
            while (true)
            {
                // Prompt user for special request code, if any.
                Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
                reqCode = Console.ReadLine().ToUpper();

                // Downcasting - Create the Flight object based on the special request code.
                if (reqCode == "CFFT")
                {
                    newFlight = new CFFTFlight(flightNo, origin, destination, expectedTime, "Scheduled");
                }
                else if (reqCode == "DDJB")
                {
                    newFlight = new DDJBFlight(flightNo, origin, destination, expectedTime, "Scheduled");
                }
                else if (reqCode == "LWTT")
                {
                    newFlight = new LWTTFlight(flightNo, origin, destination, expectedTime, "Scheduled");
                }
                else if (reqCode == "NONE" || reqCode == "")
                {
                    reqCode = "";
                    newFlight = new NORMFlight(flightNo, origin, destination, expectedTime, "Scheduled");
                }
                else
                {
                    Console.WriteLine("Invalid Special Request Code. Try again.\n");
                    continue;
                }
                break;
            }

            // Add this flight object to the flight dictionary and the corresponding airline's flight dictionary.
            flightDict.Add(flightNo, newFlight);
            if (!FlightAirlineAssignment(newFlight, airlines))
            {
                // No tied airline.
                Console.WriteLine("WARNING: The flight you created has no airline assigned to it.");
            }

            // Add the new flight's information into flights.csv file.
            File.AppendAllText("flights.csv", $"\n{flightNo},{origin},{destination},{expectedTime},{reqCode}");
            Console.WriteLine($"Flight {flightNo} has been added!");

            // Prompt user if they wish to add another flight.
            Console.WriteLine("Would you like to add another flight? (Y/N)");
            string toContinue = Console.ReadLine().ToUpper();
            if (toContinue == "Y")
            {
                Console.WriteLine();
                continue;
            }
            break;
        }
    }

    // DisplayAirlineFlights() is menu option 5, basic feature 7. It displays all flights of a specific airline based on user input.
    private static void DisplayAirlineFlights(Dictionary<string, Airline> airlines, Dictionary<string, BoardingGate> boardingGates)
    {
        // Display headers.
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("Airline Code   Airline Name");

        // List all airlines.
        foreach (Airline airline in airlines.Values)
        {
            Console.WriteLine($"{airline.Code,-14}{airline.Name}");
        }

        // Prompt user to enter the airline code.
        Console.Write("Enter Airline Code: ");
        string airlineCode = Console.ReadLine().ToUpper();

        // Retrieve the selected airline.
        if (!airlines.TryGetValue(airlineCode, out Airline selectedAirline))
        {
            Console.WriteLine("Invalid Airline Code. Please try again.");
            return;
        }

        // Display flights for the selected airline.
        Console.WriteLine("=============================================");
        Console.WriteLine($"List of Flights for {selectedAirline.Name}");
        Console.WriteLine("=============================================");
        Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time   Special Request Code   Boarding Gate");

        foreach (Flight flight in selectedAirline.Flights.Values)
        {
            string specialRequestCode = FindSpecialRequestCode(flight);
            string boardingGateName = "Unassigned";
            foreach (BoardingGate gate in boardingGates.Values)
            {
                if (gate.Flight == flight)
                {
                    boardingGateName = gate.GateName;
                    break;
                }
            }
            Console.WriteLine($"{flight.FlightNumber,-16}{selectedAirline.Name,-23}{flight.Origin,-23}{flight.Destination,-23}{flight.ExpectedTime,-34}{specialRequestCode,-23}{boardingGateName}");
        }
    }
    // ModifyFlightDetails() is menu option 6, basic feature 8. It allows user to modify flight details based on user input.
    private static void ModifyFlightDetails(Dictionary<string, Airline> airlines, Dictionary<string, Flight> flightDict, Dictionary<string, BoardingGate> boardingGates)
    {
        // Display headers.
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("Airline Code   Airline Name");

        // List all airlines.
        foreach (Airline airline in airlines.Values)
        {
            Console.WriteLine($"{airline.Code,-14}{airline.Name}");
        }

        // Prompt user to enter the airline code.
        string airlineCode;
        while (true)
        {
            Console.Write("Enter Airline Code: ");
            airlineCode = Console.ReadLine().ToUpper();

            if (airlines.ContainsKey(airlineCode))
            {
                break;
            }
            Console.WriteLine("Invalid Airline Code. Please try again.");
        }
        // Retrieve the selected airline
        Airline selectedAirline = airlines[airlineCode];

        // Display flights for the selected airline.
        Console.WriteLine("=============================================");
        Console.WriteLine($"List of Flights for {selectedAirline.Name}");
        Console.WriteLine("=============================================");
        Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time   Special Request Code   Boarding Gate");

        foreach (Flight flight in selectedAirline.Flights.Values)
        {
            string specialRequestCode = FindSpecialRequestCode(flight);
            string boardingGateName = "Unassigned";
            foreach (BoardingGate gate in boardingGates.Values)
            {
                if (gate.Flight == flight)
                {
                    boardingGateName = gate.GateName;
                    break;
                }
            }
            Console.WriteLine($"{flight.FlightNumber,-16}{selectedAirline.Name,-23}{flight.Origin,-23}{flight.Destination,-23}{flight.ExpectedTime,-34}{specialRequestCode,-23}{boardingGateName}");
        }

        // Prompt user to select a flight number.
        Flight selectedFlight;
        while (true)
        {
            Console.Write("Choose an existing Flight to modify or delete: ");
            string flightNumber = Console.ReadLine();

            if (flightDict.ContainsKey(flightNumber))
            {
                selectedFlight = flightDict[flightNumber];
                break;
            }
            Console.WriteLine("Invalid Flight Number. Please try again.");
        }

        // Prompt user to choose an action.
        int action = 0;
        while (true)
        {
            Console.WriteLine("1. Modify Flight");
            Console.WriteLine("2. Delete Flight");
            Console.Write("Choose an option: ");

            try
            {
                action = Convert.ToInt32(Console.ReadLine());
                if (action == 1 || action == 2) break;
                Console.WriteLine("Invalid input. Please enter 1 or 2.");
            }
            catch
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        // Handle Modify Flight
        if (action == 1)
        {
            int modifyOption = 0;
            while (true)
            {
                Console.WriteLine("1. Modify Basic Information");
                Console.WriteLine("2. Modify Status");
                Console.WriteLine("3. Modify Special Request Code");
                Console.WriteLine("4. Modify Boarding Gate");
                Console.Write("Choose an option: ");

                try
                {
                    modifyOption = Convert.ToInt32(Console.ReadLine());
                    if (modifyOption >= 1 && modifyOption <= 4) break;
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

            if (modifyOption == 1)
            {
                Console.Write("Enter new Origin: ");
                selectedFlight.Origin = Console.ReadLine();

                Console.Write("Enter new Destination: ");
                selectedFlight.Destination = Console.ReadLine();

                while (true)
                {
                    Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
                    string input = Console.ReadLine();
                    try
                    {
                        selectedFlight.ExpectedTime = Convert.ToDateTime(input);
                        break;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Invalid format. Please use dd/MM/yyyy HH:mm.");
                    }
                }
            }
            else if (modifyOption == 2)
            {
                Console.Write("Enter new Status: ");
                selectedFlight.Status = Console.ReadLine();
            }
            else if (modifyOption == 3)
            {
                Console.Write("Enter new Special Request Code: ");
                string specialRequestCode = Console.ReadLine();
                // Update special request code logic here.
            }
            else if (modifyOption == 4)
            {
                while (true)
                {
                    Console.Write("Enter new Boarding Gate: ");
                    string boardingGateName = Console.ReadLine();

                    if (boardingGates.ContainsKey(boardingGateName))
                    {
                        boardingGates[boardingGateName].Flight = selectedFlight;
                        break;
                    }
                    Console.WriteLine("Invalid Boarding Gate. Please try again.");
                }
            }

            Console.WriteLine("Flight updated!");
        }

        // Handle Delete Flight
        else if (action == 2)
        {
            string confirmation = string.Empty;
            while (true)
            {
                Console.Write("Are you sure you want to delete this flight? [Y/N]: ");
                confirmation = Console.ReadLine().ToUpper();

                if (confirmation == "Y" || confirmation == "N") break;
                Console.WriteLine("Invalid input. Please enter Y or N.");
            }

            if (confirmation == "Y")
            {
                selectedAirline.Flights.Remove(selectedFlight.FlightNumber);
                flightDict.Remove(selectedFlight.FlightNumber);
                Console.WriteLine("Flight deleted!");
            }
            else
            {
                Console.WriteLine("Flight deletion canceled.");
            }
        }

        // Display updated flight details.
        Console.WriteLine("=============================================");
        Console.WriteLine("Updated Flight Details");
        Console.WriteLine("=============================================");
        Console.WriteLine($"Flight Number: {selectedFlight.FlightNumber}");
        Console.WriteLine($"Airline Name: {selectedAirline.Name}");
        Console.WriteLine($"Origin: {selectedFlight.Origin}");
        Console.WriteLine($"Destination: {selectedFlight.Destination}");
        Console.WriteLine($"Expected Departure/Arrival Time: {selectedFlight.ExpectedTime}");
        Console.WriteLine($"Status: {selectedFlight.Status}");
        Console.WriteLine($"Special Request Code: {FindSpecialRequestCode(selectedFlight)}");

        // Find the boarding gate for the flight.
        string assignedBoardingGate = "Unassigned";
        foreach (BoardingGate gate in boardingGates.Values)
        {
            if (gate.Flight == selectedFlight)
            {
                assignedBoardingGate = gate.GateName;
                break;
            }
        }
        Console.WriteLine($"Boarding Gate: {assignedBoardingGate}");
    }

    // DisplayScheduledFlights() is menu option 7, basic feature 9. It displays all flights and info, ordered by Expected Departure/Arrival Time.
    public static void DisplayScheduledFlights(Dictionary<string, Airline> airlines, Dictionary<string, Flight> flightDict, Dictionary<string, BoardingGate> boardingGates, Terminal term5)
    {
        // Display headers.
        Console.WriteLine("=============================================");
        Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("Flight Number   Airline Name           Origin                Destination           Expected Departure/Arrival Time    Status        Special Code   Boarding Gate");

        // Set the Flight objects into a list. This allows us to use Sort(), which sorts by DateTime.
        List<Flight> flightList = new List<Flight>(flightDict.Values);
        flightList.Sort();

        foreach (Flight fl in flightList)
        {
            // Find the boarding gate. If none are found, stick to default ("Unassigned").
            string boardingGateName = "Unassigned";
            foreach (BoardingGate bg in boardingGates.Values)
            {
                if (bg.Flight == fl)
                {
                    boardingGateName = bg.GateName;
                    break;
                }
            }

            // Display the relevant information, formatted with respect to the headers.
            if (term5.GetAirlineFromFlight(fl) != null)
            {
                Console.WriteLine($"{fl.FlightNumber,-16}{term5.GetAirlineFromFlight(fl).Name,-23}{fl.Origin,-22}" +
                                  $"{fl.Destination,-22}{fl.ExpectedTime,-35}{fl.Status,-14}{FindSpecialRequestCode(fl),-15}{boardingGateName,-13}");
            }
            else
            {
                Console.WriteLine($"{fl.FlightNumber,-16}{"-",-23}{fl.Origin,-22}" +
                                  $"{fl.Destination,-22}{fl.ExpectedTime,-35}{fl.Status,-14}{FindSpecialRequestCode(fl),-15}{boardingGateName,-13}");
            }
        }
    }

    // ProcessAllUnassignedFlights() is menu option 8, advanced feature A. It mass processes all unassigned flights to a boarding gate.
    public static void ProcessAllUnassignedFlights(Dictionary<string, Flight> flightDict, Dictionary<string, BoardingGate> boardingGates, Dictionary<string, Airline> airlines, Terminal term5)
    {
        // Headers.
        Console.WriteLine("================================================");
        Console.WriteLine("Automatic gate assignment for unassigned flights");
        Console.WriteLine("================================================");

        // Initialise a queue and 2 variables - unassignedBg (unassigned Boarding Gate) & alreadyAssigned (assigned Flights)
        Queue<Flight> unassignedFlights = new Queue<Flight>();
        int unassignedBg = 0;
        int alreadyAssigned = 0;

        // Loop through each Flight object and BoardingGate object till a BoardingGate's Flight attribute contains the same Flight object.
        foreach (Flight fl in flightDict.Values)
        {
            bool assigned = false;
            foreach (BoardingGate bg in boardingGates.Values)
            {
                if (bg.Flight == fl)
                {
                    assigned = true;
                    alreadyAssigned++;
                    break;
                }
            }
            if (!assigned) // Flight is not assigned to any boarding gate before, enqueue it.
            {
                unassignedFlights.Enqueue(fl);
            }
        }

        // Loop through each BoardingGate object to count the number of unassigned Boarding Gates.
        foreach (BoardingGate bg in boardingGates.Values)
        {
            if (bg.Flight == null)
            {
                unassignedBg++;
            }
        }

        // Display number of unassigned flights and number of unassigned boarding gates.
        Console.WriteLine($"Number of flights without a boarding gate assigned: {unassignedFlights.Count}");
        Console.WriteLine($"Number of boarding gates without a flight assigned: {unassignedBg}");

        // Display headers and initialise processCount, an integer that counts the number of automatic assignments.
        Console.WriteLine("\nList of updated flights:");
        Console.WriteLine("Flight Number   Airline Name           Origin                Destination           Expected Departure/Arrival Time    Special Code   Boarding Gate");
        int processCount = 0;

        // Loop through the entire Queue.
        while (unassignedFlights.Count > 0)
        {
            // Dequeue a Flight object, get it's special request code.
            Flight unassignedFl = unassignedFlights.Dequeue();
            string specialReqCode = FindSpecialRequestCode(unassignedFl);
            foreach (BoardingGate bg in boardingGates.Values)
            {
                // If either the boarding gate supports the requested code, or the boarding gate does NOT support any code (if no special request code),
                // and this boarding gate has no assigned flight, then assign the Flight object to this BoardingGate object.
                if ((bg.Flight == null) &&
                   ((specialReqCode == "CFFT" && bg.SupportsCFFT) || (specialReqCode == "DDJB" && bg.SupportsDDJB) || (specialReqCode == "LWTT" && bg.SupportsLWTT) ||
                    (specialReqCode == "None" && !bg.SupportsCFFT && !bg.SupportsDDJB && !bg.SupportsLWTT)))
                {
                    bg.Flight = unassignedFl;

                    // Display updated information about this flight. Update processCount and move on to the next Flight object.
                    if (term5.GetAirlineFromFlight(unassignedFl) != null)
                    {
                        Console.WriteLine($"{unassignedFl.FlightNumber,-16}{term5.GetAirlineFromFlight(unassignedFl).Name,-23}{unassignedFl.Origin,-22}" +
                                          $"{unassignedFl.Destination,-22}{unassignedFl.ExpectedTime,-35}{specialReqCode,-15}{bg.GateName}");
                    }
                    else
                    {
                        Console.WriteLine($"{unassignedFl.FlightNumber,-16}{"-",-23}{unassignedFl.Origin,-22}" +
                                          $"{unassignedFl.Destination,-22}{unassignedFl.ExpectedTime,-35}{specialReqCode,-15}{bg.GateName}");
                    }
                    processCount++;
                    break;
                }
            }
        }

        // Display statistics.
        // Display the number of automatic assignments.
        Console.WriteLine();
        if (processCount == 0)
        {
            // No flights were automatically assigned.
            Console.WriteLine("All flights were previously assigned, no new flights were automatically assigned to a gate.");
        }

        Console.WriteLine($"Number of flights and boarding gates processed and assigned: {processCount}");

        // Display the number of Flights and Boarding Gates processed automatically over those already assigned, as a percentage.
        if (alreadyAssigned != 0)
        {
            Console.WriteLine($"Percentage of automatic assignment over manual assignment: " +
                              $"{((Convert.ToDouble(processCount) / Convert.ToDouble(alreadyAssigned)) * 100).ToString("F2")}%");
        }
        else // If there was no manual assignment piror to this, display that all processed flights so far were done automatically.
        {
            Console.WriteLine($"All {processCount} processed flights were performed automatically without manual assignment.");
        }
    }

    // DisplayTotalFeePerAirline() is menu option 9, advanced feature b. It calculates the total fee per airline for the day.
    private static void DisplayTotalFeePerAirline(Dictionary<string, Airline> airlines, Dictionary<string, BoardingGate> boardingGates, Terminal terminal)
    {
        // Create a list to store flight numbers of unassigned flights.
        List<string> unassignedFlights = new List<string>();

        // Check if all flights have been assigned boarding gates.
        foreach (Airline airline in airlines.Values)
        {
            foreach (Flight flight in airline.Flights.Values)
            {
                bool isAssigned = false;

                // Check if the flight is assigned to any boarding gate.
                foreach (BoardingGate gate in boardingGates.Values)
                {
                    if (gate.Flight != null && gate.Flight.FlightNumber == flight.FlightNumber)
                    {
                        isAssigned = true;
                        break;
                    }
                }

                // If the flight is not assigned to any boarding gate, add its flight number to the list.
                if (!isAssigned)
                {
                    unassignedFlights.Add(flight.FlightNumber);
                }
            }
        }

        // If there are unassigned flights, display an error message with the flight numbers.
        if (unassignedFlights.Count > 0)
        {
            Console.WriteLine($"Flights {string.Join(", ", unassignedFlights)} have not been assigned boarding gates. Please assign all flights before running this feature again.");
            return;
        }

        double totalFees = 0;
        double totalDiscounts = 0;

        // Display headers.
        Console.WriteLine("=============================================");
        Console.WriteLine("Total Fees per Airline for the Day");
        Console.WriteLine("=============================================");

        // Calculate fees for each airline.
        foreach (Airline airline in airlines.Values)
        {
            double airlineFees = 0;
            double airlineDiscounts = 0;

            int flightCount = airline.Flights.Count;
            int earlyLateFlights = 0;
            int specificOriginFlights = 0;
            int noSpecialRequestFlights = 0;

            foreach (Flight flight in airline.Flights.Values)
            {
                double flightFee = 0;

                // Check if the origin or destination is Singapore (SIN).
                if (flight.Origin == "Singapore (SIN)")
                {
                    flightFee += 800;
                }
                if (flight.Destination == "Singapore (SIN)")
                {
                    flightFee += 500;
                }

                // Check for special request codes and apply additional fees.
                string specialRequestCode = FindSpecialRequestCode(flight);
                if (specialRequestCode == "CFFT")
                {
                    flightFee += 200;
                }
                else if (specialRequestCode == "DDJB")
                {
                    flightFee += 150;
                }
                else if (specialRequestCode == "LWTT")
                {
                    flightFee += 100;
                }

                // Apply the boarding gate base fee.
                flightFee += 300;

                // Add the flight fee to the airline's total fees.
                airlineFees += flightFee;

                // Check for promotional discounts.
                if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour >= 21)
                {
                    earlyLateFlights++;
                }
                if (flight.Origin == "Dubai (DXB)" || flight.Origin == "Bangkok (BKK)" || flight.Origin == "Tokyo (NRT)")
                {
                    specificOriginFlights++;
                }
                if (specialRequestCode == "None")
                {
                    noSpecialRequestFlights++;
                }
            }

            // Apply promotional discounts.
            int discountForEvery3Flights = (flightCount / 3) * 350;
            int discountForEarlyLateFlights = earlyLateFlights * 110;
            int discountForSpecificOriginFlights = specificOriginFlights * 25;
            int discountForNoSpecialRequestFlights = noSpecialRequestFlights * 50;

            airlineDiscounts = discountForEvery3Flights + discountForEarlyLateFlights + discountForSpecificOriginFlights + discountForNoSpecialRequestFlights;

            // Apply additional discount for airlines with more than 5 flights.
            if (flightCount > 5)
            {
                airlineDiscounts += airlineFees * 0.03; // 3% off the total bill.
            }

            // Calculate the final fees for the airline.
            double finalAirlineFees = airlineFees - airlineDiscounts;

            // Display the fees for the airline.
            Console.WriteLine($"Airline: {airline.Name}");
            Console.WriteLine($"Original Subtotal: ${airlineFees:F2}");
            Console.WriteLine($"Discounts: -${airlineDiscounts:F2}");
            Console.WriteLine($"Final Total: ${finalAirlineFees:F2}");
            Console.WriteLine();

            // Add to the total fees and discounts.
            totalFees += airlineFees;
            totalDiscounts += airlineDiscounts;
        }

        // Calculate the final total fees for all airlines.
        double finalTotalFees = totalFees - totalDiscounts;

        // Display the final totals.
        Console.WriteLine("=============================================");
        Console.WriteLine("Summary of All Airline Fees");
        Console.WriteLine("=============================================");
        Console.WriteLine($"Total Fees: ${totalFees:F2}");
        Console.WriteLine($"Total Discounts: -${totalDiscounts:F2}");
        Console.WriteLine($"Final Total Fees Collected: ${finalTotalFees:F2}");
        Console.WriteLine($"Percentage of Discounts: {totalDiscounts / totalFees:P2}");
    }
}