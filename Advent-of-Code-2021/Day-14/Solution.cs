using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_14
{
    /// <summary>
    /// --- Day 14: Extended Polymerization ---
    /// </summary>
    public class Solution : ISolution
    {
        private class Counter
        {
            private readonly Dictionary<char, Int64> dict = new();

            public Counter(string template = "")
            {
                foreach (var ch in template.ToCharArray())
                {
                    Add(ch);
                }
            }

            public Int64 Get(char ch)
            {
                return dict.ContainsKey(ch) ? dict[ch] : 0;
            }

            public List<char> Keys()
            {
                return dict.Keys.ToList();
            }

            public Counter Add(Counter other)
            {
                foreach (var key in other.Keys())
                {
                    dict[key] = this.Get(key) + other.Get(key);
                }

                return this;
            }

            public Counter Add(char ch)
            {
                dict[ch] = dict.ContainsKey(ch) ? dict[ch] + 1 : 1;
                return this;
            }

            public Counter Subtract(char ch)
            {
                dict[ch] = dict.ContainsKey(ch) ? dict[ch] - 1 : -1;
                return this;
            }

            public Int64 Difference()
            {
                var ordered = dict.Values.OrderBy(el => el).ToList();

                return ordered.Last() - ordered.First();
            }
        }

        public (string PartOne, string PartTwo) Run()
        {
            var lines = File.ReadAllLines(@"Day-14/Input.txt");

            var template = lines[0];

            var rules = new Dictionary<string, char>();

            foreach (var line in lines.Skip(2))
            {
                var tokens = line.Split(" -> ");
                rules[tokens[0]] = tokens[1][0];
            }

            var memory = BuildMemory(rules, maxMemorySize: 22); // 22 is optimal value based on different runs

            Console.WriteLine($"Memory was built. Total size is { memory.Values.Count } entries.\n");

            var firstPart = GrowPolymer(template, rules, 10, memory).Difference();
            var secondPart = GrowPolymer(template, rules, 40, memory).Difference();

            return (
                firstPart.ToString(),
                secondPart.ToString()
                );
        }
        
        private static Dictionary<(string, Int64), Counter> BuildMemory(Dictionary<string, char> rules, int maxMemorySize)
        {
            var memory = new Dictionary<(string, Int64), Counter>();

            foreach (var ruleKey in rules.Keys)
            {
                for (var step = 1; step < maxMemorySize + 1; ++step)
                {
                    memory[(ruleKey, step)] = GrowPolymer(ruleKey, rules, step, memory);
                }
            }

            return memory;
        }

        private static Counter GrowPolymer(string template, Dictionary<string, char> rules, int steps, Dictionary<(string, Int64), Counter> memory)
        {
            if (steps == 0)
            {
                return new Counter(template);
            }

            var ret = new Counter();

            for (var i = 0; i < template.Length - 1; ++i)
            {
                var left = template[i];
                var right = template[i + 1];
                var pair = $"{ left }{ right }";

                var toInsert = rules[pair];

                if (memory.ContainsKey((pair, steps)))
                {
                    ret.Add(memory[(pair, steps)]).Subtract(right);
                }
                else
                {
                    ret.Add(GrowPolymer($"{ left }{ toInsert }", rules, steps - 1, memory));

                    if (steps > 1)
                    {
                        ret.Add(GrowPolymer($"{ toInsert }{ right }", rules, steps - 1, memory)).Subtract(toInsert).Subtract(right);
                    }
                }
            }

            ret.Add(template[^1]);
            return ret;
        }
    }
}
