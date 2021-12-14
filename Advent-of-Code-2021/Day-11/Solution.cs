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
            public readonly int Size;

            private readonly int[,] grid;

            public Grid(List<string> lines)
            {
                Size = lines.Count;

                grid = new int[Size, Size];

                for (var x = 0; x < Size; ++x)
                {
                    for (var y = 0; y < Size; ++y)
                    {
                        grid[x, y] = lines[y][x] - '0';
                    }
                }
            }

            public int Step()
            {
                for (var x = 0; x < Size; ++x)
                {
                    for (var y = 0; y < Size; ++y)
                    {
                        grid[x, y] += 1;
                    }
                }

                var flashed = new bool[Size, Size];

                for (var x = 0; x < Size; ++x)
                {
                    for (var y = 0; y < Size; ++y)
                    {
                        if (grid[x, y] > 9)
                        {
                            Flash(x, y, flashed);
                        }
                    }
                }

                var flashes = 0;

                for (var x = 0; x < Size; ++x)
                {
                    for (var y = 0; y < Size; ++y)
                    {
                        if (flashed[x, y])
                        {
                            grid[x, y] = 0;
                            flashes += 1;
                        }
                    }
                }

                return flashes;
            }

            private void Flash(int x, int y, bool[,] flashed)
            {
                if (flashed[x, y])
                {
                    return;
                }

                flashed[x, y] = true;

                var steps = new List<(int X, int Y)> { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

                foreach (var (X, Y) in steps)
                {
                    var dx = x + X;
                    var dy = y + Y;

                    if (dx >= 0 && dx < Size && dy >= 0 && dy < Size)
                    {
                        grid[dx, dy] += 1;

                        if (grid[dx, dy] > 9)
                        {
                            Flash(dx, dy, flashed);
                        }
                    }
                }
            }
        }

        public (string PartOne, string PartTwo) Run()
        {
            var lines = File.ReadAllLines(@"Day-11/Input.txt").ToList();

            var grid = new Grid(lines);

            var totalFlashesAfter100Steps = 0;
            var firstStepWhenAllFlash = -1;

            var step = 0;

            while (true)
            {
                var flashes = grid.Step();

                if (step < 100)
                {
                    totalFlashesAfter100Steps += flashes;
                }

                if (flashes == grid.Size * grid.Size)
                {
                    firstStepWhenAllFlash = step + 1;
                    break;
                }

                step += 1;
            }

            return (totalFlashesAfter100Steps.ToString(), firstStepWhenAllFlash.ToString());
        }
    }
}
