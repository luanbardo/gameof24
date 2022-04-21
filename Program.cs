using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOf24
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;
            while (run)
            {
                Loop(out run);
            }
        }

        /// <summary>
        /// Main game loop
        /// </summary>
        /// <param name="keepRunning">Should the application run the loop again?</param>
        private static void Loop(out bool keepRunning)
        {
            //Get goal from user
            Console.WriteLine("What is the goal of the game? (Ex:24)");
            string input = Console.ReadLine();
            int goal = Int32.Parse(input);

            //Get input from user
            Console.WriteLine(" \nPlease input exactly 4 numbers, each separated by a comma (Ex:13,1,4,20)");
            input = Console.ReadLine();
            int[] splitInput = Core.SplitInput(input);
            
            //Trying all operations in all permutations
            List<string> solutions = new();
            Core.TryOperationsInEveryPermutation(splitInput, goal, solutions);
            
            //Solution not possible
            if (solutions.Count == 0)
            {
                Console.WriteLine("Could not find solution :(");
            }
            else
            {
                PrintSolutions(solutions);
            }
            
            //Keep playing?
            Console.WriteLine(" \nTry other numbers? y/n ");
            input = Console.ReadLine();
            
            keepRunning = input.Equals("y");
            
            //Clean slate
            Console.Clear();
        }

        private static void PrintSolutions(List<string> solutions)
        {
            //Removing Duplicates
            solutions = solutions.Distinct().ToList();

            Console.WriteLine("\nPossible Solutions: ");

            //Print every found solution
            solutions.ForEach(s => Console.WriteLine(s + "\n"));
        }
    }
}