using CircularQueue.Lib;
using NUnit.Framework;
using System;

namespace CircularQueue.Test
{
    public class CircularQueueTests
    {
        private CircularQueue<int> _queue;

        [SetUp]
        public void Setup()
        {
            _queue = new CircularQueue<int>(5);
        }

        [Test]
        public void TestIsEmptyShouldReturnTrueAfterNewingUp()
        {
            //Arrange

            //Act
            var isEmpty = _queue.IsEmpty;

            //Assertion
            Assert.IsTrue(isEmpty);
        }

        [Test]
        public void TestIsFullShouldReturnFalseAfterNewingUp()
        {
            //Arrange

            //Act
            var isFull = _queue.IsFull;

            //Assertion
            Assert.IsFalse(isFull);
        }


        [Test]
        public void TestCountShouldReturnZeroForAnEmptyQueue()
        {
            //Arrange

            //Act
            var count = _queue.Count;

            //Assertion
            Assert.AreEqual(0, count);
        }

        [Test]
        public void TestEnqueueCountShouldBeOneAfterASingleEnqueue()
        {
            //Arrange

            //Act
            _queue.Enqueue(1);

            //Assertion
            Assert.AreEqual(1, _queue.Count);
        }

        [Test]
        public void TestEnqueueIsEmptyShouldReturnFalse()
        {
            //Arrange

            //Act
            _queue.Enqueue(1);

            //Assertion
            Assert.IsFalse(_queue.IsEmpty);
        }

        [Test]
        public void TestFrontShouldReturnOneAfterEnqueueOne()
        {
            //Arrange

            //Act
            _queue.Enqueue(1);

            //Assertion
            Assert.AreEqual(1, _queue.Front());
        }

        [Test]
        public void TestBackShouldReturnTwoAfterEnqueueThreeItems()
        {
            //Arrange

            //Act
            for (var i = 0; i < 3; ++i) _queue.Enqueue(i);

            //Assertion
            Assert.AreEqual(2, _queue.Back());
        }

        [Test]
        public void TestEnqueueShouldThrowExceptionWhenEnqueueBecauseFull()
        {
            //Arrange

            int i;

            //Act
            for (i = 0; i < 5; ++i) _queue.Enqueue(i);

            //Assert
            Assert.Throws<Exception>(() =>
            {
                _queue.Enqueue(i);
            });
        }
        [Test]
        public void TestEnqueueShouldReturnSixVerifyingWrapAround()
        {
            //Arrange

            int i;
            for (i = 0; i < 5; ++i) _queue.Enqueue(i);

            //Act
            _queue.Dequeue();
            _queue.Enqueue(5);


            //Assert
            Assert.AreEqual(5, _queue.Back());
        }
        
        [Test]
        public void TestResizeShouldReturnZeroForFront()
        {
            //Arrange
            for (var i = 0; i < 5; ++i) _queue.Enqueue(i);
            //Act
            _queue.Resize(10);

            //Assertion
            Assert.AreEqual(0, _queue.Front());
        }
        [Test]
        public void TestResizeShouldReturnFiveForBack()
        {
            //Arrange
            for (var i = 0; i < 5; ++i) _queue.Enqueue(i);
            //Act
            _queue.Resize(10);
            _queue.Enqueue(5);

            //Assertion
            Assert.AreEqual(5, _queue.Back());
        }

        [Test]
        public void TestResizeShouldTruncateReturnThreeForCount()
        {
            //Arrange
            for (var i = 0; i < 5; ++i) _queue.Enqueue(i);
            //Act
            _queue.Resize(3);

            //Assertion
            Assert.AreEqual(3, _queue.Count);
        }
        [Test]
        public void TestResizeShouldTruncateReturnTrue()
        {
            //Arrange
            for (var i = 0; i < 5; ++i) _queue.Enqueue(i);
            //Act
            _queue.Resize(3);

            //Assertion
            Assert.IsTrue(_queue.IsFull);
        }

        [Test]
        public void TestResizeShouldReturnExceptionForResizeOfSameLength()
        {
            //Arrange

            //Act
            

            //Assertion
            Assert.Throws<Exception>(() =>
            {
                _queue.Resize(5);
            });
        }

        [Test]
        public void TestDequeueShouldReturnTwoAfterDequeue()
        {
            //Arrange
            _queue.Enqueue(2);
            _queue.Enqueue(3);
            //Act

            //Assert
            Assert.AreEqual(2, _queue.Dequeue());
        }
        [Test]
        public void TestDequeueShouldReturnThreeForFrontAfterDequeue()
        {
            //Arrange
            _queue.Enqueue(2);
            _queue.Enqueue(3);
            //Act
            _queue.Dequeue();
            //Assert
            Assert.AreEqual(3, _queue.Front());
        }
        [Test]
        public void TestDequeueShouldReturnExceptionAfterDequeueOnEmpty()
        {
            //Arrange
            //Act

            //Assert
            Assert.Throws<Exception>(() =>
            {
                _queue.Dequeue();
            });
        }
        [Test]
        public void TestFrontShouldThrowExceptionWhenQueueIsEmpty()
        {
            //Arrange


            //Assert
            Assert.Throws<Exception>(() =>
            {
                _queue.Front();
            });
        }
    }
}