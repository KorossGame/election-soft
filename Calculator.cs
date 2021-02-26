using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ElectionSoft
{
    static class Calculator
    {
        private static Party winningParty;
        private static int counter = 0;

        public static void Calculate(int seatsToAllocate, List<Party> parties)
        {
            for (int seat = seatsToAllocate; seat > 0; seat--)
            {
                winningParty = GetPartyWithMaxVotes(parties);
                IncreaseMEPCount(winningParty);
            }

            foreach (Party party in parties)
            {
                Console.WriteLine(party.PartyName+"\t"+party.MEPCount);
            }
        }

        private static Party GetPartyWithMaxVotes(List<Party> parties)
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
            counter++;
            winningParty.MEPCount++;
            winningParty.NumberOfVotes /= (counter + 1);
        }
    }
}
