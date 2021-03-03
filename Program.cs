using System;
using System.Collections.Generic;

namespace ElectionSoft
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read data from file
            ReadWriteClass.ReadFile();

            // Create new list of Parties
            List<Party> parties = ReadWriteClass.CreatePartiesFromData();

            // Calculate
            Calculator.Calculate(ReadWriteClass.seatsToAllocate, parties);

            // Output
            ReadWriteClass.WriteFile(parties);
        }
    }
}
