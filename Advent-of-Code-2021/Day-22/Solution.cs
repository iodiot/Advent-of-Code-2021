using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2021.Day_22
{
    /// <summary>
    /// --- Day 22: Reactor Reboot ---
    /// </summary>
    public class Solution : ISolution
    {
        private class Cube
        {
            public int X0, Y0, Z0;
            public int X1, Y1, Z1;
            public int TotalPoints => (X1 - X0 + 1) * (Y1 - Y0 + 1) * (Z1 - Z0 + 1);

            public static Cube Zero = new(0, 0, 0, 0, 0, 0);

            public Cube(List<int> numbers)
            {
                X0 = numbers[0];
                X1 = numbers[1];
                Y0 = numbers[2];
                Y1 = numbers[3];
                Z0 = numbers[4];
                Z1 = numbers[5];
            }

            public Cube(int x0, int x1, int y0, int y1, int z0, int z1)
            {
                X0 = x0;
                X1 = x1;
                Y0 = y0;
                Y1 = y1;
                Z0 = z0;
                Z1 = z1;
            }

            public static Cube Identity(int scale)
            {
                return new Cube(-scale, scale, -scale, scale, -scale, scale);
            }
            
            public Cube Intersect(Cube other)
            {
                var x = GetOverlap(X0, X1, other.X0, other.X1);
                var y = GetOverlap(Y0, Y1, other.Y0, other.Y1);
                var z = GetOverlap(Z0, Z1, other.Z0, other.Z1);

                if (!x.Overlapped || !y.Overlapped || !z.Overlapped)
                {
                    return Zero;
                }

                return new Cube(x.From, x.To, y.From, y.To, z.From, z.To);
            }

            private static (int From, int To, bool Overlapped) GetOverlap(int from1, int to1, int from2, int to2)
            {
                return (Math.Max(from1, from2), Math.Min(to1, to2), Math.Max(from1, from2) < Math.Min(to1, to2));
            }

            public void DebugPrint()
            {
                Console.WriteLine($"x={ X0 }..{ X1 }, y={ Y0 }..{ Y1 }, z={ Z0 }..{ Z1 }");
            }
        }

        public (string PartOne, string PartTwo) Run()
        {
            var lines = File.ReadAllLines(@"Day-22/Input.txt");

            var toAdd = new List<Cube>();
            var toSubtract = new List<Cube>();

            foreach (var line in lines)
            {
                var numbers = Regex.Matches(line, "-?[0-9]+").Select(match => Convert.ToInt32(match.ToString())).ToList();
                var state = line.Split(' ')[0] == "on";

                var cube = new Cube(numbers);

                foreach (var other in toAdd)
                {
                    var inter = cube.Intersect(other);

                    if (inter != Cube.Zero)
                    {
                        toSubtract.Add(inter);
                    }
                }

                if (state)
                {
                    toAdd.Add(cube);
                }

                if (!state)
                {
                    foreach (var other in toSubtract)
                    {
                        var inter = cube.Intersect(other);

                        if (inter != Cube.Zero)
                        {
                            toAdd.Add(inter);
                        }
                    }
                }
            }

            var total = 0;

            foreach (var cube in toAdd)
            {
                total += cube.TotalPoints;
            }

            foreach (var cube in toSubtract)
            {
               total -= cube.TotalPoints;
            }


            var cube1 = Cube.Identity(50);
            var cube2 = new Cube(25, 75, 25, 75, 25, 75);
            var cube3 = new Cube(49, 60, 49, 60, 49, 60);

            cube1.Intersect(cube2).DebugPrint();
            cube1.Intersect(cube3).DebugPrint();


            return (total.ToString(), "");
        }
    }
}
