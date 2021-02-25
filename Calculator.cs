using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionSoft
{
    static class Calculator
    {
        private static Party winningParty;

        public static void Calculate(int seatsToAllocate, Party[] parties)
        {
            for (int seat = seatsToAllocate; seat > 0; seat--)
            {
                winningParty = GetPartyWithMaxVotes(parties);
                IncreaseMEPCount(winningParty);
            }
        }

        private static Party GetPartyWithMaxVotes(Party[] parties)
        {
            int maxVotes = -1;
            winningParty = null;

            foreach (Party party in parties)
            {
                if (party.NumberOfVotes > maxVotes && party.MEPCount < party.MaxMEPCount)
                {
                    maxVotes = party.NumberOfVotes;
                    winningParty = party;
                }
            }

            return winningParty;
        }

        private static void IncreaseMEPCount(Party winningParty)
        {
            winningParty.MEPCount++;
        }
    }
}
