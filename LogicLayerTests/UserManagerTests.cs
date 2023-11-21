using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataObjects;
using LogicLayer;
using DataAccessLayerInterfaces;
using DataAccessLayerFakes;

namespace LogicLayerTests
{
    [TestClass]
    public class UserManagerTests
    {
        UserManager userManager = null;

        [TestInitialize]

        public void TestSetup()
        {
            userManager = new UserManager(new UserAccessorFakes());
        }

        [TestMethod]
        public void TestAuthenticateUserPassesWithCorrectUsernameAndPasswordHash()
        {
            // arrange
            const string email = "tess@company.com";
            const string password = "newuser";
            int expectedResult = 999999;
            int actualResult = 0;

            // act
            User tessUser = userManager.LoginUser(email, password);
            actualResult = tessUser.EmployeeID;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateUserFailsWithInCorrectUsername()
        {
            // arrange
            const string email = "xtess@company.com";
            const string password = "newuser";

            // act
            User tessUser = userManager.LoginUser(email, password);

            // assert
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateUserFailsWithInCorrectPassword()
        {
            // arrange
            const string email = "tess@company.com";
            const string password = "xnewuser";

            // act
            User tessUser = userManager.LoginUser(email, password);

            // assert
        }

        public void TestGetSHA256ReturnsCorrectHashValue()
        {
            // Arrange
            const string source = "newuser";
            const string expectedResult =
                "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
            string result = "";

            // Act
            result = userManager.HashSha256(source);


            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetSHA256ThrowsArgumentNullExceptionForMissingInput()
        {
            // Arrange
            const string source = "";
            // no result expected, this should throw an exception

            // Act
            userManager.HashSha256(source);


            // Assert
            // nothing to assert

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetSHA256ThrowsArgumentNullExceptionForEmptyString()
        {
            // Arrange
            const string source = "";
            // no result expected, this should throw an exception

            // Act
            userManager.HashSha256(source);

            // Assert
            // nothing to assert
        }
    }
}
