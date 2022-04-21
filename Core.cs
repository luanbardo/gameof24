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
        /// <returns>Array containing input numbers separated by commas</returns>
        internal static int[] SplitInput(string input)
        {
            string[] splits = input.Split(',');

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
        /// Stores function in functionList for later processing
        /// </summary>
        private static void StoreFunction(int[] input, string prefix, string op1, string op2, string op3, string suffix,
            List<string> functionsList)
        {
            string function = prefix + input[0] + op1 + input[1] + op2 + input[2] + op3 + input[3] + suffix;
            functionsList.Add(function);
        }

        /// <summary>
        /// Try every written operations for every possible arrangement of the input array
        /// </summary>
        /// <param name="input">Array with the 4 numbers to be used on functions</param>
        /// <param name="goal">Number that the 4 arrays numbers must reach</param>
        /// <param name="solutions">Fresh list to store found solutions</param>
        internal static bool TryOperationsInEveryPermutation(int[] input, int goal, List<string> solutions)
        {
            //Calling TryAllOperations for each permutation of input
            input.ForEachPermutation(inputPermut => TryAllOperations(goal, inputPermut, solutions));
            return solutions.Count >= 1;
        }
        
        /// <summary>
        /// Heap's algorithm to find all permutations. Non recursive, more efficient.
        /// Source: https://stackoverflow.com/questions/11208446/generating-permutations-of-a-set-most-efficiently
        /// </summary>
        /// <param name="items">Items to permute in each possible ways</param>
        /// <param name="callback">Method to be called for each permutation</param>
        private static void ForEachPermutation<T>(this T[] items, Action<T[]> callback)
        {
            // Swap 2 elements of same type
            void Swap<T>(ref T a, ref T b)
            {
                T temp = a;
                a = b;
                b = temp;
            }

            int countOfItem = items.Length;

            if (countOfItem <= 1)
            {
                callback(items);
            }

            int[] indexes = new int[countOfItem];


            callback(items);


            for (int i = 1; i < countOfItem;)
            {
                if (indexes[i] < i)
                {
                    if ((i & 1) == 1)
                    {
                        Swap(ref items[i], ref items[indexes[i]]);
                    }
                    else
                    {
                        Swap(ref items[i], ref items[0]);
                    }

                    callback(items);
                    indexes[i]++;
                    i = 1;
                }
                else
                {
                    indexes[i++] = 0;
                }
            }
        }

        /// <summary>
        /// Try multiple math functions and store the functions in solutions list when successful
        /// </summary>
        private static void TryAllOperations(int goal, int[] input, List<string> solutions)
        {
            if (input[0] + input[1] + input[2] + input[3] == goal)
            {
                StoreFunction(input, "", " + ", " + ", " + ", "", solutions);
            }

            if (input[0] * input[1] * input[2] * input[3] == goal)
            {
                StoreFunction(input, "", " * ", " * ", " * ", "", solutions);
            }

            if (input[0] + input[1] + input[2] - input[3] == goal)
            {
                StoreFunction(input, "", " + ", " + ", " - ", "", solutions);
            }

            if (input[0] * input[1] + input[2] + input[3] == goal)
            {
                StoreFunction(input, "", " * ", " + ", " + ", "", solutions);
            }

            if (input[0] * (input[1] + input[2]) + input[3] == goal)
            {
                StoreFunction(input, "", " * ( ", " + ", " ) + ", "", solutions);
            }

            if (input[0] * (input[1] + input[2] + input[3]) == goal)
            {
                StoreFunction(input, "", " * ( ", " + ", " + ", " )", solutions);
            }

            if (input[0] * input[1] * input[2] + input[3] == goal)
            {
                StoreFunction(input, "( ", " * ", " * ", " ) + ", "", solutions);
            }

            if (input[0] * input[1] * (input[2] + input[3]) == goal)
            {
                StoreFunction(input, "( ", " * ", " * ( ", " + ", " )", solutions);
            }

            if (input[0] * input[1] + input[2] * input[3] == goal)
            {
                StoreFunction(input, "( ", " * ", " ) + ( ", " * ", " )", solutions);
            }

            if (input[0] * input[1] * input[2] - input[3] == goal)
            {
                StoreFunction(input, "( ", " * ", " * ", " ) - ", "", solutions);
            }

            if (input[0] * input[1] * (input[2] - input[3]) == goal)
            {
                StoreFunction(input, "( ", " * ", " * ( ", " - ", " )", solutions);
            }

            if (input[0] * input[1] - input[2] * input[3] == goal)
            {
                StoreFunction(input, "( ", " * ", " ) - ( ", " * ", " )", solutions);
            }
            
            if ((input[0] + input[1]) * (input[2] - input[3]) == goal)
            {
                StoreFunction(input, "( ", " + ", " ) * ( ", " - ", " )", solutions);
            }

            if (input[0] / (input[1] + input[2] + input[3]) == goal)
            {
                StoreFunction(input, "", " / (", " + ", " + ", ")", solutions);
            }

            if (input[0] * input[1] - input[2] / input[3] == goal)
            {
                StoreFunction(input, "", " * ", " - ", " / ", "", solutions);
            }

            if ((input[0] + input[1] / input[2]) * input[3] == goal)
            {
                StoreFunction(input, "(", " + ", " / ", ") * ", "", solutions);
            }

            if ((input[0] - input[1]) * input[2] - input[3] == goal)
            {
                StoreFunction(input, "( ", " - ", ") * ", " - ", "", solutions);
            }
            
            if ((input[0] + input[1]) * (input[2] + input[3]) == goal)
            {
                StoreFunction(input, "( ", " + ", " ) * ( ", " + ", " )", solutions);
            }
            
            if ((input[0] - input[1]) * (input[2] - input[3]) == goal)
            {
                StoreFunction(input, "( ", " - ", " ) * ( ", " - ", " )", solutions);
            }

            if (input[0] * (input[1] * input[2] + input[3]) == goal)
            {
                StoreFunction(input, "", " * (", " * ", " + ", " )", solutions);
            }
            
            if (input[0] * (input[1] * input[2] - input[3]) == goal)
            {
                StoreFunction(input, "", " * (", " * ", " - ", " )", solutions);
            }
            
            if (input[0] * input[1] + input[2] - input[3] == goal)
            {
                StoreFunction(input, "", " * ", " + ", " - ", "", solutions);
            }

            if (input[0] * (input[1] + input[2]) - input[3] == goal)
            {
                StoreFunction(input, "", " * ( ", " + ", " ) - ", "", solutions);
            }

            if (input[0] * (input[1] - input[2]) + input[3] == goal)
            {
                StoreFunction(input, "", " * ( ", " - ", " ) + ", "", solutions);
            }

            if (input[0] * (input[1] + input[2] - input[3]) == goal)
            {
                StoreFunction(input, "", " * ( ", " + ", " - ", " )", solutions);
            }

            if (input[0] * input[1] - (input[2] + input[3]) == goal)
            {
                StoreFunction(input, "", " * ", " - ( ", " + ", " )", solutions);
            }

            if (input[0] * input[1] == (goal - input[3]) * input[2])
            {
                StoreFunction(input, "( ", " * ", " / ", " ) + ", "", solutions);
            }

            if (input[0] * (input[1] + input[2] / input[3]) == goal)
            {
                StoreFunction(input, "", " * (", " + ", " / ", ")", solutions);
            }

            if (input[0] * input[1] + input[2] == goal * input[3])
            {
                StoreFunction(input, "(( ", " * ", " ) + ", " ) / ", "", solutions);
            }

            if ((input[0] + input[1]) * input[2] == goal * input[3])
            {
                StoreFunction(input, "(( ", " + ", " ) * ", " ) / ", "", solutions);
            }

            if (input[0] * input[1] == goal * (input[2] + input[3]))
            {
                StoreFunction(input, "( ", " * ", " ) / ( ", " + ", " )", solutions);
            }

            if (input[0] * input[1] == (goal + input[3]) * input[2])
            {
                StoreFunction(input, "( ", " * ", " / ", " ) - ", "", solutions);
            }

            if (input[0] * input[1] - input[2] == goal * input[3])
            {
                StoreFunction(input, "(( ", " * ", " ) - ", " ) / ", "", solutions);
            }

            if ((input[0] - input[1]) * input[2] == goal * input[3])
            {
                StoreFunction(input, "(( ", " - ", " ) * ", " ) / ", "", solutions);
            }

            if (input[0] * input[1] == goal * (input[2] - input[3]))
            {
                StoreFunction(input, "( ", " * ", " ) / ( ", " - ", " )", solutions);
            }

            if (input[0] * input[1] * input[2] == goal * input[3])
            {
                StoreFunction(input, "", " * ", " * ", " / ", "", solutions);
            }

            if (input[0] * input[1] == goal * input[2] * input[3])
            {
                StoreFunction(input, "", " * ", " / ( ", " * ", " )", solutions);
            }

            if (input[0] * input[3] == goal * (input[1] * input[3] - input[2]))
            {
                StoreFunction(input, "", " / ( ", " - ", " / ", " )", solutions);
            }

            if (input[0] * input[1] == goal * input[2] * input[3])
            {
                StoreFunction(input, "( ", " * ", " / ", " ) / ", "", solutions);
            }
            
            //Must not destroy the world order
            if (input[1] - input[2] + input[3] != 0)
            {
                if (input[0] / (input[1] - input[2] + input[3]) == goal)
                {
                    StoreFunction(input, "", " / (", " - ", " + ", ")", solutions);
                }
            }
        }
    }
}