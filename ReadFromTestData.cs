using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ElectionSoft
{


    static class ReadWriteClass
    {
        public static void ReadFile()
        {
            /*
            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Assessment1Data.txt");
            Console.WriteLine(path);
            var lines = File.ReadLines(path);
            var vals = new List<string>();
            var PartyData = new List<string>();
            List<Data> PartyList = new List<Data>();
            foreach (var line in lines)
            {
                string[] currentLine = line.Split(',');
                vals.AddRange(currentLine);

                foreach (char i in line)
                {
                    string a = line[0].ToString();
                    if (Regex.IsMatch(a, @"^[a-zA-Z]+$"))
                    {
                        PartyData.Add(line);
                        break;
                    }
                }
            }

            foreach (var Party in PartyData)
            {
                var elements = new List<string>();
                string[] currentParty = Party.Split(',');
                elements.AddRange(currentParty);
                string PartyName = elements[0];
                int PartyVotes = Int32.Parse(elements[1]);
                var SeatNames = new List<string>();
                SeatNames.AddRange(elements.GetRange(2, elements.Count-2));
                Data Final = new Data(PartyName, PartyVotes, SeatNames);
                PartyList.Add(Final);
            }

            foreach (var data in PartyList)
            {
                Console.WriteLine(data.PartyName);
                Console.WriteLine(data.NumberOfVotes);
                data.SeatsName.ForEach(Console.WriteLine);
                Console.WriteLine("------------------------");
            }
            */
        }

        public static void WriteFile(List<Party> par) 
        {

            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Assessment1TestResults.txt");
            for (int i = 0; i < par.Count; i++)
            {
                string partyInfo = par[i].getDataAboutParty();
                if (partyInfo != "")
                {
                    File.AppendAllText(path, partyInfo);
                }

            }
            
        }
    }
    
}