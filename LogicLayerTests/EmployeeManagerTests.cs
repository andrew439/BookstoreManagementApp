using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayerFakes;
using System.Collections.Generic;
using LogicLayer;
using DataObjects;
using System;

namespace LogicLayerTests
{
    [TestClass]
    public class EmployeeManagerTests
    {
        private EmployeeManager _employeeManager = null;

        [TestInitialize]
        public void TestSetup()
        {
        //    _employeeManager = new EmployeeManager(new DataAccessLayer.EmployeeAccessor());
            _employeeManager = new EmployeeManager(new EmployeeAccessorFakes());
        }

        [TestMethod]
        public void TestRetrieveAllActiveEmployeeVMsReturnsCorrectList()
        {
            // Arrange
            int expectedCount = 2;
            int actualCount = 0;

            // Act
            var employees = _employeeManager.RetrieveAllActiveEmployeeVMs();
            actualCount = employees.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
