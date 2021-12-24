using System;

namespace Advent_of_Code_2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var solution = new Day_24.Solution();

            var watch = System.Diagnostics.Stopwatch.StartNew();

            var (PartOne, PartTwo) = solution.Run();

            Console.WriteLine($"First part: { PartOne }");
            Console.WriteLine($"Second part: { PartTwo }");

            Console.WriteLine(String.Format("\nTotal run time: {0:0.00} seconds.", watch.ElapsedMilliseconds / 1000.0));

            Console.Read();
        }
    }
}
