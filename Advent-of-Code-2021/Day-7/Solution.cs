using System;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_7
{
    /// <summary>
    /// --- Day 7: The Treachery of Whales ---
    /// </summary>
    public class Solution : ISolution
    {
        public (string PartOne, string PartTwo) Run()
        {
            var positions = File.ReadAllText(@"Day-7/Input.txt").Split(',').Select(el => Convert.ToInt32(el)).ToArray();

            return (
                CountOptimalFuel(positions.ToArray(), constantRate: true).ToString(), 
                CountOptimalFuel(positions.ToArray(), constantRate: false).ToString()
                );
        }

        private static int CountOptimalFuel(int[] positions, bool constantRate = true)
        {
            var minFuel = Int32.MaxValue;

            var min = positions.Min();
            var max = positions.Max();

            for (var align = min; align <= max; ++align)
            {
                var fuel = 0;

                foreach (var pos in positions)
                {
                    var step = Math.Abs(pos - align);

                    fuel += constantRate ? step : step * (step + 1) / 2;
                }

                minFuel = fuel < minFuel ? fuel : minFuel;
            }

            return minFuel;
        }
    }
}
