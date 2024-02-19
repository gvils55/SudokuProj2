using System;
using System.IO;


namespace SodukoProject.sudoku_reader
{
    public class FileReader : ISudokuReader
    {
        public string Input()
        {
            while (true)
            {
                Console.WriteLine("\nEnter the path to the file: ");
                string filePath = Console.ReadLine();

                try
                {
                    string content = ReadFileContent(filePath);
                    return content;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File not found. Please enter a valid file path.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    // You might want to decide whether to break the loop or continue based on the exception type.
                    // For simplicity, the loop continues in this example.
                }
            }
        }

        public void Output(bool solved, TimeSpan executionTime, string sudokuStr)
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
            string filePath = "C:/Users/Owner/source/repos/sudoku1/SudokuProject/sudoku reader/result.txt";

            try
            {
                File.WriteAllText(filePath, sudokuStr);
                Console.WriteLine("String successfully written to the file: " + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to the file: " + ex.Message);
            }
        }


        public string ReadFileContent(string filePath)
        {

            if (File.Exists(filePath))
            {
                // Read the content of the file
                return File.ReadAllText(filePath);
            }
            else
            {
                throw new FileNotFoundException("File not found.");
            }
        }
    }
}
