using System;
using System.Collections.Generic;
using System.Linq;

namespace MAD_Lab1_Program1
{
    internal static class Program
    {
        private static string _userInput;
        private static int _parsedInput;
        private static List<List<int>> _grid;

        /*
            Asks user the the number between 3 and 9
            Create a times table using that number
            Output a menu "Flip on 1) Horizon, 2) Vertical, 3) Diagonal Left, 4) Diagonal Right, 5) To End: "
            Based upon selection, output the table with the new orientation, followed by the menu unless 5 is selected.
         */
        private static void Main(string[] args)
        {
            GetGridAmountInput();
            GenerateGrid();

            while (true)
            {
                PrintGrid();
                GetMenuInput();
                switch (_parsedInput)
                {
                    case 1:
                        FlipGridHorizontal();
                        break;
                    case 2:
                        FlipGridVertical();
                        break;
                    case 3:
                        break;
                    case 4:
                        FlipGridVertical();
                        FlipGridHorizontal();
                        break;
                    case 5:
                        Console.WriteLine("Goodbye!");
                        return;
                }
            }
        }

        private static bool InputOutOfRange(int min, int max, int value) => value < min || value > max;

        private static void GetGridAmountInput()
        {
            do
            {
                Console.Write("Please enter a number between 3 and 9\n==");
                _userInput = Console.ReadLine();
                if (!int.TryParse(_userInput, out _parsedInput))
                    Console.WriteLine($"Invalid input. Please enter a number.");
            } while (InputOutOfRange(3, 9, _parsedInput));
        }

        private static void GenerateGrid()
        {
            _grid = new List<List<int>>();
            //generate times table
            for (var r = 1; r <= _parsedInput; r++)
            {
                var row = new List<int>();
                for (var c = 1; c <= _parsedInput; c++)
                    row.Add(c * r);
                _grid.Add(row);
            }
        }

        private static void FlipGridHorizontal()
        {
            foreach (var row in _grid)
            {
                if (row.First() < row.Last())
                    row.Sort((x, y) => y.CompareTo(x));
                else
                    row.Sort((x, y) => x.CompareTo(y));
            }
        }

        private static void FlipGridVertical()
        {
            if (_grid.First().First() < _grid.Last().First())
                _grid.Sort((x, y) => y.First().CompareTo(x.First()));
            else
                _grid.Sort((x, y) => x.First().CompareTo(y.First()));
        }

        private static void PrintGrid()
        {
            Console.WriteLine("----------");
            foreach (var row in _grid)
            {
                foreach (var val in row)
                {
                    Console.Write($"{val}");
                    Console.Write(val < 10 ? "   " : "  ");
                }

                Console.WriteLine("|");
            }

            Console.WriteLine("----------");
        }

        private static void GetMenuInput()
        {
            do
            {
                Console.Write("1) Flip on Horizon\n"          +
                              "2) Flip on Vertical\n"         +
                              "3) Flip on Diagonal (Left)\n"  +
                              "4) Flip on Diagonal (Right)\n" +
                              "5) End program\n"              +
                              "==");
                _userInput = Console.ReadLine();
                if (!int.TryParse(_userInput, out _parsedInput))
                    Console.WriteLine($"Invalid input. Please enter a number.");
            } while (InputOutOfRange(1, 5, _parsedInput));
        }
    }
}