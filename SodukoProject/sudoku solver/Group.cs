using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoProject.sudoku_solver
{
    public class Group
    {
        public string GroupKind { get; private set; }
        public int GroupNum { get; private set; }
        public List<Square> Array { get; private set; }
        public List<string> MissingNums { get; private set; }



        public Group(string groupKind, int groupNum, Board board)
        {
            GroupKind = groupKind;
            GroupNum = groupNum;
            Array = new List<Square>();

            if (GroupKind == "r")
            {
                for (int i = 0; i < board.Dimension; i++)
                {
                    Square sq = board.Squares[GroupNum,i];
                    Array.Add(sq);
                }
            }
            else if (GroupKind == "c")
            {
                for (int i = 0; i < board.Dimension; i++)
                {
                    Square sq = board.Squares[i,GroupNum];
                    Array.Add(sq);
                }
            }
            else
            {
                int boxLen = (int)Math.Sqrt(board.Dimension);
                int startRow = (GroupNum / boxLen) * boxLen;
                int startCol = (GroupNum % boxLen) * boxLen;

                for (int i = startRow; i < startRow + boxLen; i++)
                {
                    for (int j = startCol; j < startCol + boxLen; j++)
                    {
                        Square sq = board.Squares[i,j];
                        Array.Add(sq);
                    }
                }
            }

            MissingNums = new List<string>();
            for (int i = 0; i < board.Dimension; i++)
            {
                MissingNums.Add((i + 1).ToString());
            }

            SetMissing();
        }

        /// <summary>
        /// sets the the attribute MissingNums ro b a list that that contains the options that are not written on the Array
        /// </summary>
        public void SetMissing()
        {
            foreach (Square sq in Array)
            {
                if (sq.Symbol != "0")
                {
                    MissingNums.Remove(sq.Symbol);
                }
            }
        }

        /// <summary>
        /// prints the options that are missing in the Array
        /// </summary>
        public void PrintGroup()
        {
            Console.WriteLine("kind: " + GroupKind + " num: " + GroupNum);
            Console.WriteLine(string.Join(", ", MissingNums));
        }


        /// <summary>
        /// a method that receives a number and checks MisssingNums contains num
        /// </summary>
        /// <param name="num"></param>
        /// <returns>true if num is not in MissingNums, else false</returns>
        public bool IsNumInGroup(string num)
        {
            return !MissingNums.Contains(num);
        }


        /// <summary>
        /// this method removes opt from every empty square in the group(if the square has opt as an option)
        /// </summary>
        /// <param name="opt"></param> an option symbol
        public void EliminateOptionFromGroup(string opt)
        {
            foreach (Square sq in Array)
            {
                if (sq.Options.Contains(opt))
                {
                    sq.Options.Remove(opt);
                }
            }

            MissingNums.Remove(opt);
        }


        /// <summary>
        /// this method goes through each square in group and checks if there is a square
        /// is the only square that contains opt as an option
        /// </summary>
        /// <param name="opt"></param> an option symbol
        /// <returns>the position of square that is the only one has opt as an option, if there isnt one
        /// return (-1,-1) as the position</returns>
        public Tuple<int, int> SingleSquareWithThisOption(string opt)
        {
            int rowPos = -1;
            int colPos = -1;
            int count = 0;

            foreach (Square sq in Array)
            {
                if (sq.Symbol == "0" && sq.Options.Contains(opt))
                {
                    count++;
                    rowPos = sq.Row;
                    colPos = sq.Col;
                }
                if (count > 1)
                {
                    break;
                }
            }

            if (count != 1)
            {
                rowPos = -1;
                colPos = -1;
            }

            return new Tuple<int, int>(rowPos, colPos);
        }


        /// <summary>
        /// a method that retrieves opt to MissingNums
        /// </summary>
        /// <param name="opt"></param> an option symbol
        public void RetrieveOptToMissing(string opt)
        {
            MissingNums.Add(opt);
        }


        /// <summary>
        /// a method that checks if there are numbers that apear in the group more than once
        /// </summary>
        /// <returns>true if there are duplicates, else false</returns>
        public bool areThereDuplicates()
        {
            foreach (Square sq in Array)
            {
                string symbol = sq.Symbol;
                if (symbol != "0")
                {
                    int count = 0;
                    count = countDups(symbol);
                    if ( count > 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// a method that counts the amount of the given number in the group
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public int countDups(string symbol)
        {
            int count = 0;
            foreach (Square sq in Array)
            {
                if(sq.Symbol == symbol)
                {
                    count++;
                }
            }
            return count;
        }

    }
}
