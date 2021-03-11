using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ElectionSoft
{
    static class ReadWriteClass
    {
        // Election data which are not used for calculations
        private static string electionName;
        private static int electionTotalVotes;

        // Seats to allocate
        public static int seatsToAllocate { get; private set; } = 0;

        // Data
        private static string[] data;

        // Variables for error handling
        private static bool errorRaised = false;
        private static string errorMessage = "";

        public static void ReadFile()
        {
            // Path to read from
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assessment1Data.txt");

            // If file doesn't exist - return
            if (!File.Exists(path))
            {
                errorRaised = true;
                errorMessage += "ERROR: Input file doesn't exist\n";
                return;
            }

            // Read content from file
            data = File.ReadAllLines(path);
        }

        public static void WriteFile(List<Party> parties)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assessment1TestResults.txt");

            // If file already exists delete it
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            List<string> dataToWrite = new List<string>();

            // If run in debug mode - write errors in console and on top of file
            #if DEBUG
                Console.WriteLine(errorMessage);
                if (errorRaised) dataToWrite.Add(errorMessage);
            #endif

            dataToWrite.Add(electionName);

            for (int i = 0; i < parties.Count; i++)
            {
                string partyInfo = parties[i].getDataAboutParty();
                if (partyInfo != "")
                {
                    dataToWrite.Add(partyInfo);
                }
            }

            File.WriteAllLines(path, dataToWrite);
        }

        public static List<Party> CreatePartiesFromData()
        {
            // Create lists of parties and their data
            List<Party> PartyList = new List<Party>();

            // Votes used for validation
            int votesTotal = 0;

            // Set data about election
            try
            {
                electionName = data[0];
                seatsToAllocate = Int32.Parse(data[1]);
                electionTotalVotes = Int32.Parse(data[2]);
            }
            catch (FormatException)
            {
                seatsToAllocate = 0;
                electionTotalVotes = 0;
            }

            if (seatsToAllocate < 0 || electionTotalVotes < 0)
            {
                errorRaised = true;
                if (seatsToAllocate < 0)
                {
                    errorMessage += "ERROR: MEP seats to allocate are less than 0!\n";
                }
                else
                {
                    errorMessage += "ERROR: Total election votes are less than 0\n";
                }
            }

            // Parse Parties data
            for (int line = 3; line < data.Length; line++)
            {
                // Get data about party
                string[] currentLine = data[line].Split(',');

                // Check the name of party
                string nameOfParty = currentLine[0].ToString();

                if (Regex.IsMatch(nameOfParty, @"^[a-zA-Z ]+$"))
                {
                    int partyVotes;

                    try
                    {
                        partyVotes = Int32.Parse(currentLine[1]);
                    }
                    catch (FormatException)
                    {
                        partyVotes = 0;
                    }

                    if (partyVotes < 0)
                    {
                        errorRaised = true;
                        errorMessage += $"ERROR: Party `{nameOfParty}` has votes less than 0!\n";
                    }

                    List<string> seatNames = new List<string>();

                    // Add party votes to all votes count
                    votesTotal += partyVotes;

                    // Add seats name
                    for (int seatIndex = 2; seatIndex < currentLine.Length; seatIndex++)
                    {
                        string seatName = currentLine[seatIndex].TrimEnd(' ',';');
                        if (seatName == "\0" || String.IsNullOrEmpty(seatName))
                        {
                            errorRaised = true;
                            errorMessage += $"ERROR: Party {nameOfParty} has Seat(-s) without name!\n";
                        }
                        else
                        {
                            seatNames.Add(seatName);
                        }
                    }

                    // Construct new party
                    Party partyObject = new Party(nameOfParty, partyVotes, seatNames);
                    PartyList.Add(partyObject);
                }
            }

            // Validate number of votes
            ValidateVotesCount(votesTotal);
            return PartyList;
        }

        private static void ValidateVotesCount(int votesTotal)
        {
            if (votesTotal != electionTotalVotes)
            {
                errorRaised = true;
                errorMessage += "ERROR: Total election votes are not equal with total parties votes!\n";
            }
        }
    }
}