using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_9
{
    /// <summary>
    /// --- Day 9: Smoke Basin ---
    /// </summary>
    public class Solution : ISolution
    {
        private struct Caves
        {
            public int Width { get { return map[0].Length; } }
            public int Height { get { return map.Count; } }

            private readonly List<string> map;
            private readonly Dictionary<(int X, int Y), int> basins;

            private static readonly List<(int X, int Y)> offsets = new() { (-1, 0), (1, 0), (0, -1), (0, 1) };

            public Caves(List<string> lines)
            {
                map = lines.ToList();
                basins = new();
            }

            public int Get(int x, int y)
            {
                return x < 0 || x >= Width || y < 0 || y >= Height ? Int32.MaxValue : map[y][x] - '0';
            }

            public bool IsLowPoint(int x, int y)
            {
                var v = Get(x, y);

                foreach (var (X, Y) in offsets)
                {
                    if (Get(x + X, y + Y) <= v)
                    {
                        return false;
                    }
                }

                return true;
            }

            public bool FillBasin(int x, int y, int hash)
            {
                if (Get(x, y) >= 9 || basins.ContainsKey((x, y)))
                {
                    return false;
                }

                var queue = new Queue<(int X, int Y)>();

                queue.Enqueue((x, y));

                while (queue.Count > 0)
                {
                    var pos = queue.Dequeue();

                    if (Get(pos.X, pos.Y) >= 9 || basins.ContainsKey(pos))
                    {
                        continue;
                    }

                    basins[pos] = hash;

                    foreach (var (X, Y) in offsets)
                    {
                        queue.Enqueue((pos.X + X, pos.Y + Y));
                    }
                }

                return true;
            }

            public int CountTopBasins()
            {
                var invBasins = new Dictionary<int, int>();

                foreach (var val in basins.Values)
                {
                    invBasins[val] = invBasins.ContainsKey(val) ? invBasins[val] + 1 : 1;
                }

                var values = invBasins.Values.ToList();
                values.Sort();
                values.Reverse();

                return values[0] * values[1] * values[2];
            }
        }

        public (string PartOne, string PartTwo) Run()
        {
            var lines = File.ReadAllLines(@"Day-9/Input.txt").ToList();

            return (RunPartOne(lines.ToList()).ToString(), RunPartTwo(lines.ToList()).ToString());
        }

        private static int RunPartOne(List<string> lines)
        {
            var riskLevel = 0;

            var caves = new Caves(lines);

            for (var x = 0; x < caves.Width; ++x)
            {
                for (var y = 0; y < caves.Height; ++y)
                {
                    if (caves.IsLowPoint(x, y))
                    {
                        riskLevel += caves.Get(x, y) + 1;
                    }
                }
            }

            return riskLevel;
        }

        private static int RunPartTwo(List<string> lines)
        {
            var caves = new Caves(lines);

            var hash = 100;

            for (var x = 0; x < caves.Width; ++x)
            {
                for (var y = 0; y < caves.Height; ++y)
                {
                    if (caves.FillBasin(x, y, hash))
                    {
                        hash += 1;
                    }
                }
            }

            return caves.CountTopBasins();
        }
    }
}
