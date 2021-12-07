using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Advent_of_Code_2021.Day_6
{
    /// <summary>
    /// --- Day 6: Lanternfish ---
    /// </summary>
    public class Solution : ISolution
    {
        public (string PartOne, string PartTwo) Run()
        {
            var state = File.ReadAllText(@"Day-6/Input.txt").Split(',').Select(el => Convert.ToInt32(el)).ToArray();

            return (CountFish(state.ToArray(), 80).ToString(), CountFish(state.ToArray(), 256).ToString());
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
                    fishTotal += (CountFishHeirs(days - day - 1, new Dictionary<long, long>()) + 1) * fishToAdd;
                }
            }

            return fishTotal;
        }

        private static long CountFishHeirs(long daysLeft, Dictionary<long, long> memory)
        {
            long total = 0;

            daysLeft -= 9;

            if (daysLeft >= 0)
            {
                if (!memory.ContainsKey(daysLeft))
                {
                    memory[daysLeft] = CountFishHeirs(daysLeft, memory);
                }

                total += memory[daysLeft] + 1;
            }

            while (daysLeft >= 7)
            {
                daysLeft -= 7;

                if (!memory.ContainsKey(daysLeft))
                {
                    memory[daysLeft] = CountFishHeirs(daysLeft, memory);
                }

                total += memory[daysLeft] + 1;
            }

            return total;
        }
    }
}
