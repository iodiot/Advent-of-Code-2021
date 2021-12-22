using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2021.Day_21
{
    /// <summary>
    /// --- Day 21: Dirac Dice ---
    /// </summary>
    public class Solution : ISolution
    {
        private class Player
        {
            public int Space { get; private set; }
            public int Score { get; private set; }

            public Player(int space, int score = 0)
            {
                Space = space;
                Score = score;
            }

            public void Move(int value)
            {
                Space += value;

                while (Space > 10)
                {
                    Space -= 10;
                }

                Score += Space;
            }

            public Player MakeCopy()
            {
                return new Player(Space, Score);
            }
        }

        private class Die
        {
            public int Value { get; private set; }
            public int RolledTimes { get; private set; }
            public int Sides { get; private set; }

            public Die(int sides, int value = 1)
            {
                Sides = sides;
                Value = value;
            }

            public int Roll()
            {
                RolledTimes += 1;

                var toReturn = Value;

                Value += 1;
                if (Value > Sides)
                {
                    Value = 1;
                }

                return toReturn;
            }

            public Die MakeCopy()
            {
                return new Die(Sides, Value);
            }
        }

        public (string PartOne, string PartTwo) Run()
        {
            var text = File.ReadAllText(@"Day-21/Input.txt");

            var numbers = Regex.Matches(text, "[0-9]+").Select(match => Convert.ToInt32(match.ToString())).ToList();

            return (RunFirstPart(numbers[1], numbers[3]).ToString(), RunSecondPart(numbers[1], numbers[3]).ToString());
        }

        private static int RunFirstPart(int startPosition1, int startPosition2)
        {
            var player1 = new Player(startPosition1);
            var player2 = new Player(startPosition2);

            var die = new Die(sides: 100);

            var finalScore = -1;

            while (true)
            {
                player1.Move(die.Roll() + die.Roll() + die.Roll());

                if (player1.Score >= 1000)
                {
                    finalScore = player2.Score * die.RolledTimes;
                    break;
                }

                player2.Move(die.Roll() + die.Roll() + die.Roll());

                if (player2.Score >= 1000)
                {
                    finalScore = player1.Score * die.RolledTimes;
                    break;
                }
            }

            return finalScore;
        }

        private int RunSecondPart(int startSpace1, int startSpace2)
        {
            var wins = Simulate(new Player(startSpace1), new Player(startSpace2), new Dictionary<string, int>());

            return wins;
        }

        private static string HashKey(Player player1, Player player2)
        {
            return $"{player1.Space}-{player1.Score}-{player2.Space}-{player2.Score}";
        }

        private static int Simulate(Player player1, Player player2, Dictionary<string, int> dp)
        {
            const int winScore = 21;

            if (player1.Score >= winScore || player2.Score >= winScore)
            {
                return player1.Score >= winScore ? 1 : 0;
            }

            var key = HashKey(player1, player2);

            if (dp.ContainsKey(key))
            {
                return dp[key];
            }

            var wins = 0;

            while (true)
            {
                for (var i = 0; i < 3; ++i)
                {
                    var player = player1.MakeCopy();
                    player.Move(i + 1);
                    wins += Simulate(player, player2.MakeCopy(), dp);
                }

                player1.Move(6);

                if (player1.Score >= winScore)
                {
                    wins += 1;
                    break;
                }

                for (var i = 0; i < 3; ++i)
                {
                    var player = player2.MakeCopy();
                    player.Move(i + 1);
                    wins += Simulate(player1.MakeCopy(), player, dp);
                }

                player2.Move(6);

                if (player2.Score >= winScore)
                {
                    break;
                }
            }

            dp[HashKey(player1, player2)] = wins;
            
            return wins;
        }
    }
}
