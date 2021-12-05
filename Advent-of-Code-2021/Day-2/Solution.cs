using System;
using System.IO;

namespace Advent_of_Code_2021.Day_2
{
    /// <summary>
    /// --- Day 2: Dive! ---
    /// </summary>
    public class Solution : ISolution
    {
        private struct Position
        {
            public int Hor;
            public int Depth;
            public int Aim;
        }

        public void Run()
        {
            var lines = File.ReadAllLines(@"Day-2/Input.txt");

            var pos1 = new Position();
            var pos2 = new Position();

            foreach (var line in lines)
            {
                var tokens = line.Split(" ");

                var units = Convert.ToInt32(tokens[1]);

                switch (tokens[0])
                {
                    case "forward":
                        pos1.Hor += units;
                        pos2.Hor += units;
                        pos2.Depth += pos2.Aim * units;
                        break;
                    case "down":
                        pos1.Depth += units;
                        pos2.Aim += units;
                        break;
                    case "up":
                        pos1.Depth -= units;
                        pos2.Aim -= units;
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine($"First part: { pos1.Hor * pos1.Depth }");
            Console.WriteLine($"Second part: { pos2.Hor * pos2.Depth }");
        }
    }
}
