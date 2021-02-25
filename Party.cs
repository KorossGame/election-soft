using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionSoft
{
    class Party
    {
        public string PartyName { get; private set; }
        public int NumberOfVotes { get; private set; }
        public List<string> SeatsName { get; private set; }
        public int MEPCount { get; set; }
        public int MaxMEPCount { get; private set; }
    }
}
