using AG.ServiceResponsePattern.Enums;
using System.Collections.Generic;

namespace AG.ServiceResponsePattern.Models
{
    /// <summary>A wrapper around service methods to inform callers if the request was successful or not. 
    /// All exceptions within the service call should be logged, and the overall outcome of the method reported back to callers via the Result property.
    /// This implimentation does not return content if the method was successful.
    /// </summary>
    public partial class ServiceResponse
    {
        /// <summary>Restrict creation of objects to internal project - consumers should use the ServiceResponseFactory to reliably populate object.</summary>
        internal ServiceResponse() { }

        /// <summary>Indicates if the request was successful or not.</summary>
        public ServiceResponseResult Result { get; set; }

        /// <summary>Indicates why the request failed.</summary>
        public ServiceResponseErrorCode ErrorCode { get; set; }

        /// <summary>Contains a list of errors indicating why the request failed.</summary>
        public IList<ServiceResponseError> Errors { get; set; } = new List<ServiceResponseError>();

        /// <summary>Contains a list of actions that can be taken as a follow-up to a service response.</summary>
        public IList<ServiceResponseAction> Actions { get; set; } = new List<ServiceResponseAction>();

        /// <summary>Shortcut to determine if Result was set to successful.</summary>
        public bool WasSuccessful => Result == ServiceResponseResult.Successful;

        /// <summary>Shortcut to determine if Result was set to Failed.</summary>
        public bool WasUnsuccessful => Result == ServiceResponseResult.Failed;
    }

    /// <summary>
    /// <summary>A wrapper around service methods to inform callers if the request was successful or not. 
    /// All exceptions within the service call should be logged, and the overall outcome of the method reported back to callers via the Result property.
    /// This implimentation accepts a type param and, if successful, returns a value of that type via the Content property.
    /// </summary>
    public partial class ServiceResponse<ResponseType> : ServiceResponse
    {
        /// <summary>Restrict creation of objects to internal project - consumers should use the ServiceResponseFactory to reliably populate object.</summary>
        internal ServiceResponse() { }

        /// <summary>For successfull requests, this contains the content returned by the request.</summary>
        public ResponseType Content { get; set; }
    }
}