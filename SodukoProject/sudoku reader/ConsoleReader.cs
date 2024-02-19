using System;
using System.Text;
using System.Threading.Tasks;

namespace SodukoProject.sudoku_reader
{

    public class ConsoleReader : ISudokuReader
    {
        public string Input()
        {

            Console.WriteLine("Enter a sudoku string:");
            string content = Console.ReadLine();
            return content;
        }

        public void Output(bool solved, TimeSpan executionTime,string sudokuStr)
        {
            Console.WriteLine(sudokuStr);
            if (solved)
            {
                Console.WriteLine();
                Console.WriteLine($"Solved in: {executionTime.TotalSeconds} seconds");
            }
            else
            {
                Console.WriteLine($"Failed to solve in: {executionTime.TotalSeconds} seconds");
            }

            // Implement the logic to display the Sudoku board
            // Example implementation:
            // ...
        }

        public static string ConvertToString(string[,] sudokuBoard)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < sudokuBoard.GetLength(0); i++)
            {
                for (int j = 0; j < sudokuBoard.GetLength(1); j++)
                {
                    result.Append(sudokuBoard[i, j]);
                }
            }

            return result.ToString();
        }
    }
}
