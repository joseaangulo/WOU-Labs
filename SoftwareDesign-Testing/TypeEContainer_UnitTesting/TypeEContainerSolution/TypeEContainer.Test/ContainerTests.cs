using System;
using Container.Lib;
using NUnit.Framework;
using static System.Exception;


namespace Container.Tests
{
    public class ContainerTests
    {

        private Container<int> _container;
        
        [SetUp]
        public void Setup()
        {
            _container = new Container<int>();
        }

        [Test]
        public void TestInsertShouldAddFiveToContainer()
        {

            //Act
            _container.Insert(5);
            var exists = _container.Exists(5);

            //Assert
            Assert.IsTrue(exists);
        }
        [Test]
        public void TestInsertShouldThrowExceptionForContainerFull()
        {

            //Act
            _container.Insert(1);
            _container.Insert(2);
            _container.Insert(3);
            _container.Insert(4);
            _container.Insert(5);

            //Assert
            Assert.Throws<Exception>(() =>
            {
                _container.Insert(6);
            });
        }
        /*[Test]
        public void TestInsertShouldThrowExceptionForOverFlow()
        {

            //Act

            //Assert
            Assert.Throws<Exception>(() =>
            {
                _container.Insert();
            });
        }*/

        [Test]
        public void TestExistsShouldReturnTrueForFive()
        {
            //Arrange
            _container.Insert(5);

            //Act
            var exists = _container.Exists(5);

            //Assert
            Assert.IsTrue(exists);
        }
        [Test]
        public void TestExistsShouldReturnFalseForEmptyContainer()
        {
            //Arrange

            //Act
            var exists = _container.Exists(5);

            //Assert
            Assert.IsFalse(exists);
        }
        [Test]
        public void TestExistsShouldReturnFalseForNotInContainer()
        {
            //Arrange
            _container.Insert(5);

            //Act
            var exists = _container.Exists(7);

            //Assert
            Assert.IsFalse(exists);
        }
        [Test]
        public void TestDeleteShouldRemoveAllOccurrencesFromContainer()
        {
            //Arrange
            _container.Insert(7);

            //Act
            _container.Delete(7);
            var exists = _container.Exists(7);

            //Assert
            Assert.IsFalse(exists);
        }
        [Test]
        public void TestDeleteShouldThrowExceptionOnEmptyContainer()
        {
            //Arrange
            
            //Act

            //Assert
            Assert.Throws<Exception>(() =>
            {
                _container.Delete(7);
            });
        }
        [Test]
        public void TestDeleteShouldThrowExceptionOnContainerWithoutItem()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<Exception>(() =>
            {
                _container.Delete(7);
            });
        }
    }
}