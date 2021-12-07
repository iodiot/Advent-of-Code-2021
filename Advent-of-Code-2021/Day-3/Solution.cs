using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_3
{
    /// <summary>
    /// --- Day 3: Binary Diagnostic ---
    /// </summary>
    public class Solution : ISolution
    {
        public (string PartOne, string PartTwo) Run()
        {
            RunFirstPart();
            RunSecondPart();

            return (RunFirstPart().ToString(), RunSecondPart().ToString());
        }

        private static int RunFirstPart()
        {
            var lines = File.ReadAllLines(@"Day-3/Input.txt");

            var gammaRate = new StringBuilder();
            var epsilonRate = new StringBuilder();

            for (var x = 0; x < lines[0].Length; ++x)
            {
                var sb = new StringBuilder();

                for (var y = 0; y < lines.Length; ++y)
                {
                    sb.Append(lines[y][x]);
                }

                var mostCommonIsOne = sb.ToString().Count(ch => ch == '1') > sb.Length / 2;

                gammaRate.Append(mostCommonIsOne ? '1' : '0');
                epsilonRate.Append(mostCommonIsOne ? '0' : '1');
            }

            var gammaRateInt = Convert.ToInt32(gammaRate.ToString(), 2);
            var epsilonRateInt = Convert.ToInt32(epsilonRate.ToString(), 2);

            return gammaRateInt * epsilonRateInt;
        }

        private static int RunSecondPart()
        {
            var lines = File.ReadAllLines(@"Day-3/Input.txt").ToList();

            return DetermineRating(lines.ToList(), true) * DetermineRating(lines.ToList(), false);
        }

        private static int DetermineRating(List<string> numbers, bool findOxygenGeneratorLevel = true)
        {
            var x = 0;

            while (numbers.Count != 1)
            {
                var sb = new StringBuilder();

                for (var y = 0; y < numbers.Count; ++y)
                {
                    sb.Append(numbers[y][x]);
                }

                char bit;

                var ones = sb.ToString().Count(ch => ch == '1');
                var zeros = sb.ToString().Count(ch => ch == '0');

                if (findOxygenGeneratorLevel)
                {
                    bit = (ones >= zeros) ? '1' : '0';
                }
                else
                {
                    bit = (zeros <= ones) ? '0' : '1';
                }

                numbers = numbers.Where(line => line[x] == bit).ToList();

                ++x;
            }

            return Convert.ToInt32(numbers[0], 2);
        }
    }
}
