using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoProject.sudoku_solver.ecxeptions;
using SodukoProject.sudoku_reader;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace SodukoProject.sudoku_solver
{
    public class Algorithm
    {
        
        /// <summary>
        /// a method that prints a menu to the sudoku project
        /// </summary>
        public static void show_menu()
        {
            Console.WriteLine("\nSudoku Menu:");
            Console.WriteLine("1. Read Sudoku from file");
            Console.WriteLine("2. Read Sudoku from console");
            Console.WriteLine("3. Exit");
        }


        /// <summary>
        /// a method that recieves an input from the user based of the menu 
        /// </summary>
        /// <returns>an ISudokuReader based on the choice(file reader or console reader)</returns>
        /// <exception cref="exit"></exception>
        public static ISudokuReader get_choice()
        {
            bool isValidChoice = false;
            ISudokuReader reader = null;

            while (!isValidChoice)
            {
                Console.WriteLine("Enter your choice (1, 2, or 3): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        reader = new FileReader();
                        isValidChoice = true;
                        break;
                    case "2":
                        reader = new ConsoleReader();
                        isValidChoice = true;
                        break;
                    case "3":
                        throw new exit();
                    default:
                        Console.WriteLine("\nInvalid choice. Please enter 1, 2, or 3.");
                        break;
                }
            }
            return reader;
        }


        /// <summary>
        /// a method that receives board and tries to solve it
        /// </summary>
        /// <param name="board"></param> a Board object
        /// <returns>true if solved the board, else false </returns>
        public static bool Solve(Board board)
        {
            board.SetOptionsToAll();
            return Backtrack(board);
        }


        /// <summary>
        /// a recursive method that receives board and tries to solve it using rule based and backtracking
        /// </summary>
        /// <param name="board"></param>
        /// <returns>true if solved the board, else false </returns>
        public static bool Backtrack(Board board)
        {
            int sumChanges = 0;
            int changed = 1;

            while (changed != 0)
            {
                changed = 0;
                changed += board.HiddenSingles();
                changed += board.NakedSingles(0);
                sumChanges += changed;
            }

            Square pos = board.FindEmptyPos();
            if (pos == null)
            {
                return true;
            }

            int row = pos.Row;
            int col = pos.Col;
            List<string> options = new List<string>(pos.Options);

            foreach (string opt in options)
            {
                board.WriteToBoard(row, col, opt);
                if (Backtrack(board))
                {
                    return true;
                }
                board.RetrieveChanges(1);
            }

            board.RetrieveChanges(sumChanges);
            return false;
        }


        /// <summary>
        /// a method that receives sudokuStr and tries to solve it
        /// </summary>
        /// <param name="sudokuStr"></param> a string that represents a sudoku board
        /// <returns>true if solved the board, else false</returns>
        public static bool solveSudokuString(string sudokuStr)
        {
            Board bo = BoardScanner.getBoard(sudokuStr);


            Console.WriteLine("\nsudoku before:");
            bo.PrintBoard();
            Console.WriteLine(sudokuStr);

            bool solved = Solve(bo);

            Console.WriteLine("\nsudoku after:");
            bo.PrintBoard();

            return solved;

        }


        
        /// <summary>
        /// the main method that ask from the user a certain sudoku string and tries to solve it 
        /// throws ecxeption if input is invalid 
        /// prints to the console if solved or not
        /// </summary>
        public static void main1()
        {
            bool finished_playing = false;
            while (!finished_playing)
            {
                try
                {
                    show_menu();
                    ISudokuReader reader = get_choice();
                    string sudokuStr = reader.Input();
                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    bool solved = solveSudokuString(sudokuStr);
                    stopwatch.Stop();
                    TimeSpan executionTime = stopwatch.Elapsed;

                    reader.Output(solved, executionTime, sudokuStr);

                }
                catch (exit ex)
                {
                    Console.WriteLine("\nExiting the program.");
                    finished_playing = true;
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}\n");
                }
            }
        }
        

        public static void Main(string[] args)
        {
            main1();
        }
    }
}
