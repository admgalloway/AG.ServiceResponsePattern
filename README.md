# Service Response Pattern

The Service Response pattern provides a simple and inuitive way of passing the results of a method back to callers. Fundamentally, this involves wrapping the raw return value from a method in a response object, which includes additional details related to the outcome of the method.

## Principles
- Wrap every return value from a method in a response object to notify callers of the outcome of the request.
- Write methods to handle their own errors and exceptions, and return the appropriate error code to the callers.
- Always check the response from a method before continuing, and make decisions based on the response object.

## Features and benefits
- Provides a consistent and reliable interface for checking the outcome of method calls - no more null checks or out params.
- Encourages every method to catch and handle it's own exceptions, isolating error logic within the method.
- Promotes readable and intuitive code which takes little-to-no time to understand or investigate.
- Allows short, clean unit tests to reliably verify that each method produces specific results under various conditions.


## The Service Response object

The `ServiceResponse` object is the core of this pattern. It acts as the wrapper for values returned from methods and exposes the following properties to callers:

    
    public class ServiceResponse 
    { 
        // Indicates if the request was successful or not
        public ServiceResponseResult Result { get; set; }

        // Indicates why the request failed
        public ServiceResponseErrorCode ErrorCode { get; set; }

        // Contains a list of errors to be presented to the caller
        public IList<ServiceResponseError> Errors { get; set; }
        
        // Readonly property to determine if response was successful
        public bool WasSuccessful => Result == ServiceResponseResult.Successful;

        // Readonly property to determine if response failed
        public bool WasUnsuccessful => Result == ServiceResponseResult.Failed;
    }

This implementation is used when a method does not return a value once it completes. For methods that do return a value, the following implementation requires a type param `ResponseType` and exposes an additional property `Content`, which returns the value to the caller:

    public class ServiceResponse<ResponseType> : ServiceResponse
    { 
        // Contains the return value itself. This is only set for successful responses
        public ResponseType Content { get; set; }
    }


## Basic Usage
Getting started is simple. Where you would normally return an object directly from a method, replace it with an instance of the ServiceResponse class and specify the object type in the ServiceResponse type param:

	public User GetById(int id);

becomes

	public ServiceResponse<User> GetById(int id);


Once you receive a ServiceResponse there are two properties you can use to determine the outcome of the response and decide how to proceed:

    var userResponse = _userService.GetById(1);
    if (userResponse.WasUnsuccessful)
    {
        ...
    }

    if (userResponse.WasSuccessful)    
    {
        userResponse.Content.FirstName = "Adam";
    }

An additional extension method for WasSuccessful exists which will unwrap the content from a response into an out param:

    if (userResponse.WasSuccessful(out var user))    
    {
        user.FirstName = "Adam";
    }


## Failed Responses and Error Handling

The ErrorCode on the ServiceResponse is used to notify callers of common reasons for a method to fail. These are defined in the `ServiceResponseErrorCode` enum and can be used by callers to make informed decisions about how to present errors to users or how to proceed with processing requests.

The values of this enum are mapped to HTTP status codes to simplify the process of converting them to HTTP responses. This is for convience only has no meaningful impact on their general usage within this package.

The ValidationError indicates that the input provided by a caller was invalid - typical examples include missing mandatory params or invalid email formats. Responses that return a ValidationError should also populate the Errors property with a list of details to tell the caller what was invalid.



## Creating response objects with the ServiceResponseFactory

The `ServiceResponseFactory` provides a quick and intuitive way of generating ServiceResponse objects, with support for responses that return content and those that do not.

### Successful Responses
Successful responses indicate to the caller that the method completed as expected:

    return ServiceResponseFactory.SuccessfulResponse();

If a method returns content from a successful response, simply supply the object when generating it: 

	var user = new User();
    ...
	return ServiceResponseFactory.SuccessfulResponse(user);
    
### Failed Responses
Failed responses come in several flavours, providing specific and detailed error messages back to the caller. Failed requests do not return any content.

#### Not Found
Return a failed response when the requested resource does not exist:

	return ServiceResponseFactory.FailedNotFoundResponse<User>()
    
#### Authentication errors
Return a failed response when the caller could not be authenticated:
	
    ServiceResponseFactory.FailedAuthenticationResponse<User>()
    
#### Authorization errors
Return a failed response when the caller was not authorized to run or access this method:

    ServiceResponseFactory.FailedAuthorizationResponse<User>()
    
#### Multiple Validation errors
Return a failed response when the caller supplied invalid input, along with a list of the validation errors:

	var validationErrors = new List<ValidationError>();
    validationErrors.Add("id", "mandatory");
    ...
    ServiceResponseFactory.FailedValidationResponse<User>(validationErrors)
    
#### Single Validation error
If there was only invalid input value then you will find this method more succinct. Note that the response still contains a list of errors, this one is simply be the only one in the list:

    ServiceResponseFactory.FailedValidationResponse<User>("id", "mandatory")
    
##### System (or internal errors)
Return a failed response when an internal or system error occurred:

    ServiceResponse.FailedResponse<User>()
    
##### Forwarding on dependency errors
Return a failed response, passing on the error code from a previous service response:

    ServiceResponse previousResponse;
    ...
    ServiceResponseFactory.FailedResponse<User>(previousResponse.ErrorCode)


Note that all of these examples specify the type param for a User object. If working with a method that doesn't specify a response type then the usage remains the same, but without the type param.
    ServiceResponseFactory.FailedValidationResponse<User>("id", "mandatory")
    
##### System (or internal errors)
Return a failed response when an internal or system error occurred:

    ServiceResponse.FailedResponse<User>()
    
##### Forwarding on dependency errors
Return a failed response, passing on the error code from a previous service response:

    ServiceResponse previousResponse;
    ...
    ServiceResponseFactory.FailedResponse<User>(previousResponse.ErrorCode)


Note that all of these examples specify the type param for a User object. If working with a method that doesn't specify a response type then the usage remains the same, but without the type param.