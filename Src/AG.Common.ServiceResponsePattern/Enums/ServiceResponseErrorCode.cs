namespace AG.Common.ServiceResponsePattern.Enums
{
    /// <summary>Defines a list of error codes to communicate common errors found in services, and communicated back to callers.</summary>
    /// <remarks>These are mapped to HTTP status codes for convience only, allowing for simpler wrapping at the API level.</remarks>
    public enum ServiceResponseErrorCode
    {
        NoError                 = 0,
        ValidationError         = 400,
        AuthorizationError      = 401,
        AuthenticationError     = 403,
        NotFound                = 404,
        SystemError             = 500
    }
}
