using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(@"Day-2/Input.txt");

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine();

            Console.WriteLine($"First part: { -1 }");
            Console.WriteLine($"Second part: { -1 }");

        }
    }
}
