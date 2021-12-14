using System;

namespace Advent_of_Code_2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var solution = new Day_12.Solution();

            var (PartOne, PartTwo) = solution.Run();

            Console.WriteLine($"First part: { PartOne }");
            Console.WriteLine($"Second part: { PartTwo }");

            Console.ReadKey();
        }
    }
}
