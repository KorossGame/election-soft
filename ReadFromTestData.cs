using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ReadFromTestData
{

    public class Data
    {
        public string PartyName { get; set; }
        public int NumberOfVotes { get; set; }
        public List<string> SeatsName { get; set; }

        public Data(string PartyName, int NumberOfVotes, List<string> SeatsName)
        {
            this.PartyName = PartyName;
            this.NumberOfVotes = NumberOfVotes;
            this.SeatsName = SeatsName;
        }
    }

    static class Program
    {
        public static void ReadFile()
        {
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
        }
    }

    // run read class to test if code is working correctly
    // uncomment below
    static class Read
    {
        static void Main(string[] args)
        {
           // Program.ReadFile();
        }
    }
}