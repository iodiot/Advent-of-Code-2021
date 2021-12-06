using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_6
{
    /// <summary>
    /// --- Day 6: Lanternfish ---
    /// </summary>
    public class Solution : ISolution
    {
        public void Run()
        {
            var state = File.ReadAllText(@"Day-6/Input.txt").Split(',').Select(el => Convert.ToInt32(el)).ToArray();

            Console.WriteLine($"First part: { CountFish(state.ToArray(), 80) }");
            Console.WriteLine($"Second part: { CountFish(state.ToArray(), 256) }");
        }

        private static long CountFish(int[] state, int days)
        {
            long fishTotal = state.Length;

            for (var day = 0; day < days; ++day)
            {
                long fishToAdd = 0;

                for (var i = 0; i < state.Length; ++i)
                {
                    state[i] = state[i] - 1;

                    if (state[i] < 0)
                    {
                        state[i] = 6;
                        fishToAdd += 1;
                    }
                }

                if (fishToAdd > 0)
                {
                    fishTotal += fishToAdd;

                    var daysLeft = days - day - 1;

                    fishTotal += CountFishHeirs(fishToAdd, daysLeft);
                }

                Console.WriteLine(day);
            }

            return fishTotal;
        }

        private static long CountFishHeirs(long fishToAdd, long daysLeft)
        {
            long total = 0;

            daysLeft -= 9;

            if (daysLeft >= 0)
            {
                total += CountFishHeirs(fishToAdd, daysLeft) + fishToAdd;
            }

            while (daysLeft >= 7)
            {
                daysLeft -= 7;
                total += CountFishHeirs(fishToAdd, daysLeft) + fishToAdd;
            }

            return total;
        }
    }
}
