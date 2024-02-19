using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoProject.sudoku_reader
{
    public interface ISudokuReader
    {
        string Input();  // Abstract method for reading a Sudoku board
        void Output(bool solved, TimeSpan executionTime, string sudokuStr);    // Abstract method for displaying a Sudoku board

    }
}
