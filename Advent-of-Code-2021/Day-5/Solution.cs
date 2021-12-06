using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_5
{
    /// <summary>
    /// --- Day 5: Hydrothermal Venture ---
    /// </summary>
    public class Solution : ISolution
    {
        private struct Vent
        {
            public int X1, Y1, X2, Y2;
            public bool IsOrtho { get; private set; }

            public Vent(string line)
            {
                var tokens = line.Replace(" -> ", ",").Split(',').Select(el => Convert.ToInt32(el)).ToArray();
                X1 = tokens[0];
                Y1 = tokens[1];
                X2 = tokens[2];
                Y2 = tokens[3];

                IsOrtho = (X1 == X2) || (Y1 == Y2);
            }
        }

        public void Run()
        {
            var vents = File.ReadAllLines(@"Day-5/Input.txt").Select(line => new Vent(line)).ToList();

            Console.WriteLine($"First part: { CountDangerousPoints(vents, onlyOrtho: true) }");
            Console.WriteLine($"Second part: { CountDangerousPoints(vents, onlyOrtho: false) }");
        }

        private static int CountDangerousPoints(List<Vent> vents, bool onlyOrtho = true)
        {
            var grid = new Dictionary<(int x, int y), int>();

            foreach (var vent in vents)
            {
                if (!onlyOrtho || (onlyOrtho && vent.IsOrtho))
                {
                    BresenhamLine(vent.X1, vent.Y1, vent.X2, vent.Y2, grid);
                }
            }

            return grid.Values.Where(el => el > 1).Count();
        }

        private static void Swap(ref int left, ref int right)
        {
            var tmp = left;
            left = right;
            right = tmp;
        }

        private static void BresenhamLine(int x1, int y1, int x2, int y2, Dictionary<(int x, int y), int> grid)
        {
            var steep = Math.Abs(y2 - y1) > Math.Abs(x2 - x1);
            
            if (steep)
            {
                Swap(ref x1, ref y1);
                Swap(ref x2, ref y2);
            }

            if (x1 > x2)
            {
                Swap(ref x1, ref x2);
                Swap(ref y1, ref y2);
            }

            var dx = x2 - x1;
            var dy = Math.Abs(y2 - y1);
            var error = dx / 2;
            var ystep = (y1 < y2) ? 1 : -1;
            var y = y1;

            for (var x = x1; x <= x2; ++x)
            {
                var coord = (steep ? y : x, steep ? x : y);
                grid[coord] = grid.ContainsKey(coord) ? grid[coord] + 1 : 1; 

                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }
    }
}
