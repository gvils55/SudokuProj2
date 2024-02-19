using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static SodukoProject.sudoku_solver.ecxeptions;

namespace SodukoProject.sudoku_solver
{
    public class Board
    {

        public Square[,] Squares { get;  set; }
        public int Dimension { get; }
        public  string[] OptionsInBoard { get;}
        public List<Square> ChangedSquares { get; private set; }
        public List<Square> EmptySquares { get; private set; }
        public Group[] Rows { get; private set; }
        public Group[] Cols { get; private set; }
        public Group[] Blocks { get; private set; }


        
        public Board(string bo)
        {
            double dim1 = Math.Sqrt(bo.Length);
            Dimension = (int)dim1;
            Squares = new Square[Dimension, Dimension];
            OptionsInBoard = new string[Dimension];
            // Initialize the board with default Square objects
            for (int i = 0; i < Dimension; i++)
            {
                OptionsInBoard[i] = (i + 1).ToString();
                for (int j = 0; j < Dimension; j++)
                {
                    char ch = (char)bo[Dimension * i + j];
                    string str = ch.ToString();
                    Squares[i, j] = new Square(str, i, j);
                }
            }

            Rows = new Group[Dimension];
            Cols = new Group[Dimension];
            Blocks = new Group[Dimension];
            for(int i = 0; i < Dimension; ++i)
            {
                Group rowObj = new Group("r", i, this);
                Rows[i] = rowObj;
                Group colObj = new Group("c", i, this);
                Cols[i] = colObj;
                Group blockObj = new Group("b", i, this);
                Blocks[i] = blockObj;

            }

            ChangedSquares = new List<Square>();
            EmptySquares = GetEmptySquares();
        }


        /// <summary>
        /// this method prints the 2d array using the attribute squares
        ///the method prints the symbol of each square in his suitible position
        ///the method prints the board with lines as well so that the board will look like a real sudoku board
        /// </summary>
        public void PrintBoard()
        {
            Console.WriteLine();
            double squareRoot = Math.Sqrt(Dimension);
            for (int i = 0; i < Dimension; i++)
            {
                if (i % squareRoot == 0 && i != 0)
                {
                    Console.WriteLine(new string('-', Dimension * 2 + (int)squareRoot));
                }

                for (int j = 0; j < Dimension; j++)
                {
                    if (j % squareRoot == 0 && j != 0)
                    {
                        Console.Write("| ");
                    }

                    Console.Write(Squares[i, j].ToString() + " ");
                }

                Console.WriteLine();
            }
        }


        /// <summary>
        /// this method goes through each empty square in Squares and prints the option this specific square has
        /// </summary>
        public void PrintOptions()
        {
            foreach (Square empSq in EmptySquares)
            {
                Square sq = empSq;
                Console.WriteLine("sq: " + sq.Row + ", " + sq.Col);
                Console.WriteLine(string.Join(", ", sq.Options));
            }
        }


        /// <summary>
        /// this method goes through each square in Squares and checks which square is empty
        /// every empty square ia added a list of squares 
        /// </summary>
        /// <returns> the list of the empty squares</returns>
        public List<Square> GetEmptySquares()
        {
            List<Square> result = new List<Square>();
            for(int i = 0; i < Dimension; i++)
            {
                for(int j = 0; j < Dimension;j++)
                {
                    if (Squares[i,j].Symbol == "0")
                    {
                        result.Add(Squares[i,j]);
                    }
                }
            }
            return result;
        }



        /// <summary>
        /// this method checks if opt is in the specific group(based on the parameters groupNum and kind)
        /// </summary>
        /// <param name="groupNum"></param> num of group to check
        /// <param name="opt"></param> an option
        /// <param name="kind"></param> kind of group to check
        /// <returns>true if opt is in the group, else false </returns>
        public bool IfNumInThisGroup(int groupNum, string opt, string kind)
        {
            Group[] temp;

            if (kind == "r")
            {
                temp = Rows;
            }
            else if (kind == "c")
            {
                temp = Cols;
            }
            else
            {
                temp = Blocks;
            }

            Group currGroup = temp[groupNum];
            return currGroup.IsNumInGroup(opt);
        }


