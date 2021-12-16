using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_15
{
    public class Solution : ISolution
    {
        private struct Position
        {
            public int X;
            public int Y;

            public Position(int x, int y)
            {
                X = x;
                Y = y;
            }

            public bool InRectangle(int side)
            {
                return X >= 0 && X < side && Y >= 0 && Y < side;
            }
        }

        private class Node
        {
            public int Weight = -1;
            public Node PrevNode = null;

            public Node(int weight, Node prevNode)
            {
                Weight = weight;
                PrevNode = prevNode;
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

            return (
                CountRiskLevel(cave, size, stretchFactor: 1).ToString(),
                CountRiskLevel(cave, size, stretchFactor: 5).ToString()
                );

            // 2837 -- high
        }

        private static int CountRiskLevel(int[,] cave, int size, int stretchFactor = 1)
        {
            var steps = new List<Position> { new Position(-1, 0), new Position(+1, 0), new Position(0, -1), new Position(0, +1) };

            var nodes = new Dictionary<Position, Node>();

            var queue = new Queue<Position>();

            var startPosition = new Position(size * stretchFactor - 1, size * stretchFactor - 1);
            var endPosition = new Position(0, 0);

            queue.Enqueue(startPosition);
            nodes[startPosition] = new Node(0, null);

            while (queue.Count > 0)
            {
                var position = queue.Dequeue();
                var node = nodes[position];

                if (position.X == endPosition.X && position.Y == endPosition.Y)
                {
                    break;
                }

                foreach (var step in steps)
                {
                    var nextPosition = new Position(position.X + step.X, position.Y + step.Y);

                    if (!nextPosition.InRectangle(size * stretchFactor))
                    {
                        continue;
                    }

                    var thisRisk = cave[nextPosition.X % size, nextPosition.Y % size];

                    // We are in stretched cave
                    if (!nextPosition.InRectangle(size))
                    {
                        var moreRisk = nextPosition.X / size + nextPosition.Y / size;

                        thisRisk += moreRisk;

                        while (thisRisk > 9)
                        {
                            thisRisk -= 9;
                        }
                    }

                    var nextWeight = node.Weight + thisRisk;

                    if (!nodes.ContainsKey(nextPosition))
                    {
                        nodes[nextPosition] = new Node(nextWeight, node);
                        queue.Enqueue(nextPosition);
                    }
                    else if (nodes[nextPosition].Weight > nextWeight)
                    {
                        nodes[nextPosition] = new Node(nextWeight, node);
                    }
                }
            }

            return nodes[endPosition].Weight;
        }
      
    }
}
