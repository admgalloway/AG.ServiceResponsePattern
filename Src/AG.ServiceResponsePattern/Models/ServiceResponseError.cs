﻿namespace AG.ServiceResponsePattern.Models
{
    /// <summary>Represents an error related to input from a caller (typically provided by a user).</summary>
    public class ServiceResponseError
    {
        public ServiceResponseError(string fieldName, string errorCode)
        {
            FieldName = fieldName;
            ErrorCode = errorCode;
        }

        /// <summary>Indicates which input field this error relates to.</summary>
        public string FieldName { get; set; }

        /// <summary>Indicates why the request failed. Callers that present details to users should translate these code into user-friendly messages.</summary>
        /// <remarks>This implentation now uses a string value. Enums were good for a few generic error codes, but they were very restrictive, required a lot of
        /// effort to add new types, and did not handle project-specific error codes well. Project-specific may (and should) be used to enforce , then cast to 
        /// strings when supplied to this class.</remarks>
        public string ErrorCode { get; set; }

        /// ErrorMessage has been removed from this class. Specifying messages for users at this 
        /// level is too rigid. The preferred approach is to keep a dictionary of error codes used
        /// within a project so that accompanying error messages can be managed independently.
    }
}