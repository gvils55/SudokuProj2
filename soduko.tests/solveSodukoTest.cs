﻿using NUnit.Framework;
using SodukoProject.sudoku_solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soduko.tests
{
    public class SudokuTests
    {
        private Algorithm algo;
        private bool isSolved;


        [SetUp]
        public void Setup()
        {
            algo = new Algorithm();
        }


        [Test]
        [TestCase("800000000003600000070090200050007000000045700000100030001000068008500010090000400")]
        [TestCase("1002240104120143")]
        [TestCase("530070000600195000098000060800060003400803001700020006060000280000419005000080079")]
        [TestCase("002008050000040600000630000050000070300010004010000080000076000006020000040300900")]
        [TestCase("004300209005009001070060043006002087190007400050083000600000105003508690042910300")]
        [TestCase("600120384008459072000006005000264030070080006940003000310000050089700000502000190")]
        [TestCase("008020900040800070900003600001700050300080002060005300006900004070001080002060500")]
        public void solve_sudoku_returnsTrue(string sodukoStr)
        {
            isSolved = Algorithm.solveSudokuString(sodukoStr);
            Assert.IsTrue(isSolved);
        }


        [Test]
        [TestCase("003001000800000000051009060080002900007000080200040503600900000002084000410050607")]
        [TestCase("200900000000000060000001000502600407000004100000098023000003080005010000007000000")]
        [TestCase("00000000000000@00000000000000000000000000000000000000000000000000000000000000@00000000000000000000000000000000000000000000000000000000000000@000000000000000000000000000000000000000000000000000@0000000000000000000@0000000000000000000@00000000000000000000001")]
        public void solve_sudoku_returnsFalse(string sodukoStr)
        {
            isSolved = Algorithm.solveSudokuString(sodukoStr);
            Assert.IsFalse(isSolved);
        }
    }
}