using System;
using System.Collections.Generic;

namespace ElectionSoft
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> a = new List<string> { "BP1" , "BP2" };
            List<string> b = new List<string> { "AP1" };

            Party c = new Party("Brexit Party", 452321, a);
            Party d = new Party("Liberal Democrats", 203989, b);
            Party e = new Party("Labour", 164682, a);
            Party f = new Party("Conservative", 126138, b);

            List<Party> par = new List<Party> { d, c, e, f };

            Calculator.Calculate(3, par);
        }
    }
}
