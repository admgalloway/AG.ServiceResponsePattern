using AG.ServiceResponsePattern.Enums;
using AG.ServiceResponsePattern.Extensions;
using AG.ServiceResponsePattern.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AG.ServiceResponsePattern.Tests.Extensions
{
    [TestFixture]
    public class ServiceResponseFactoryTypeTests
    {
        [Test]
        public void Test_That_Successful_Returns_Response()
        {
            // Arrange
            string testResponseText = "success test response";

            // Act
            var response = ServiceResponseFactory.SuccessfulResponse(testResponseText);

            // Assert
            Assert.IsTrue(response.Result == ServiceResponseResult.Successful);
            Assert.IsTrue(response.ErrorCode == ServiceResponseErrorCode.NoError);
            Assert.IsEmpty(response.Errors);
            Assert.AreEqual(testResponseText, response.Content);
        }

        [Test]
        public void Test_That_FailedAuthorization_Returns_Response()
        {
            // Arrange

            // Act
            var response = ServiceResponseFactory.FailedAuthorizationResponse<string>();

            // Assert
            Assert.IsTrue(response.Result == ServiceResponseResult.Failed);
            Assert.IsTrue(response.ErrorCode == ServiceResponseErrorCode.AuthorizationError);
            Assert.IsEmpty(response.Errors);
            Assert.IsNull(response.Content);
        }

        [Test]
        public void Test_That_FailedAuthentication_Returns_Response()
        {
            // Arrange

            // Act
            var response = ServiceResponseFactory.FailedAuthenticationResponse<string>();

            // Assert
            Assert.IsTrue(response.Result == ServiceResponseResult.Failed);
            Assert.IsTrue(response.ErrorCode == ServiceResponseErrorCode.AuthenticationError);
            Assert.IsEmpty(response.Errors);
            Assert.IsNull(response.Content);
        }

        [Test]
        public void Test_That_Failed_Returns_Response()
        {
            // Arrange

            // Act
            var response = ServiceResponseFactory.FailedResponse<string>();

            // Assert
            Assert.IsTrue(response.Result == ServiceResponseResult.Failed);
            Assert.IsTrue(response.ErrorCode == ServiceResponseErrorCode.SystemError);
            Assert.IsEmpty(response.Errors);
            Assert.IsNull(response.Content);
        }

        [Test]
        public void Test_That_FailedValidation_Returns_Response()
        {
            // Arrange
            var errorField = "username";
            var errorCode = "Mandatory";

            var validationErrors = new List<ServiceResponseError>{
                new ServiceResponseError(errorField, errorCode)
            };

            // Act
            var response = ServiceResponseFactory.FailedValidationResponse<string>(validationErrors);

            // Assert
            Assert.IsTrue(response.Result == ServiceResponseResult.Failed);
            Assert.IsTrue(response.ErrorCode == ServiceResponseErrorCode.ValidationError);
            Assert.IsNull(response.Content);

            Assert.AreEqual(response.Errors.Count, 1);
            Assert.AreEqual(response.Errors.First().FieldName, errorField);
            Assert.AreEqual(response.Errors.First().ErrorCode, errorCode);
        }

        [Test]
        public void Test_That_FailedValidation_Single_Returns_Response()
        {
            // Arrange
            var errorField = "username";
            var errorCode = "Mandatory";

            // Act
            var response = ServiceResponseFactory.FailedValidationResponse<string>(errorField, errorCode);

            // Assert
            Assert.IsTrue(response.Result == ServiceResponseResult.Failed);
            Assert.IsTrue(response.ErrorCode == ServiceResponseErrorCode.ValidationError);
            Assert.AreEqual(response.Errors.Count, 1);

            Assert.AreEqual(response.Errors.First().FieldName, errorField);
            Assert.AreEqual(response.Errors.First().ErrorCode, errorCode);
        }

    }
}