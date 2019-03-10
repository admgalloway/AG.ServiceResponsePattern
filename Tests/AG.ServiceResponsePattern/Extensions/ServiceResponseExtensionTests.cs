using AG.ServiceResponsePattern.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using AG.ServiceResponsePattern.Extensions;

namespace AG.ServiceResponsePattern.Tests.Extensions
{
    [TestFixture]
    public class ServiceResponseExtensionTests
    {
        [Test]
        public void Test_That_WasSuccessful_Returns_True()
        {
            // Arrange
            var response = ServiceResponseFactory.SuccessfulResponse();

            // Act
            var successful = response.WasSuccessful;

            // Assert
            Assert.AreEqual(true, successful);
        }

        [Test]
        public void Test_That_WasSuccessful_Returns_False()
        {
            // Arrange
            var response = ServiceResponseFactory.FailedResponse();

            // Act
            var successful = response.WasSuccessful;

            // Assert
            Assert.AreEqual(false, successful);
        }

        [Test]
        public void Test_That_WasUnsuccessful_Returns_True()
        {
            // Arrange
            var response = ServiceResponseFactory.FailedResponse();

            // Act
            var successful = response.WasUnsuccessful;

            // Assert
            Assert.AreEqual(true, successful);
        }

        [Test]
        public void Test_That_WasUnsuccessful_Returns_False()
        {
            // Arrange
            var response = ServiceResponseFactory.SuccessfulResponse();

            // Act
            var successful = response.WasUnsuccessful;

            // Assert
            Assert.AreEqual(false, successful);
        }

        [Test]
        public void Test_That_WasSuccessful_Sets_Out_Param()
        {
            // Arrange
            var testResponseValue = "congrats";
            var response = ServiceResponseFactory.SuccessfulResponse(testResponseValue);
            // Act
            response.WasSuccessful(out var responseOutValue);

            // Assert
            Assert.AreEqual(testResponseValue, responseOutValue);
        }

        [Test]
        public void Test_That_Add_Single_Adds_Error_To_List()
        {
            // Arrange
            var errors = new List<ServiceResponseError>();
            var errorField = "username";
            var errorCode = "Mandatory";

            // Act
            errors.Add(errorField, errorCode);

            // Assert
            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(errors.First().FieldName, errorField);
            Assert.AreEqual(errors.First().ErrorCode, errorCode);
        }

    }
}