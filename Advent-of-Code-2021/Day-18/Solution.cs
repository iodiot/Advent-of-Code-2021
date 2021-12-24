using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2021.Day_18
{
    /// <summary>
    /// --- Day 18: Snailfish ---
    /// </summary>
    public class Solution : ISolution
    {
        public (string PartOne, string PartTwo) Run()
        {
            var lines = File.ReadAllLines(@"Day-18/Input.txt");

            var numbers = lines.Select(line => new SnailfishNumber(Regex.Matches(line, "([0-9]+)|\\[|\\]|,").Select(match => match.ToString()).ToList())).ToList();

            return (RunFirstPart(numbers).ToString(), RunSecondPart(numbers).ToString());
        }

        private static int RunFirstPart(List<SnailfishNumber> numbers)
        {
            var number = numbers[0].MakeCopy();

            foreach (var other in numbers.Skip(1))
            {
                number.Add(other).Reduce(); ;
            }

            return number.CalcMagnitude();
        }

        private static int RunSecondPart(List<SnailfishNumber> numbers)
        {
            var maxMagnitude = -1;

            for (var i = 0; i < numbers.Count; ++i)
            {
                for (var j = 0; j < numbers.Count; ++j)
                {
                    if (i != j)
                    {
                        maxMagnitude = Math.Max(numbers[i].MakeCopy().Add(numbers[j]).Reduce().CalcMagnitude(), maxMagnitude);
                    }
                }
            }

            return maxMagnitude;
        }
    }

    public class SnailfishNumber
    {
        public List<string> Tokens => tokens.ToList();

        private List<string> tokens;

        public SnailfishNumber(List<string> tokens)
        {
            this.tokens = tokens;
        }

        public SnailfishNumber MakeCopy()
        {
            return new SnailfishNumber(Tokens);
        }

        public SnailfishNumber Reduce()
        {
            while (true)
            {
                if (TryExplode())
                {
                    continue;
                }

                if (TrySplit())
                {
                    continue;
                }

                break;
            }

            return this;
        }

        public SnailfishNumber Add(SnailfishNumber other)
        {
            var newTokens = new List<string>();

            newTokens.Add("[");
            newTokens.AddRange(tokens);
            newTokens.Add(",");
            newTokens.AddRange(other.Tokens);
            newTokens.Add("]");

            tokens = newTokens;

            return this;
        }

        public int CalcMagnitude()
        {
            return CalcMagnitudeRecursive(tokens, 1).Magnitude;
        }

        private static (int Magnitude, int End) CalcMagnitudeRecursive(List<string> tokens, int start)
        {
            var sum = 0;

            for (var i = 0; i < 2; ++i)
            {
                var factor = i == 0 ? 3 : 2;

                if (tokens[start][0] == '[')
                {
                    var (magnitude, end) = CalcMagnitudeRecursive(tokens, start + 1);

                    sum += factor * magnitude;
                    start = end;
                }
                else
                {
                    sum += factor * Convert.ToInt32(tokens[start]);
                    start += 1;
                }

                start += 1; 
            }

            return (sum, start);
        }

        private bool TryExplode()
        {
            var depth = 0;
            var pos = -1;

            for (var i = 0; i < tokens.Count; ++i)
            {
                var token = tokens[i];
                var ch = tokens[i][0];

                if (ch == '[')
                {
                    depth += 1;
                }
                else if (ch == ']')
                {
                    depth -= 1;
                }
                else
                {
                    if (depth == 5 && char.IsDigit(tokens[i][0]) && char.IsDigit(tokens[i + 2][0]))
                    {
                        pos = i;
                        break;
                    }
                }
            }

            if (pos != -1)
            {
                AddToNearbyInt(pos - 1, Convert.ToInt32(tokens[pos]), -1);
                AddToNearbyInt(pos + 3, Convert.ToInt32(tokens[pos + 2]), +1);

                var newTokens = new List<string>();
                newTokens.AddRange(tokens.Take(pos - 1));
                newTokens.Add("0");
                newTokens.AddRange(tokens.Skip(pos + 4));
                tokens = newTokens;
            }

            return pos != -1;
        }

        private bool TrySplit()
        {
            var pos = -1;

            for (var i = 0; i < tokens.Count; ++i)
            {
                if (char.IsDigit(tokens[i][0]) && Convert.ToInt32(tokens[i]) >= 10)
                {
                    pos = i;
                    break;
                }
            }

            if (pos != -1)
            {
                var n = Convert.ToInt32(tokens[pos]);
                var floor = (int)Math.Floor((double)n / 2);
                var ceiling = (int)Math.Ceiling((double)n / 2);

                var newTokens = new List<string>();
                newTokens.AddRange(tokens.Take(pos));
                newTokens.AddRange(new List<string> { "[", floor.ToString(), ",", ceiling.ToString(), "]" });
                newTokens.AddRange(tokens.Skip(pos + 1));
                tokens = newTokens;
            }

            return pos != -1;
        }

        private void AddToNearbyInt(int pos, int toAdd, int step)
        {
            while (pos >= 0 && pos < tokens.Count)
            {
                if (char.IsDigit(tokens[pos][0]))
                {
                    tokens[pos] = (Convert.ToInt32(tokens[pos]) + toAdd).ToString();
                    return;
                }

                pos += step;
            }
        }
    }
}
