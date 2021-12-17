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

            return (
                RunFirstPart(lines.ToList()).ToString(), 
                RunSecondPart(lines.ToList()).ToString()
                );
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
            var mapping = new Dictionary<int, List<string>>();

            for (var i = 0; i < 10; ++i)
            {
                mapping[i] = new List<string>();
            }

            foreach (var line in lines)
            {
                var words = line.Split(" | ")[0].Split(' ').ToList();

                foreach (var word in words)
                {
                    var sorted = SortWord(word);

                    switch (word.Length)
                    {
                        case 2:
                            mapping[1].Add(sorted);
                            break;
                        case 3:
                            mapping[7].Add(sorted);
                            break;
                        case 4:
                            mapping[4].Add(sorted);
                            break;
                        case 5:
                            mapping[2].Add(sorted);
                            mapping[3].Add(sorted);
                            mapping[5].Add(sorted);
                            break;
                        case 6:
                            mapping[0].Add(sorted);
                            mapping[6].Add(sorted);
                            mapping[9].Add(sorted);
                            break;
                        case 7:
                            mapping[8].Add(sorted);
                            break;
                    }
                }

                int x = 0;
            }

            return 0;
        }
    }
}
