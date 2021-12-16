using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_15
{
    public class Solution : ISolution
    {
        public class Position
        {
            public int X;
            public int Y;

            public Position(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public class PositionData
        {
            public Position Position;
            public Position PrevPosition;
            public bool IsUnvisited;
            public int WeightSum;

            public PositionData(Position position)
            {
                Position = position;
                IsUnvisited = true;
                WeightSum = int.MaxValue;
                PrevPosition = null;
            }
        }

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

            return (
                ComputeRiskLevel(cave, size).ToString(),
                  ""
                );

            // 2837 -- high
        }

        private static int ComputeRiskLevel(int[,] cave, int size)
        {
            var r = FindShortesPath(new Position(0, 0), new Position(size - 1, size - 1));

            return 0;
        }

        private static int FindShortesPath(Position start, Position end)
        {
            var datas = new Dictionary<Position, PositionData>();

            return 0;
        }
    }
}
