using AG.Common.ServiceResponsePattern.Enums;
using AG.Common.ServiceResponsePattern.Models;
using System.Collections.Generic;

namespace AG.Common.ServiceResponsePattern.Extensions
{
    /// <summary>Set of methods to simplify the creation of service response objects.</summary>
    public static class ServiceResponseFactory
    {
        #region Without type params

        /// <summary>Create a ServiceResponse for a request which completed successfully.</summary>
        public static ServiceResponse SuccessfulResponse()
        {
            return Response(ServiceResponseResult.Successful);
        }

        /// <summary>Create a ServiceResponse for a request which failed because a requested resource could not be found.</summary>
        public static ServiceResponse FailedNotFoundResponse()
        {
            return Response(ServiceResponseResult.Failed, ServiceResponseErrorCode.NotFound);
        }

        /// <summary>Create a ServiceResponse for a request which failed because the caller was not authorized.</summary>
        public static ServiceResponse FailedAuthorizationResponse()
        {
            return Response(ServiceResponseResult.Failed, ServiceResponseErrorCode.AuthorizationError);
        }

        /// <summary>Create a ServiceResponse for a request which failed because the caller could not be authenticated.</summary>
        public static ServiceResponse FailedAuthenticationResponse()
        {
            return Response(ServiceResponseResult.Failed, ServiceResponseErrorCode.AuthenticationError);
        }

        /// <summary>Create a ServiceResponse for a request which failed because of input from the caller.</summary>
        public static ServiceResponse FailedValidationResponse(IList<ServiceResponseError> serviceResponseErrors)
        {
            return Response(ServiceResponseResult.Failed, ServiceResponseErrorCode.ValidationError, serviceResponseErrors);
        }

        /// <summary>Create a ServiceResponse for a request which failed because of input from the caller.</summary>
        /// <remarks>This method only returns a single message in the Errors collection.</remarks>
        public static ServiceResponse FailedValidationResponse(string fieldName, string errorCode)
        {
            var serviceResponseErrors = new List<ServiceResponseError> { new ServiceResponseError(fieldName, errorCode) };
            return FailedValidationResponse(serviceResponseErrors);
        }

        /// <summary>Create a ServiceResponse where the request has failed due to a system fault such as a timeout, serialisation error, etc.</summary>
        public static ServiceResponse FailedResponse()
        {
            return Response(ServiceResponseResult.Failed, ServiceResponseErrorCode.SystemError);
        }

        /// <summary>Create a Failed ServiceResponse using the supplied error code.</summary>
        public static ServiceResponse FailedResponse(ServiceResponseErrorCode errorCode)
        {
            return Response(ServiceResponseResult.Failed, errorCode);
        }

        /// <summary>Generic parameterised method for create all responses with a type param.</summary>
        private static ServiceResponse Response(ServiceResponseResult result, ServiceResponseErrorCode errorCode = ServiceResponseErrorCode.NoError, IList<ServiceResponseError> serviceResponseErrors = null)
        {
            return new ServiceResponse
            {
                Result = result,
                ErrorCode = errorCode,
                Errors = serviceResponseErrors ?? new List<ServiceResponseError>()
            };
        }

        #endregion

        #region With type params

        /// <summary>Create a ServiceResponse for a request which completed successfully, and has content to return.</summary>
        public static ServiceResponse<T> SuccessfulResponse<T>(T content)
        {
            return new ServiceResponse<T> { Result = ServiceResponseResult.Successful, Content = content };
        }

        /// <summary>Create a ServiceResponse for a request which failed because a requested resource could not be found.</summary>
        public static ServiceResponse<T> FailedNotFoundResponse<T>()
        {
            return Response<T>(ServiceResponseResult.Failed, ServiceResponseErrorCode.NotFound);
        }

        /// <summary>Create a ServiceResponse for a request which failed because the called was not authorized.</summary>
        public static ServiceResponse<T> FailedAuthorizationResponse<T>()
        {
            return Response<T>(ServiceResponseResult.Failed, ServiceResponseErrorCode.AuthorizationError);
        }

        /// <summary>Create a ServiceResponse for a request which failed because the called could not be authenticated.</summary>
        public static ServiceResponse<T> FailedAuthenticationResponse<T>()
        {
            return Response<T>(ServiceResponseResult.Failed, ServiceResponseErrorCode.AuthenticationError);
        }

        /// <summary>Create a ServiceResponse for a request which failed because of input from the caller.</summary>
        public static ServiceResponse<T> FailedValidationResponse<T>(IList<ServiceResponseError> validationErrors)
        {
            return Response<T>(ServiceResponseResult.Failed, ServiceResponseErrorCode.ValidationError, validationErrors);
        }

        /// <summary>Create a ServiceResponse for a request which failed because of input from the caller.</summary>
        /// <remarks>This method only returns a single message in the Errors collection.</remarks>
        public static ServiceResponse<T> FailedValidationResponse<T>(string fieldName, string validationErrorCode)
        {
            var serviceResponseErrors = new List<ServiceResponseError> { new ServiceResponseError(fieldName, validationErrorCode) };
            return FailedValidationResponse<T>(serviceResponseErrors);
        }

        /// <summary>Create a Failed ServiceResponse using the supplied error code.</summary>
        public static ServiceResponse<T> FailedResponse<T>(ServiceResponseErrorCode errorCode)
        {
            return Response<T>(ServiceResponseResult.Failed, errorCode);
        }

        /// <summary>Create a ServiceResponse where the request has failed due to a system fault such as a timeout, serialisation error, etc.</summary>
        public static ServiceResponse<T> FailedResponse<T>()
        {
            return Response<T>(ServiceResponseResult.Failed, ServiceResponseErrorCode.SystemError);
        }

        /// <summary>Generic parameterised method for create all responses with a type param</summary>
        private static ServiceResponse<T> Response<T>(ServiceResponseResult result, ServiceResponseErrorCode errorCode = ServiceResponseErrorCode.NoError, IList<ServiceResponseError> serviceResponseErrors = null)
        {
            return new ServiceResponse<T>
            {
                Result = result,
                ErrorCode = errorCode,
                Errors = serviceResponseErrors ?? new List<ServiceResponseError>()
            };
        }

        #endregion

    }
}
