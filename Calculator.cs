using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ElectionSoft
{
    static class Calculator
    {
        private static Random random = new Random(Guid.NewGuid().GetHashCode());
        private static List<Party> maxVotesParties = new List<Party>();
        private static Party winningParty;
        private static int counter = 0;

        public static void Calculate(int seatsToAllocate, List<Party> parties)
        {
            if (parties.Count == 0) return;

            for (int seat = seatsToAllocate; seat > 0; seat--)
            {
                winningParty = GetPartyWithMaxVotes(parties);
                IncreaseMEPCount(winningParty);
            }
        }

        private static Party GetPartyWithMaxVotes(List<Party> parties)
        {
            // Clear list from previous function call
            maxVotesParties.Clear();

            // Reset max votes and winning party
            int maxVotes = -1;
            winningParty = null;

            // Save parties with max votes
            foreach (Party party in parties)
            {
                if (party.NumberOfVotes > maxVotes && party.MEPCount < party.MaxMEPCount)
                {
                    // As we got new max vote count - set new max votes, clear the list and add leading party
                    maxVotes = party.NumberOfVotes;
                    maxVotesParties.Clear();
                    maxVotesParties.Add(party);
                }
                else if (party.NumberOfVotes == maxVotes && party.MEPCount < party.MaxMEPCount)
                {
                    // As we have equal number of votes we add each party to list
                    maxVotesParties.Add(party);
                }
            }
            
            // Choose random party from list of parties with max votes count
            int winningIndex = random.Next(maxVotesParties.Count - 1);
            winningParty = maxVotesParties[winningIndex];

            return winningParty;
        }

        private static void IncreaseMEPCount(Party winningParty)
        {
            // Increase counter of MEP allocated
            counter++;

            // Increase MEP count of particullar party
            winningParty.MEPCount++;

            // Set new number of votes of winning party
            winningParty.NumberOfVotes = winningParty.NumberOfVotes * winningParty.MEPCount / (counter + 1);
        }
    }
}
