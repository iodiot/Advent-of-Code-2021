using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_10
{
    /// <summary>
    /// --- Day 10: Syntax Scoring ---
    /// </summary>
    public class Solution : ISolution
    {
        private static readonly string openBrackets   = "<{[(";
        private static readonly string closeBrackets  = ">}])";

        private static Dictionary<char, int> scoreTableOne = new() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        private static Dictionary<char, int> scoreTableTwo = new() { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };

        public (string PartOne, string PartTwo) Run()
        {
            var lines = File.ReadAllLines(@"Day-10/Input.txt");

            var totalScore = 0;
            var scores = new List<ulong>();

            foreach (var line in lines)
            {
                var stack = new Stack<char>();

                var broken = false;

                foreach (var ch in line)
                {
                    if (openBrackets.Contains(ch))
                    {
                        stack.Push(ch);
                    }
                    else if (stack.Count > 0)
                    {
                        var el = stack.Pop();

                        if (el != openBrackets[closeBrackets.IndexOf(ch)])
                        {
                            totalScore += scoreTableOne[ch];
                            broken = true;
                            break;
                        }
                    }
                }

                if (stack.Count > 0 && !broken)
                {
                    ulong score = 0;
                    foreach (var el in stack)
                    {
                        score = score * 5 + (ulong)scoreTableTwo[closeBrackets[openBrackets.IndexOf(el)]];
                    }
                    scores.Add(score);
                }
            }

            scores.Sort();

            return (totalScore.ToString(), scores[scores.Count / 2].ToString());
        }
    }
}
