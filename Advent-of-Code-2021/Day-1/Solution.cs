using System;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_1
{
    /// <summary>
    /// --- Day 1: Sonar Sweep ---
    /// </summary>
    public class Solution : ISolution
    {
        public (string PartOne, string PartTwo) Run()
        {
            var numbers = File.ReadAllLines(@"Day-1/Input.txt").Select(line => Convert.ToInt32(line)).ToList();

            var counter = 0;

            for (var i = 1; i < numbers.Count; ++i)
            {
                counter += (numbers[i] > numbers[i - 1]) ? 1 : 0;
            }

            var firstPart = counter.ToString();

            counter = 0;

            for (var i = 1; i < numbers.Count - 2; ++i)
            {
                var left = numbers[i] + numbers[i + 1] + numbers[i + 2];
                var right = numbers[i - 1] + numbers[i] + numbers[i + 1];
                counter += (left > right) ? 1 : 0;
            }

            return (firstPart, counter.ToString());
        }
    }
}
