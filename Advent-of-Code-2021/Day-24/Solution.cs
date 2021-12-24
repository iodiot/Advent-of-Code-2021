using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2021.Day_24
{
    /// <summary>
    /// --- Day 24: Arithmetic Logic Unit ---
    /// </summary>
    public class Solution : ISolution
    {
        public (string PartOne, string PartTwo) Run()
        {
            var lines = File.ReadAllLines(@"Day-24/Input.txt").ToList();

            var alu = new Alu(lines);

            long largestModelNumber = -1;
            
            for (var i = 99999999999999; i >= 99999999999999 - 100; --i)
            {
                var input = i.ToString().ToCharArray().Reverse().Select(ch => (long)(ch - '0')).ToList();

                if (input.Contains(0))
                {
                    continue;
                }

                //if (i % 1000 == 0) Console.WriteLine(i.ToString());

                var r = alu.Run(new Queue<long>(input));

                if (r == 0)
                {
                    largestModelNumber = r;
                    break;
                }

                Console.WriteLine(string.Format("{0} => {1:X8}", i, r));
            }

            return (largestModelNumber.ToString(), "");
        }
    }

    public class Alu
    {
        private List<string> lines;

        public Alu(List<string> lines)
        {
            this.lines = lines;
        }

        public long Run(Queue<long> input)
        {
            var memory = new Dictionary<char, long>();

            for (var i = 0; i < lines.Count; ++i)
            {
                var tokens = lines[i].Split(' ');

                if (!memory.ContainsKey(tokens[1][0]))
                {
                    memory[tokens[1][0]] = 0;
                }

                if (tokens.Length == 3 && char.IsLetter(tokens[2][0]) && !memory.ContainsKey(tokens[2][0]))
                {
                    memory[tokens[2][0]] = 0;
                }

                var left = tokens[1][0];
                var right = tokens.Length == 3 ? (char.IsLetter(tokens[2][0]) ? (memory[tokens[2][0]]) : Convert.ToInt32(tokens[2])) : 0;

                switch (tokens[0])
                {
                    case "inp":
                        memory[left] = input.Dequeue();
                        break;
                    case "add":
                        memory[left] += right;
                        break;
                    case "mul":
                        memory[left] *= right;
                        break;
                    case "div":
                        memory[left] = (int)Math.Floor((double)memory[left] / right);
                        break;
                    case "mod":
                        memory[left] = memory[left] % right;
                        break;
                    case "eql":
                        memory[left] = (memory[left] == right) ? 1 : 0;
                        break;
                    default:
                        break;
                }
            }

            return memory['z'];
        }
    }
}
