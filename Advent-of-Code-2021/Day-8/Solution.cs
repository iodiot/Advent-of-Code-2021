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

            return (counter.ToString(), "");
        }
    }
}
