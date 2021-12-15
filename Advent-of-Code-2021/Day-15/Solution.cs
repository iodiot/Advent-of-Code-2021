using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_15
{
    public class Solution : ISolution
    {
        public (string PartOne, string PartTwo) Run()
        {
            var lines = File.ReadAllLines(@"Day-15/Input.txt");

            var size = lines.Length;

            var cave = new int[size, size];

            for (var y = 0; y < size; ++y)
            {
                for (var x = 0; x < size; ++x)
                {
                    cave[x, y] = lines[y][x] - '0';
                }
            }

            var times = 5;

            var stretchedCave = StretchCave(cave, size, times);

            return (
                ComputeRiskLevel(size - 1, size - 1, cave, new Dictionary<(int, int), int>()).ToString(),
                ComputeRiskLevel(size * times - 1, size * times - 1, stretchedCave, new Dictionary<(int, int), int>()).ToString()
                );

            // 2837 -- high
        }

        private static int[,] StretchCave(int[,] cave, int size, int times)
        {
            var newCave = new int[size * times, size * times];

            for (var gx = 0; gx < times; ++gx)
            {
                for (var gy = 0; gy < times; ++gy)
                {
                    var moreRisk = gx + gy;

                    for (var x = 0; x < size; ++x)
                    {
                        for (var y = 0; y < size; ++y)
                        {
                            var risk = cave[x, y] + moreRisk;

                            while (risk > 9)
                            {
                                risk -= 9;
                            }

                            newCave[gx * size + x, gy * size + y] = risk;
                        }
                    }
                }
            }

            //for (var y = 0; y < size * times; ++y)
            //{
            //    var sb = new StringBuilder();
            //    for (var x = 0; x < size * times; ++x)
            //    {
            //        sb.Append(newCave[x, y]);
            //    }
            //    Console.WriteLine(sb.ToString());
            //}

            return newCave;
        }

        private int ComputeRiskLevel(int x, int y, int[,] cave, Dictionary<(int X, int Y), int> dp)
        {
            if (x == 0 && y == 0)
            {
                return 0;
            }

            if (dp.ContainsKey((x, y)))
            {
                return dp[(x, y)];
            }

            var options = new List<(int X, int Y)> { (x - 1, y), (x, y - 1) };

            var minRiskLevel = Int32.MaxValue;

            foreach (var (X, Y) in options)
            {
                if (X >= 0 && Y >= 0)
                {
                    minRiskLevel = Math.Min(minRiskLevel, ComputeRiskLevel(X, Y, cave, dp));
                }
            }

            dp[(x, y)] = cave[x, y] + minRiskLevel;

            return dp[(x, y)];
        }
    }
}
