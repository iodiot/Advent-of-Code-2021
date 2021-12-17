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

            // 2145 -- low
            return (CalculateMaxY(area).ToString(), "");
        }

        private static int CalculateMaxY(Area area)
        {
            return (Math.Abs(area.MinY) - 1) * (Math.Abs(area.MinY)) / 2;
        }

        private static int _CalculateMaxY(Area area)
        {
            var maxY = 0;

            var bottom = 72;
            var top = 132;

            for (var startVel = 1; startVel < top; ++startVel)
            {
                for (int vel = startVel, y = 0; ; ++vel)
                {
                    y += vel;

                    if (y < top)
                    {
                        if (y >= bottom)
                        {
                            maxY = startVel * (startVel + 1) / 2;
                            Console.WriteLine($"vy={ startVel } max={ maxY } y={ y }");
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return maxY;
        }

        private static (bool PassedArea, int MaxY) ModelProbe(int vx, int vy, Area area)
        {
            var ovx = vx;
            var ovy = vy;

            var x = 0;
            var y = 0;

            var maxY = y;

            var passedArea = false;

            while (true)
            {
                if (area.ContainsPoint(x, y))
                {
                    passedArea = true;
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

            Console.WriteLine($"{ ovx } { ovy } { passedArea } { maxY }");

            return (passedArea, maxY);
        }
    }
}
