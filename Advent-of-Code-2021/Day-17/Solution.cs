using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2021.Day_17
{
    /// <summary>
    /// --- Day 17: Trick Shot ---
    /// Second part is pure bruteforce.
    /// Tried different approaches. 
    /// Actualy the task is to find when sequence vx + (vx + 1) ... + (vx + n) is within area under some n.
    /// I didn't find the closed formula.
    /// </summary>
    public class Solution : ISolution
    {
        private struct Area
        {
            public int MinX, MaxX, MinY, MaxY;

            public bool ContainsPoint(int x, int y)
            {
                return x >= MinX && x <= MaxX && y >= MinY && y <= MaxY;
            }
        }

        public (string PartOne, string PartTwo) Run()
        {
            var input = File.ReadAllText(@"Day-17/Input.txt");

            var numbers = Regex.Matches(input, "-?[0-9]+").Select(match => Convert.ToInt32(match.ToString())).ToList();

            var area = new Area() {MinX = numbers[0], MaxX = numbers[1], MinY = numbers[2], MaxY = numbers[3]};

            return (CalculateMaxY(area).ToString(), CalculateOnTargetLaunches(area).ToString());
        }

        private static int CalculateMaxY(Area area)
        {
            return (Math.Abs(area.MinY) - 1) * (Math.Abs(area.MinY)) / 2;
        }

        private static int CalculateOnTargetLaunches(Area area)
        {
            var count = 0;

            for (var vx = 0; vx <= area.MaxX; ++vx)
            {
                for (var vy = -500; vy < 500; ++vy)
                {
                    count += ModelProbe(vx, vy, area) == true ? 1 : 0;
                }
            }

            return count;
        }

        private static bool ModelProbe(int vx, int vy, Area area)
        {
            var x = 0;
            var y = 0;

            var maxY = y;

            while (true)
            {
                if (area.ContainsPoint(x, y))
                {
                    return true;
                }

                x += vx;
                y += vy;
                vx += -Math.Sign(vx);
                vy -= 1;

                maxY = Math.Max(maxY, y);

                if (vy < 0 && area.MinY > y)
                {
                    break;
                }
            }

            return false;
        }
    }
}
