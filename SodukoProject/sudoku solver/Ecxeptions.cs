using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoProject.sudoku_solver
{
    public class ecxeptions
    {
        // Custom exception class
        public class grid_isnt_squared : Exception
        {
            // Constructors
            public grid_isnt_squared() : base() { }

            public grid_isnt_squared(string message) : base(message) { }

            public grid_isnt_squared(string message, Exception innerException) : base(message, innerException) { }

            // You can add more members or custom logic as needed
        }
    }


    // Custom exception class
    public class no_squared_root : Exception
    {
        // Constructors
        public no_squared_root() : base() { }

        public no_squared_root(string message) : base(message) { }

        public no_squared_root(string message, Exception innerException) : base(message, innerException) { }

        // You can add more members or custom logic as needed
    }

    // Custom exception class
    public class invalid_symbols : Exception
    {
        // Constructors
        public invalid_symbols() : base() { }

        public invalid_symbols(string message) : base(message) { }

        public invalid_symbols(string message, Exception innerException) : base(message, innerException) { }

        // You can add more members or custom logic as needed
    }

    public class unsolvble_sudoku : Exception
    {
        // Constructors
        public unsolvble_sudoku() : base() { }

        public unsolvble_sudoku(string message) : base(message) { }

        public unsolvble_sudoku(string message, Exception innerException) : base(message, innerException) { }

        // You can add more members or custom logic as needed
    }

    public class exit : Exception
    {
        // Constructors
        public exit() : base() { }

        public exit(string message) : base(message) { }

        public exit(string message, Exception innerException) : base(message, innerException) { }

        // You can add more members or custom logic as needed
    }

    public class duplicate_in_board : Exception
    {
        // Constructors
        public duplicate_in_board() : base() { }

        public duplicate_in_board(string message) : base(message) { }

        public duplicate_in_board(string message, Exception innerException) : base(message, innerException) { }

        // You can add more members or custom logic as needed
    }
}

