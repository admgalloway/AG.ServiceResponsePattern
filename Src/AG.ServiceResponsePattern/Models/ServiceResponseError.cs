namespace AG.ServiceResponsePattern.Models
{
    /// <summary>Represents an error related to input from a caller (typically provided by a user).</summary>
    public class ServiceResponseError
    {
        public ServiceResponseError(string fieldName, object errorCode, string errorMessage)
        {
            FieldName = fieldName;
            ErrorCode = errorCode.ToString();
            ErrorMessage = errorMessage;
        }

        /// <summary>Indicates which input field this error relates to.</summary>
        public string FieldName { get; set; }

        /// <summary>Indicates why the request failed. Callers that present details to users should translate these code into user-friendly messages.</summary>
        /// <remarks>This implentation now uses a string value. Enums were good for a few generic error codes, but they were very restrictive, required a lot of
        /// effort to add new types, and did not handle project-specific error codes well. Project-specific may (and should) be used to enforce , then cast to 
        /// strings when supplied to this class.</remarks>
        public string ErrorCode { get; set; }

        /// <summary>User friendly message to be displayed to a user.</summary>
        /// <remarks>This is designed as a fallback. Clients should map the error code to tailored user messages.</remarks>
        public string ErrorMessage { get; set; }
    }
}
