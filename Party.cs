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
        public int MEPCount { get; set; }
        public int MaxMEPCount { get; private set; }

        public Party(string partyName, int numberOfVotes, List<string> seatsName)
        {
            PartyName = partyName;
            NumberOfVotes = numberOfVotes;
            SeatsName = seatsName;
            MaxMEPCount = SeatsName.Count;
        }

        ~Party() { }

        private void format()
        {
            for (int i=0;i< MaxMEPCount; i++)
            {
                Console.WriteLine(SeatsName[i]);
            }
        }
    }
}
