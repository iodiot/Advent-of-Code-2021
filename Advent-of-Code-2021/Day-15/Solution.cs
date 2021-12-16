using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_15
{
    public class DijkstraCave
	{
        private class Position
        {
            public int X;
            public int Y;

            public Position(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private class PositionData
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

        private readonly int[,] cave;
        private readonly int size;
        private readonly Dictionary<Position, PositionData> positionDatas;

        public DijkstraCave(int[,] cave, int size)
		{
            this.cave = cave;
            this.size = size;

            positionDatas = new Dictionary<Position, PositionData>();
		}

        private int ComputeRiskLevel()
        {
            var r = FindShortesPath(new Position(0, 0), new Position(size - 1, size - 1));

            return 0;
        }

        private int FindShortesPath(Position start, Position end)
        {
            positionDatas[start] = new PositionData(start);

            var first = positionDatas[start];

            first.WeightSum = 0;

            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();

                if (current == null)
                {
                    break;
                }

                // SetSumToNextVertex(current);
            }


            return 0;
        }

        private static PositionData FindUnvisitedVertexWithMinSum()
        {
            var minValue = Int32.MaxValue;

            PositionData minPositionData = null;

            foreach (var i in infos)
            {
                if (i.IsUnvisited && i.EdgesWeightSum < minValue)
                {
                    minPositionData = i;
                    minValue = i.EdgesWeightSum;
                }
            }

            return minPositionData;
        }

        private static void SetSumToNextVertex(PositionData data)
        {
            data.IsUnvisited = false;

            foreach (var e in info.Vertex.Edges)
            {
                var nextInfo = GetVertexInfo(e.ConnectedVertex);
                var sum = info.EdgesWeightSum + e.EdgeWeight;
                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousVertex = info.Vertex;
                }
            }
        }
    }

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

            return (
                "",
                  ""
                );

            // 2837 -- high
        }

        
      
    }
}
