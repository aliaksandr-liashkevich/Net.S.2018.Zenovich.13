using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.S._2018.Zenovich._13.BinarySearchTree;

namespace Net.S._2018.Zenovich._13.Tests
{
    [TestClass]
    public class UserBinarySearchTreeTest
    {
        [TestMethod]
        public void PreOrder_ExpectedIntValues()
        {
            // arrange
            int[] input = { 33, 31, 35, 13, 17, 18, 15, 3 };
            int[] expected = { 19, 13, 3, 17, 15, 18, 33, 31, 35 };

            UserBinarySearchTree<int> tree = new UserBinarySearchTree<int>(19);

            foreach (var item in input)
            {
                tree.AddNode(item);
            }

            // act
            int index = 0;
            foreach (var item in tree.PreOrder())
            {
                // assert
                Assert.AreEqual(expected[index], item);
                index++;
            }
        }

        [TestMethod]
        public void Inorder_ExpectedPoints()
        {
            // arrange
            Point[] expected = new Point[]
            {
                new Point(0, 0),
                new Point(1, 1),
                new Point(2, 2),
                new Point(3, 3)
            };
            UserBinarySearchTree<Point> tree = new UserBinarySearchTree<Point>(new Point(0,0), new CoordinatesBasedComparer());

            for (int i = 1; i < expected.Length; i++)
            {
                tree.AddNode(expected[i]);
            }

            // act
            int index = 0;
            foreach (var item in tree.Inorder())
            {
                // assert
                Assert.AreEqual(expected[index], item);
                index++;
            }
        }

        public class CoordinatesBasedComparer : IComparer<Point>
        {

            public int Compare(Point x, Point y)
            {
                if ((x.X == y.X) && (x.Y == y.Y))
                    return 0;
                if ((x.X < y.X) || ((x.X == y.X) && (x.Y < y.Y)))
                    return -1;

                return 1;
            }
        }

        [TestMethod]
        public void Inorder_ExpectedStrings()
        {
            UserBinarySearchTree<string> tree = new UserBinarySearchTree<string>("0");
            List<string> expected = new List<string>();
            expected.Add("0");

            for (int i = 1; i < 10; i++)
            {
                tree.AddNode(i.ToString());
                expected.Add(i.ToString());
            }

            int index = 0;
            foreach (var item in tree.Inorder())
            {
                // assert
                Assert.AreEqual(expected[index], item);
                index++;
            }
        }
    }
}
