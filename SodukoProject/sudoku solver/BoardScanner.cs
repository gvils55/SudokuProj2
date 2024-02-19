using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodukoProject.sudoku_solver.ecxeptions;

namespace SodukoProject.sudoku_solver
{
    public class BoardScanner
    {
        /// <summary>
        /// a method that receives a string input from the user and checks if the string is valid based on the rules of sudoku
        /// throws an adjusted exception when the strinf is not valid
        /// </summary>
        /// <param name="grid"></param> a string that represents a sudoko board
        /// <exception cref="grid_isnt_squared"></exception>
        /// <exception cref="no_squared_root"></exception>
        /// <exception cref="invalid_symbols"></exception>
        public static void check_vaildity(string grid)
        {
            double dimension_length = Math.Sqrt(grid.Length);
            if (dimension_length % 1 != 0)
            {
                throw new grid_isnt_squared("grid isn't squared, rows and cols must be equal");
            }
            
            double root_length = Math.Sqrt(dimension_length);
            if (root_length % 1 != 0)
            {
                throw new no_squared_root("the dimension of grid doesn't have a squared root");

            }
            int new_len = (int)dimension_length;
            foreach (char c in grid)
            {
                if(c > 48 + new_len || c< 48)
                {
                    throw new invalid_symbols("grid contains invalid symbols");
                }
            }
        }

        /// <summary>
        /// a method that receives a string that represents a sudoku board
        /// and makes a new Board object based on the sudoku string 
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static Board getBoard(string grid)
        {
            check_vaildity(grid);
            Board bo = new Board(grid);
            bo.passVadality();
            return bo;
        }
    }
}
