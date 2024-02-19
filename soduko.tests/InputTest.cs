using SodukoProject.sudoku_solver;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SodukoProject.sudoku_solver;

namespace soduko.tests
{
    public class validityTests
    {
        private BoardScanner scanner;


        [SetUp]
        public void Setup()
        {
            scanner = new BoardScanner();
        }

        [Test]
        [TestCase("1")]
        [TestCase("800000000003600000070090200050007000000045700000100030001000068008500010090000400")]
        [TestCase("1032240134120143")]
        [TestCase("002008050000040600000630000050000070300010004010000080000076000006020000040300900")]
        [TestCase("530070000600195000098000060800060003400803001700020006060000280000419005000080079")]
        public void check_is_valid_Not_Throw_exception(string sodukoStr)
        {
            Board bo = BoardScanner.getBoard(sodukoStr);
            Assert.DoesNotThrow(() => BoardScanner.getBoard(sodukoStr));
        }

        [Test]
        [TestCase("00202805000004000630000050000070300010004111000080000076000006020000040300900")]
        [TestCase("5300700006001950000980000608000600034008030017000200060600002800004190050000")]
        [TestCase("5300abgx0006001950000i8000060800060003400803001700020006060000280000419005000080079")]
        [TestCase("1032240134120145")]
        [TestCase("1212")]
        public void check_is_valid_Throw_exception(string sodukoStr)
        {
            Assert.Throws<ArgumentException>(() => BoardScanner.getBoard(sodukoStr));
        }
    }
}