using System;
using System.Collections.Generic;

namespace GameOf24
{
    public class Tests
    {
        internal static void RandomNumbersTest(out bool keepRunning)
        {
            Random random = new();
            int unsolved = 0;

            //Getting trials amount
            Console.WriteLine("How many tests?");
            string input = Console.ReadLine();
            int trials = Int32.Parse(input);

            for (int i = 0; i < trials; i++)
            {
                //Generating random input
                int[] randomInput = {0, 0, 0, 0};
                for (int j = 0; j < 4; j++)
                {
                    randomInput[j] = random.Next(1, 10);
                }

                List<string> solutions = new();
                bool foundSolution = Core.TryOperationsInEveryPermutation(randomInput, 24, solutions);

                if (!foundSolution)
                {
                    unsolved++;
                    Console.WriteLine("{0},{1},{2},{3} is unsolvable", randomInput[0],randomInput[1],randomInput[2],randomInput[3]);
                }
            }

            Console.WriteLine(unsolved + " tries went unsolved");

            keepRunning = false;
        }
    }
}