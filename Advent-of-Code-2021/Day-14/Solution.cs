using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_14
{
    /// <summary>
    /// --- Day 14: Extended Polymerization ---
    /// </summary>
    public class Solution : ISolution
    {
        public (string PartOne, string PartTwo) Run()
        {
            var lines = File.ReadAllLines(@"Day-14/Input.txt");

            var template = lines[0];

            var rules = new Dictionary<string, char>();

            foreach (var line in lines.Skip(2))
            {
                var tokens = line.Split(" -> ");
                rules[tokens[0]] = tokens[1][0];
            }

            var rate = new Dictionary<char, int>();

            foreach (var element in template)
            {
                rate[element] = rate.ContainsKey(element) ? rate[element] + 1 : 1;
            }

            return ("", "");
        }
    }
}
