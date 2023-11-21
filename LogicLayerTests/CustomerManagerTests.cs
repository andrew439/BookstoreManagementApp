using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataAccessLayerFakes;
using LogicLayer;
using DataObjects;
using System;

namespace LogicLayerTests
{
    [TestClass]
    public class CustomerManagerTests
    {
        private CustomerManager _customerManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            //    _customerManager = new CustomerManager(new DataAccessLayer.CustomerAccessor());
            _customerManager = new CustomerManager(new CustomerAccessorFakes());
        }

        [TestMethod]
        public void TestRetrieveAllCustomers()
        {
            // Arrange
            int expectedCount = 2;
            int actualCount = 0;

            // Act
            var customers = _customerManager.RetrieveAllCustomers();
            actualCount = customers.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRetrieveAllActiveCustomersReturnsCorrectList()
        {
            // Arrange
            int expectedCount = 1;
            int actualCount = 0;

            // Act
            var employees = _customerManager.RetrieveAllActiveCustomers();
            actualCount = employees.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
