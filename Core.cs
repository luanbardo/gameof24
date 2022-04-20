using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOf24
{
    public static class Core
    {
        /// <summary>
        /// Splits the string at every comma
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal static int[] SplitInput(string str)
        {
            string[] splits = str.Split(',');

            List<int> results = new List<int>();

            for (int i = 0; i < splits.Length; i++)
            {
                if (int.TryParse(splits[i], out int result))
                {
                    results.Add(result);
                }
            }

            return results.ToArray();
        }

        /// <summary>
        /// Prints a solution to the console
        /// </summary>
        /// <param name="input"></param>
        /// <param name="prefix"></param>
        /// <param name="op1">First symbol</param>
        /// <param name="op2">Second symbol</param>
        /// <param name="op3">Third symbol</param>
        /// <param name="suffix"></param>
        /// <param name="functionsList"></param>
        private static void StoreFunction(int[] input, string prefix, string op1, string op2, string op3, string suffix,
            List<string> functionsList)
        {
            string function = prefix + input[0] + op1 + input[1] + op2 + input[2] + op3 + input[3] + suffix;
            functionsList.Add(function);
        }

        /// <summary>
        /// Try every written operations for every possible sorting of input
        /// When a solution is found, its written to the console
        /// </summary>
        /// <param name="input">Array with the 4 numbers to be tried</param>
        /// <param name="goal">Number that the 4 arrays numbers must reach</param>
        internal static void TryOperations(int[] input, int goal)
        {
            List<string> solutions = new();
            //Trying all operations for every possible randomization of the input list
            input.ForEachPermutation((inputPermut) => TryAllOperations(goal, inputPermut, solutions));

            //Solution not possible
            if (solutions.Count == 0)
            {
                Console.WriteLine("Could not find solution :(");
            }
            else
            {
                //Removing Duplicates
                solutions = solutions.Distinct().ToList();
                
                Console.WriteLine("\nPossible Solutions: ");
                
                //Print every found solution
                solutions.ForEach(s => Console.WriteLine(s + "\n"));
            }
        }

        /// <summary>
        /// Try multiple math functions and store the function if sucessful
        /// </summary>
        /// <param name="toReach"></param>
        /// <param name="input"></param>
        /// <param name="foundSolution"></param>
        /// <returns></returns>
        private static void TryAllOperations(int toReach, int[] input, List<string> solutions)
        {
            if (input[0] + input[1] + input[2] + input[3] == toReach)
            {
                StoreFunction(input, "", " + ", " + ", " + ", "", solutions);
            }

            if (input[0] * input[1] * input[2] * input[3] == toReach)
            {
                StoreFunction(input, "", " * ", " * ", " * ", "", solutions);
            }

            if (input[0] + input[1] + input[2] - input[3] == toReach)
            {
                StoreFunction(input, "", " + ", " + ", " - ", "", solutions);
            }

            if (input[0] * input[1] + input[2] + input[3] == toReach)
            {
                StoreFunction(input, "", " * ", " + ", " + ", "", solutions);
            }

            if (input[0] * (input[1] + input[2]) + input[3] == toReach)
            {
                StoreFunction(input, "", " * ( ", " + ", " ) + ", "", solutions);
            }

            if (input[0] * (input[1] + input[2] + input[3]) == toReach)
            {
                StoreFunction(input, "", " * ( ", " + ", " + ", " )", solutions);
            }

            if (input[0] * input[1] * input[2] + input[3] == toReach)
            {
                StoreFunction(input, "( ", " * ", " * ", " ) + ", "", solutions);
            }

            if (input[0] * input[1] * (input[2] + input[3]) == toReach)
            {
                StoreFunction(input, "( ", " * ", " * ( ", " + ", " )", solutions);
            }

            if (input[0] * input[1] + input[2] * input[3] == toReach)
            {
                StoreFunction(input, "( ", " * ", " ) + ( ", " * ", " )", solutions);
            }

            if (input[0] * input[1] * input[2] - input[3] == toReach)
            {
                StoreFunction(input, "( ", " * ", " * ", " ) - ", "", solutions);
            }

            if (input[0] * input[1] * (input[2] - input[3]) == toReach)
            {
                StoreFunction(input, "( ", " * ", " * ( ", " - ", " )", solutions);
            }

            if (input[0] * input[1] - input[2] * input[3] == toReach)
            {
                StoreFunction(input, "( ", " * ", " ) - ( ", " * ", " )", solutions);
            }

            if (input[0] * input[1] + input[2] - input[3] == toReach)
            {
                StoreFunction(input, "", " * ", " + ", " - ", "", solutions);
            }

            if (input[0] * (input[1] + input[2]) - input[3] == toReach)
            {
                StoreFunction(input, "", " * ( ", " + ", " ) - ", "", solutions);
            }

            if (input[0] * (input[1] - input[2]) + input[3] == toReach)
            {
                StoreFunction(input, "", " * ( ", " - ", " ) + ", "", solutions);
            }

            if (input[0] * (input[1] + input[2] - input[3]) == toReach)
            {
                StoreFunction(input, "", " * ( ", " + ", " - ", " )", solutions);
            }

            if (input[0] * input[1] - (input[2] + input[3]) == toReach)
            {
                StoreFunction(input, "", " * ", " - ( ", " + ", " )", solutions);
            }

            if (input[0] * input[1] == (toReach - input[3]) * input[2])
            {
                StoreFunction(input, "( ", " * ", " / ", " ) + ", "", solutions);
            }

            if (input[0] * (input[1] + input[2] / input[3]) == toReach)
            {
                StoreFunction(input, "", " * (", " + ", " / ", ")", solutions);
            }

            if (input[0] * input[1] + input[2] == toReach * input[3])
            {
                StoreFunction(input, "(( ", " * ", " ) + ", " ) / ", "", solutions);
            }

            if ((input[0] + input[1]) * input[2] == toReach * input[3])
            {
                StoreFunction(input, "(( ", " + ", " ) * ", " ) / ", "", solutions);
            }

            if (input[0] * input[1] == toReach * (input[2] + input[3]))
            {
                StoreFunction(input, "( ", " * ", " ) / ( ", " + ", " )", solutions);
            }

            if (input[0] * input[1] == (toReach + input[3]) * input[2])
            {
                StoreFunction(input, "( ", " * ", " / ", " ) - ", "", solutions);
            }

            if (input[0] * input[1] - input[2] == toReach * input[3])
            {
                StoreFunction(input, "(( ", " * ", " ) - ", " ) / ", "", solutions);
            }

            if ((input[0] - input[1]) * input[2] == toReach * input[3])
            {
                StoreFunction(input, "(( ", " - ", " ) * ", " ) / ", "", solutions);
            }

            if (input[0] * input[1] == toReach * (input[2] - input[3]))
            {
                StoreFunction(input, "( ", " * ", " ) / ( ", " - ", " )", solutions);
            }

            if (input[0] * input[1] * input[2] == toReach * input[3])
            {
                StoreFunction(input, "", " * ", " * ", " / ", "", solutions);
            }

            if (input[0] * input[1] == toReach * input[2] * input[3])
            {
                StoreFunction(input, "", " * ", " / ( ", " * ", " )", solutions);
            }

            if (input[0] * input[3] == toReach * (input[1] * input[3] - input[2]))
            {
                StoreFunction(input, "", " / ( ", " - ", " / ", " )", solutions);
            }

            if (input[0] * input[1] == toReach * input[2] * input[3])
            {
                StoreFunction(input, "( ", " * ", " / ", " ) / ", "", solutions);
            }
        }

        /// <summary>
        /// Heap's algorithm to find all permutations. Non recursive, more efficient.
        /// Source: https://stackoverflow.com/questions/11208446/generating-permutations-of-a-set-most-efficiently
        /// </summary>
        /// <param name="items">Items to permute in each possible ways</param>
        /// <param name="funcExecuteAndTellIfShouldStop"></param>
        /// <returns>Return true if cancelled</returns>
        public static void ForEachPermutation<T>(this T[] items, Action<T[]> funcExecuteAndTellIfShouldStop)
        {
            // Swap 2 elements of same type
            static void Swap<T>(ref T a, ref T b)
            {
                T temp = a;
                a = b;
                b = temp;
            }

            int countOfItem = items.Length;

            if (countOfItem <= 1)
            {
                funcExecuteAndTellIfShouldStop(items);
            }

            int[] indexes = new int[countOfItem];


            funcExecuteAndTellIfShouldStop(items);


            for (int i = 1; i < countOfItem;)
            {
                if (indexes[i] < i)
                {
                    // On the web there is an implementation with a multiplication which should be less efficient.
                    if ((i & 1) == 1) // if (i % 2 == 1)  ... more efficient ??? At least the same.
                    {
                        Swap(ref items[i], ref items[indexes[i]]);
                    }
                    else
                    {
                        Swap(ref items[i], ref items[0]);
                    }

                    funcExecuteAndTellIfShouldStop(items);
                    indexes[i]++;
                    i = 1;
                }
                else
                {
                    indexes[i++] = 0;
                }
            }
        }
    }
}