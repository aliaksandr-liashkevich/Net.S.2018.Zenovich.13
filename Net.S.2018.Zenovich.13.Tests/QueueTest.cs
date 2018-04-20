using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.S._2018.Zenovich._13.Queue;

namespace Net.S._2018.Zenovich._13.Tests
{
    [TestClass]
    public class QueueTest
    {
        private IUserQueue<int> queue;

        [TestInitialize]
        public void TestInitialize()
        {
            queue = new UserQueue<int>();
        }

        [TestMethod]
        public void Enqueue_Count2()
        {
            // arrange
            queue.Enqueue(1);
            queue.Enqueue(2);
            int expected = 2;

            // act
            int actual = queue.Count;

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Dequeue_AreEqual100()
        {
            // arrange
            int expected = 100;
            queue.Enqueue(expected);

            // act
            int actual = queue.Dequeue();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Enumerator_3Elements()
        {
            // arrange
            List<int> expected = new List<int>()
            {
                1, 2, 3
            };
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            // act
            var actual = new List<int>(queue);

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Enqueue_ArgumentNullException()
        {
            // arrange
            IUserQueue<string> queue = new UserQueue<string>();
            string element = null;

            // act
            queue.Enqueue(element);
        }
    }
}
