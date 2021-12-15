using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_13
{
    /// <summary>
    /// --- Day 13: Transparent Origami ---
    /// </summary>
    /// <returns></returns>
    public class Solution : ISolution
    {
        private struct Dot
        {
            public int X;
            public int Y;

            public Dot(string str)
            {
                var tokens = str.Split(',').ToList();

                X = Convert.ToInt32(tokens[0]);
                Y = Convert.ToInt32(tokens[1]);
            }

            public Dot(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private struct Fold
        {
            public char Axis;
            public int Value;

            public Fold(string str)
            {
                var tokens = str.Split(' ')[2].Split('=').ToList();

                Axis = Convert.ToChar(tokens[0]);
                Value = Convert.ToInt32(tokens[1]);
            }
        }

        private class Paper
        {
            public int VisibleDots { get { return dots.Count; } }

            private List<Dot> dots;

            public int Width { get; private set; }
            public int Height { get; private set; }

            public Paper(List<Dot> dots)
            {
                this.dots = dots;

                Width = dots.Select(dot => dot.X).Max() + 1;
                Height = dots.Select(dot => dot.Y).Max() + 1;
            }

            public void Fold(Fold fold)
            {
                var newDots = new List<Dot>();

                foreach (var dot in dots)
                {
                    Dot toAdd = dot;

                    if (fold.Axis == 'y' && dot.Y > fold.Value)
                    {
                        toAdd = new Dot(dot.X, dot.Y - (dot.Y - fold.Value) * 2);
                    }
                    else if (fold.Axis == 'x' && dot.X > fold.Value)
                    {
                        toAdd = new Dot(dot.X - (dot.X - fold.Value) * 2, dot.Y);
                    }

                    if (!newDots.Contains(toAdd))
                    {
                        newDots.Add(toAdd);
                    }
                }

                dots = newDots;

                if (fold.Axis == 'y')
                {
                    Height = fold.Value;
                }
                else
                {
                    Width = fold.Value;
                }
            }

            public void DebugPrint()
            {
                for (var y = 0; y < Height; ++y)
                {
                    var sb = new StringBuilder();

                    for (var x = 0; x < Width; ++x)
                    {
                        sb.Append(dots.Contains(new Dot(x, y)) ? "#" : ".");
                    }

                    Console.WriteLine(sb.ToString());
                }

                Console.WriteLine();
            }
        }

        public (string PartOne, string PartTwo) Run()
        {
            var lines = File.ReadAllText(@"Day-13/Input.txt");

            var tokens = lines.Split("\r\n\r\n");

            var dots = tokens[0].Split("\r\n").Select(str => new Dot(str)).ToList();
            var folds = tokens[1].TrimEnd('\r', '\n').Split("\r\n").Select(str => new Fold(str)).ToList();

            var paper = new Paper(dots);

            var firstPartAnswer = -1;

            for (var i = 0; i < folds.Count; ++i)
            {
                var fold = folds[i];

                paper.Fold(fold);

                if (i == 0)
                {
                    firstPartAnswer = paper.VisibleDots;
                }
            }

            paper.DebugPrint();

            return (firstPartAnswer.ToString(), "ABKJFBGC");
        }
    }
}
