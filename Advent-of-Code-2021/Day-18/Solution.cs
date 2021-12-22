using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2021.Day_18
{
    public class Node
    {
        public Node Left;
        public Node Right;
        public int Value;
        public bool IsLeaf;
        public bool IsRegularPair => !IsLeaf && Left.IsLeaf && Right.IsLeaf;

        public Node FirstLeftLeaf;
        public Node FirstRightLeaf;

        public Node(Node left, Node right)
        {
            Left = left;
            Right = right;
        }

        public Node(int value)
        {
            Value = value;
            IsLeaf = true;
        }
    }


    public static class PairBuilder
    {
        private static int GetPrecedence(string token)
        {
            return token == "," ? 1 : -1;
        }

        private static List<string> InfixToPostfix(List<string> tokens)
        {
            var result = new List<string>();

            var stack = new Stack<string>();

            foreach (var token in tokens)
            {
                if (Char.IsDigit(token[0]))
                {
                    result.Add(token);
                }
                else if (token[0] == '[')
                {
                    stack.Push(token);
                }
                else if (token[0] == ']')
                {
                    while (stack.Count > 0 && stack.Peek() != "[")
                    {
                        result.Add(stack.Pop());
                    }

                    if (stack.Count > 0 && stack.Peek() != "[")
                    {
                        return null;
                    }
                    else
                    {
                        stack.Pop();
                    }
                }
                else
                {
                    while (stack.Count > 0 && GetPrecedence(token) <= GetPrecedence(stack.Peek()))
                    {
                        result.Add(stack.Pop());
                    }

                    stack.Push(token);
                }
            }

            while (stack.Count > 0)
            {
                result.Add(stack.Pop());
            }

            return result;
        }

        public static Node BuildTree(List<string> tokens)
        {
            var postfix = InfixToPostfix(tokens);

            var sb = new StringBuilder();
            foreach (var token in postfix)
            {
                sb.Append(token);
            }

            Console.WriteLine(sb.ToString());

            var stack = new Stack<Node>();

            var leafs = new Dictionary<int, Node>();
            var leafIndexes = new List<int>();


            for (var i = 0; i < postfix.Count; ++i)
            {
                if (char.IsDigit(postfix[i][0]))
                {
                    leafs[i] = new Node(Convert.ToInt32(postfix[i]));
                    leafIndexes.Add(i);
                }
            }

            var lastLeafIndex = -1;

            for (var i = 0; i < postfix.Count; ++i)
            {
                var token = postfix[i];

                if (char.IsDigit(token[0]))
                {
                    stack.Push(leafs[i]);
                    lastLeafIndex = i;
                }
                else
                {
                    var right = stack.Pop();
                    var left = stack.Pop();

                    var node = new Node(left, right);

                    var n = leafIndexes.IndexOf(lastLeafIndex);

                    if (n != -1)
                    {
                        if (n - 1 >= 0)
                        {
                            node.FirstLeftLeaf = leafs[leafIndexes[n - 1]];
                        }

                        if (n + 1 < leafIndexes.Count)
                        {
                            node.FirstRightLeaf = leafs[leafIndexes[n + 1]];
                        }
                    }

                    stack.Push(node);
                }
            }

            return stack.Pop();
        }
    }

    /// <summary>
    /// --- Day 18: Snailfish ---
    /// </summary>
    public class Solution : ISolution
    {
        public (string PartOne, string PartTwo) Run()
        {
            var lines = File.ReadAllLines(@"Day-18/Input.txt");

            foreach (var line in lines)
            {
                var tokens = Regex.Matches(line, "([0-9]+)|\\[|\\]|,").Select(match => match.ToString()).ToList();

                var tree = PairBuilder.BuildTree(tokens);
                 PrintTree(tree);
                exploded = false;
                Reduce(tree, 0);
                PrintTree(tree);

            }

            return ("", "");
        }

        public static void PrintTree(Node tree, int depth = 0)
        {
            if (tree.IsLeaf)
            {
                Console.Write(tree.Value);
                return;
            }
            
            Console.Write("[");
            PrintTree(tree.Left, depth + 1);
            Console.Write(",");
            PrintTree(tree.Right, depth + 1);
            Console.Write("]");

            if (depth == 0)
            {
                Console.WriteLine();
            }
        }

        bool exploded;

        public Node Reduce(Node node, int depth)
        {
            if (node.IsLeaf)
            {
                return node;
            }

            if (depth == 3 && !exploded)
            {
                if (node.Left.IsRegularPair && node.Right.IsLeaf)
                {
                    exploded = true;

                    if (node.FirstLeftLeaf != null)
                    {
                        node.FirstLeftLeaf.Value += node.Left.Left.Value;
                    }

                    return new Node(new Node(0), new Node(node.Left.Right.Value + node.Right.Value));
                }
                else if (node.Left.IsLeaf && node.Right.IsRegularPair)
                {
                    exploded = true;

                    if (node.FirstRightLeaf != null)
                    {
                        node.FirstRightLeaf.Value += node.Right.Right.Value;
                    }

                    return new Node(new Node(node.Left.Value + node.Right.Left.Value), new Node(0));
                }
            }

            if (depth >= 3)
            {
                return node;
            }

            node.Left = Reduce(node.Left, depth + 1);
            node.Right = Reduce(node.Right, depth + 1);

            return node;
        }
    }
}
