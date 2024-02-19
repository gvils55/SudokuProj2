using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoProject.sudoku_solver
{
    public class Square
    {
        public string Symbol { get;  set; }
        public int Row { get;  set; }
        public int Col { get;  set; }
        public List<string> Options { get;  set; }

        public Square(string ch, int r, int c)
        {
            Symbol =ch;
            Row = r;
            Col = c;
            Options = new List<string>();
        }

        public void print_opt()
        {
            for(int i = 0; i < Options.Count; i++)
                Console.WriteLine(Options[i]);
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }
}
