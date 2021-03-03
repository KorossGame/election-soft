using System;
using System.Collections.Generic;

namespace ElectionSoft
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> a = new List<string> { "MEP1" , "MEP2" };

            Party c = new Party("Brexit Party", 200000, a);
            Party d = new Party("Conservative", 100000, a);

            List<Party> par = new List<Party> { c, d };

            // Calculate
            Calculator.Calculate(2, par);

            // Output
            for (int i=0; i<par.Count; i++)
            {
                string partyInfo = par[i].getDataAboutParty();
                if (partyInfo != "")
                {
                    if (i == par.Count - 1)
                    {
                        Console.WriteLine(partyInfo);
                    }
                    else
                    {
                        Console.WriteLine(partyInfo);
                    }
                }
            }
        }
    }
}
