using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_11
{
    /// <summary>
    /// --- Day 11: Dumbo Octopus ---
    /// </summary>
    public class Solution : ISolution
    {
        private class Grid
        {
            private readonly static int SIZE = 5;

            private int[,] grid;

            public Grid(List<string> lines)
            {
                grid = new int[SIZE, SIZE];

                for (var x = 0; x < SIZE; ++x)
                {
                    for (var y = 0; y < SIZE; ++y)
                    {
                        grid[x, y] = lines[y][x] - '0';
                    }
                }
            }

            public void Step()
            {
                Print();

                for (var x = 0; x < SIZE; ++x)
                {
                    for (var y = 0; y < SIZE; ++y)
                    {
                        grid[x, y] += 1;
                    }
                }

                var willFlash = new List<(int X, int Y)>();
                var didFlashed = new List<(int X, int Y)>();

                while (true)
                {
                    for (var x = 0; x < SIZE; ++x)
                    {
                        for (var y = 0; y < SIZE; ++y)
                        {
                            if (grid[x, y] >= 9)
                            {
                                willFlash.Add((x, y));
                            }
                        }
                    }

                    if (willFlash.Count == 0)
                    {
                        break;
                    }

                    var atLeastOneFlashed = false;

                    foreach (var (X, Y) in willFlash)
                    {
                        if (!didFlashed.Contains((X, Y)))
                        {
                            Flash(X, Y);
                            didFlashed.Add((X, Y));
                            atLeastOneFlashed = true;
                        }
                    }

                    if (atLeastOneFlashed)
                    {
                        break;
                    }

                    willFlash.Clear();
                }

                for (var x = 0; x < SIZE; ++x)
                {
                    for (var y = 0; y < SIZE; ++y)
                    {
                        if (grid[x, y] >= 9)
                        {
                            grid[x, y] = 0;
                        }
                    }
                }
            }

            private void Flash(int x, int y)
            {
                var steps = new List<(int X, int Y)> { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

                foreach (var (X, Y) in steps)
                {
                    var xx = x + X;
                    var yy = y + Y;

                    if (xx >= 0 && xx < SIZE && yy >= 0 && yy < SIZE)
                    {
                        grid[x, y] += 1;
                    }
                }
            }

            public void Print()
            {
                for (var y = 0; y < SIZE; ++y)
                {
                    var sb = new StringBuilder();

                    for (var x = 0; x < SIZE; ++x)
                    {
                        sb.Append(grid[x, y]); 
                    }

                    Console.WriteLine(sb.ToString());
                }

                Console.WriteLine();
            }
        }

        public (string PartOne, string PartTwo) Run()
        {
            var lines = File.ReadAllLines(@"Day-11/Input.txt").ToList();

            var grid = new Grid(lines);

            grid.Step();

            grid.Print();

            return ("", "");
        }
    }
}
