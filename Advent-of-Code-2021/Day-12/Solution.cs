using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_12
{
    /// <summary>
    /// --- Day 12: Passage Pathing ---
    /// </summary>
    public class Solution : ISolution
    {
        public (string PartOne, string PartTwo) Run()
        {
            var lines = File.ReadAllLines(@"Day-12/Input.txt");

            var junctions = new Dictionary<string, List<string>>();

            foreach (var line in lines)
            {
                var from = line.Split('-')[0];
                var to = line.Split('-')[1];

                if (!junctions.ContainsKey(from))
                {
                    junctions[from] = new List<string>();
                }

                if (!junctions.ContainsKey(to))
                {
                    junctions[to] = new List<string>();
                }

                junctions[from].Add(to);
                junctions[to].Add(from);
            }

            return (CountPathes(junctions, firstPart: true).ToString(), CountPathes(junctions, firstPart: false).ToString());
        }

        private int CountPathes(Dictionary<string, List<string>> junctions, bool firstPart = true)
        {
            var allPathes = new List<string>();

            var queue = new Queue<List<string>>();

            foreach (var cave in junctions["start"])
            {
                queue.Enqueue(new List<string> { "start", cave });
            }

            while (queue.Count > 0)
            {
                var path = queue.Dequeue();

                var lastCave = path[^1];

                foreach (var cave in junctions[lastCave])
                {
                    var newPath = path.ToList();

                    if (cave == "end")
                    {
                        newPath.Add("end");
                        allPathes.Add(HashPath(newPath));
                        continue;
                    }

                    var isBigCave = char.IsUpper(cave[0]);

                    var firstPartCond = firstPart && !newPath.Contains(cave);
                    var secondPartCond = !firstPart && (!HasDoubleSmallCave(newPath) && cave != "start" || !newPath.Contains(cave));

                    if (isBigCave || firstPartCond || secondPartCond)
                    {
                        newPath.Add(cave);
                        queue.Enqueue(newPath);
                    }
                }
            }

            //foreach (var path in allPathes)
            //{
            //    Console.WriteLine(path);
            //}

            return allPathes.Count;
        }

        private string HashPath(List<string> path)
        {
            return string.Join(",", path);
        }

        private static bool HasDoubleSmallCave(List<string> path)
        {
            var smallCaves = path.Where(cave => cave != "start" && cave != "end" && char.IsLower(cave[0])).ToList();

            foreach (var smallCave in smallCaves)
            {
                if (path.Count(cave => cave == smallCave) == 2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
