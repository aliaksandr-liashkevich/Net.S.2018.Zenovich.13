using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.S._2018.Zenovich._13.Matrix;

namespace Net.S._2018.Zenovich._13.Tests
{
    [TestClass]
    public class MatrixTest
    {
        [TestMethod]
        public void CtorDiagonalMatrix_CheckCreation()
        {
            // arrange
            int[] array = new int[]
            {
                2,
                  5,
                    3
            };
            Func<int, int, int> func = (firstElement, secondElement) => firstElement + secondElement;

            // act
            SquareMatrix<int> diagonal = new DiagonalMatrix<int>(array, func);

            // assert
            int length = array.Length;
            for (int i = 0; i < length; i++)
            {
                Assert.AreEqual(diagonal[i, i], array[i]);
            }
        }

        [TestMethod]
        public void CtorSymmetricalMatrix_CheckCreation()
        {
            // arrange
            int[,] expected = 
            {
                {2, 5, 3},
                {5, 2, 1},
                {3, 1, 2}
            };

            int[] array = new int[]
            {
                2, 5, 3,
                   2, 1,
                      2
            };
            int length = 3;
            Func<int, int, int> func = (firstElement, secondElement) => firstElement + secondElement;

            // act
            SquareMatrix<int> symmetric = new SymmetricalMatrix<int>(array, length, func);

            // assert
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Assert.AreEqual(expected[i, j], symmetric[i, j]);
                }
            }
        }

        [TestMethod]
        public void SumMatrix()
        {
            // arrange
            int[,] expected =
            {
                {4, 5, 3},
                {5, 7, 1},
                {3, 1, 5}
            };

            int[] arrayDiagonal = new int[]
            {
                2,
                  5,
                    3
            };

            int[] arraySymmetrical = new int[]
            {
                2, 5, 3,
                   2, 1,
                      2
            };
            int length = 3;

            Func<int, int, int> func = (firstElement, secondElement) => firstElement + secondElement;

            // act
            SquareMatrix<int> diagonal = new DiagonalMatrix<int>(arrayDiagonal, func);
            SquareMatrix<int> symmetric = new SymmetricalMatrix<int>(arraySymmetrical, length, func);

            // assert
            SquareMatrix<int> squareMatrix = diagonal.Sum(symmetric);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Assert.AreEqual(expected[i, j], squareMatrix[i, j]);
                }
            }
        }
    }
}