        /// <summary>
        /// this method receives a certain square(an empty square) and sets his options 
        /// </summary>
        /// <param name="sq"></param> a square
        public void SetOptionsToSquare(Square sq)
        {
            int boxLen = (int)Math.Sqrt(Dimension);
            int row = sq.Row;
            int col = sq.Col;

            for (int i = 0; i < OptionsInBoard.Length; i++)
            {
                string opt = OptionsInBoard[i];
                bool bool1 = IfNumInThisGroup(row, opt, "r");
                bool bool2 = IfNumInThisGroup(col, opt, "c");
                int boxNum = (row / boxLen) * boxLen + (col / boxLen);
                bool bool3 = IfNumInThisGroup(boxNum, opt, "b");

                if (!(bool1 || bool2 || bool3) && !sq.Options.Contains(opt))
                {
                    sq.Options.Add(opt);
                }
            }
        }


        /// <summary>
        /// this method through each empty square in Squares and sets the options of the certain square 
        /// </summary>
        public void SetOptionsToAll()
        {
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    Square sq = Squares[i,j];
                    if (sq.Symbol == "0")
                    {
                        SetOptionsToSquare(sq);
                    }
                }
            }
        }



        /// <summary>
        /// </summary>
        /// <returns>the first square in EmptySquares, if EmptySquares is empty returns null</returns>
        public Square FindEmptyPos()
        {
            if (EmptySquares.Count == 0)
            {
                return null;
            }
            else
            {
                return EmptySquares[0];
            }
        }


        /// <summary>
        /// this method receives a num and position and adds the num to the given position in Squares
        /// after it sets the board because of the addition of the num
        /// it updates EmptySquares and updates the options of the empty squares that are in the same col/row/block of the given position
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="num"></param>
        public void WriteToBoard(int row, int col, string num)
        {
            Square sq = Squares[row,col];
            sq.Symbol = num;
            sq.Options.Clear();
            EliminateConnectedOptions(row, col, num);
            //Console.WriteLine("added the num: " + num + " to:");
            //Console.WriteLine(row + ", " + col);
            ChangedSquares.Add(sq);
            EmptySquares.Remove(sq);
        }


        /// <summary>
        /// this method receives a num and position and removes this num from all of the empty squares
        /// that are in the same col/row/block of the given position, therefore, updates the squares options
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="num"></param>
        public void EliminateConnectedOptions(int row, int col, string num)
        {
            Group rowArr = Rows[row];
            Group colArr = Cols[col];
            int blockLen = (int)Math.Sqrt(Dimension);
            int blockNum = (row / blockLen) * blockLen + (col / blockLen);
            Group blockArr = Blocks[blockNum];

            rowArr.EliminateOptionFromGroup(num);
            colArr.EliminateOptionFromGroup(num);
            blockArr.EliminateOptionFromGroup(num);
        }



        /// <summary>
        /// a recursive method that goes through each square in EmptySquares and checks if a square
        /// has only one number in his options atrribute, if it has then this num wiil have to the squares symbol
        /// so it will write the symbol to board and increase count
        /// </summary>
        /// <param name="count"></param> an amount of squares that have been changed(will be 0 at first call)
        /// <returns>the amount of squares that have been changed</returns>
        public int NakedSingles(int count)
        {
            for (int i = 0; i < EmptySquares.Count; i++)
            {
                Square sq = EmptySquares[i];
                if (sq.Options.Count == 1)
                {
                    WriteToBoard(sq.Row, sq.Col, sq.Options[0]);
                    return NakedSingles(count + 1);
                }
            }
            return count;
        }



        /// <summary>
        /// this method goes through each number possible in the board and checks if there is a hidden single 
        /// of this num in the rows, columns and blocks of the board 
        /// </summary>
        /// <returns>the amount of squares that hvae been changed </returns>
        public int HiddenSingles()
        {
            int sum = 0;
            foreach (string opt in OptionsInBoard)
            {
                int n1 = HiddenSingleGroup(opt, 0, "r");
                int n2 = HiddenSingleGroup(opt, 0, "c");
                int n3 = HiddenSingleGroup(opt, 0, "b");
                sum += n1 + n2 + n3;
            }
            return sum;
        }


        /// <summary>
        /// a recursive method that goes through the group array(the group will be determained based on the kind)
        /// the method checks if there is a square that is the only square that contains num as an option a group
        /// if there is, the method will get the position this square and will write it to the board and will increase count
        /// </summary>
        /// <param name="num"></param> a number that is in numbr thatcan be in the board
        /// <param name="count"></param> count of empty squares that have been changed(first call will allways be 0)
        /// <param name="kind"></param> the kind of the groups to check
        /// <returns>the amount of squares that have been changed in the board </returns>
        public int HiddenSingleGroup(string num, int count, string kind)
        {
            Group[] objArray;

            if (kind == "r")
            {
                objArray = Rows;
            }
            else if (kind == "c")
            {
                objArray = Cols;
            }
            else
            {
                objArray = Blocks;
            }

            foreach (Group groupObj in objArray)
            {
                Tuple<int, int> position = groupObj.SingleSquareWithThisOption(num);
                int row = position.Item1;
                int col = position.Item2;

                if (row != -1 && col != -1)
                {
                    WriteToBoard(row, col, num);
                    return HiddenSingleGroup(num, count + 1, kind);
                }
            }

            return count;
        }


        /// <summary>
        /// a method that pops the last count changed squares and resets them
        /// those squares will be empty and their options will reset
        /// after the retreive of the squares the board will update the options of every empty square
        /// </summary>
        /// <param name="count"></param> amount of squares that nedd to be retrieved
        public void RetrieveChanges(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int lastIndex = ChangedSquares.Count - 1;
                if (lastIndex >= 0)
                {
                    Square sq = ChangedSquares[lastIndex];
                    ChangedSquares.RemoveAt(lastIndex);

                    int row = sq.Row;
                    int col = sq.Col;
                    string opt = sq.Symbol;

                    sq.Symbol = "0";
                    EmptySquares.Add(sq);
                    RetrieveToMissing(row, col, opt);
                }
            }

            SetOptionsToAll();
        }


        /// <summary>
        /// a method that adds the string opt to the missingNums attribute of group
        /// the method goes to every group that contains the given position and retrieves the given option
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="opt"></param>
        public void RetrieveToMissing(int row, int col, string opt)
        {
            Group rowObj = Rows[row];
            Group colObj = Cols[col];
            int boxLen = (int)Math.Sqrt(Dimension);
            int boxNum = (row / boxLen) * boxLen + (col / boxLen);
            Group boxObj = Blocks[boxNum];

            rowObj.RetrieveOptToMissing(opt);
            colObj.RetrieveOptToMissing(opt);
            boxObj.RetrieveOptToMissing(opt);
        }


        /// <summary>
        /// a method that combines all the symbols in Squares to a one long string
        /// </summary>
        /// <returns>the combined string</returns>
        public string array_to_string()
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {

                    result.Append(Squares[i, j].Symbol);
                }
            }
            return result.ToString();
        }


        /// <summary>
        /// a method that gooes through each group in the board and check if a certain group contains a duplicate of a certain num
        /// </summary>
        /// <exception cref="duplicate_in_board"></exception>
        public void passVadality()
        {
            for(int i = 0; i < Dimension; i++)
            {
                bool b1 = Rows[i].areThereDuplicates();
                bool b2 = Cols[i].areThereDuplicates();
                bool b3 = Blocks[i].areThereDuplicates();
                if (b1 || b2 || b3 == true)
                {
                    throw new duplicate_in_board("board is not valid, it contains duplicates in a certain group");

                }
            }
        }
    }
}
