using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_4
{
    /// <summary>
    /// --- Day 4: Giant Squid ---
    /// </summary>
    public class Solution : ISolution
    {
        private class Board
        {
            public bool Wins { get; private set; }

            private readonly Dictionary<(int, int), bool> values = new();
            private readonly Dictionary<int, (int x, int y)> invValues = new();

            public void SetValue(int x, int y, int value)
            {
                values[(x, y)] = false;
                invValues[value] = (x, y);
            }

            public bool GetMarked(int x, int y)
            {
                return values[(x, y)];
            }

            public bool Play(int number)
            {
                if (invValues.ContainsKey(number))
                {
                    var coord = invValues[number];
                    values[coord] = true;

                   if (CheckRow(coord.y) || CheckColumn(coord.x))
                    {
                        Wins = true;
                        return true;
                    }
                }

                return false;
            }

            private bool CheckRow(int row)
            {
                for (var x = 0; x < 5; ++x)
                {
                    if (!GetMarked(x, row))
                    {
                        return false;
                    }
                }

                return true;
            }

            private bool CheckColumn(int column)
            {
                for (var y = 0; y < 5; ++y)
                {
                    if (!GetMarked(column, y))
                    {
                        return false;
                    }
                }

                return true;
            }

            public int CountSumOfUnmarked()
            {
                var sum = 0;

                foreach (var (number, coord) in invValues)
                {
                    if (!GetMarked(coord.x, coord.y))
                    {
                        sum += number;
                    }
                }

                return sum;
            }
        }

        public void Run()
        {
            var lines = File.ReadAllLines(@"Day-4/Input.txt");

            var numbers = lines[0].Split(',').Select(el => Convert.ToInt32(el)).ToList();

            var n = 2;

            var boards = new List<Board>();

            while (n < lines.Length)
            {
                var board = new Board();

                for (var y = 0; y < 5; ++y)
                {
                    var numbersInRow = lines[n + y].Replace("  ", " ").Trim(' ').Split(' ').Select(el => Convert.ToInt32(el)).ToArray();

                    for (var x = 0; x < 5; ++x)
                    {
                        board.SetValue(x, y, numbersInRow[x]);
                    }
                }

                boards.Add(board);

                n += 6;
            }

            Console.WriteLine($"First part: { PlayBingo(boards, numbers, letTheSquidWin: false) }");
            Console.WriteLine($"Second part: { PlayBingo(boards, numbers, letTheSquidWin: true) }");
        }

        private static int PlayBingo(List<Board> boards, List<int> numbers, bool letTheSquidWin = false)
        {
            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    var wins = board.Play(number);

                    if ((wins && !letTheSquidWin) || (letTheSquidWin && boards.All(board => board.Wins)))
                    {
                        return number * board.CountSumOfUnmarked();
                    }
                }
            }

            return -1;
        }
    }
}
