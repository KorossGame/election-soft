using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionSoft
{
    class Party
    {
        public string PartyName { get; private set; }
        public int NumberOfVotes { get; set; }
        public List<string> SeatsName { get; private set; }
        public int MEPCount { get; set; } = 0;
        public int MaxMEPCount { get; private set; }

        private string output;

        public Party(string partyName, int numberOfVotes, List<string> seatsName)
        {
            PartyName = partyName;
            NumberOfVotes = numberOfVotes;
            SeatsName = seatsName;
            MaxMEPCount = SeatsName.Count;
        }

        ~Party() { }

        public string getDataAboutParty()
        {
            if (MEPCount <= 0) return "";

            // Add party name
            output = PartyName;

            // Add allocated seats name
            for (int i = 0; i < MEPCount; i++)
            {
                output += ',' + SeatsName[i];
            }

            output += ";";

            return output;
        }
    }
}
