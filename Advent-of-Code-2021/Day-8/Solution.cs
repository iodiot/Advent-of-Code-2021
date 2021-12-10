using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_8
{
    /// <summary>
    /// --- Day 8: Seven Segment Search ---
    /// </summary>
    public class Solution : ISolution
    {
        public (string PartOne, string PartTwo) Run()
        {
            var lines = File.ReadAllLines(@"Day-8/Input.txt");

            return (RunFirstPart(lines.ToList()).ToString(), "");
        }

        private static int RunFirstPart(List<string> lines)
        {
            var counter = 0;

            foreach (var line in lines)
            {
                var words = line.Split(" | ")[1].Split(' ').ToList();

                foreach (var word in words)
                {
                    if (word.Length == 2 || word.Length == 3 || word.Length == 4 || word.Length == 7)
                    {
                        counter += 1;
                    }
                }

            }

            return counter;
        }

        private static string SortWord(string str)
        {
            return String.Concat(str.OrderBy(ch => ch));
        }

        private static int RunSecondPart(List<string> lines)
        {
            var mapping = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                var words = line.Split(" | ")[1].Split(' ').ToList();

                foreach (var word in words)
                {
                    var sorted = SortWord(word);

                    switch (word.Length)
                    {
                        case 2:
                            mapping[sorted] = 1;
                            break;
                        case 3:
                            mapping[sorted] = 7;
                            break;
                        case 4:
                            mapping[sorted] = 4;
                            break;
                        case 7:
                            mapping[sorted] = 8;
                            break;
                    }
                }
            }

            return 0;
        }
    }
}
