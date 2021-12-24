using System;

namespace Advent_of_Code_2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var solution = new Day_14.Solution();

            var watch = System.Diagnostics.Stopwatch.StartNew();

            var (PartOne, PartTwo) = solution.Run();

            Console.WriteLine($"First part: { PartOne }");
            Console.WriteLine($"Second part: { PartTwo }");

            Console.WriteLine($"\nTotal run time: { watch.ElapsedMilliseconds / 1000 } seconds.");

            Console.Read();
        }
    }
}
