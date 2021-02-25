using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionSoft
{
    class Calculator
    {
        private Party winningParty;

        private void Calculate(int seatsToAllocate, Party[] parties)
        {
            for (int seat = seatsToAllocate; seat > 0; seat--)
            {
                winningParty = GetPartyWithMaxVotes(parties);
                IncreaseMEPCount(winningParty);
            }
        }

        private Party GetPartyWithMaxVotes(Party[] parties)
        {
            int maxVotes = -1;
            winningParty = null;

            foreach (Party party in parties)
            {
                if (party.numberOfVotes > maxVotes && party.MEPCount < party.MaxMEPCount)
                {
                    maxVotes = party.numberOfVotes;
                    winningParty = party;
                }
            }

            return winningParty;
        }

        private void IncreaseMEPCount(Party winningParty)
        {
            winningParty.MEPCount++;
        }
    }
}
