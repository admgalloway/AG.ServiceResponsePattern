using AG.ServiceResponsePattern.Enums;
using AG.ServiceResponsePattern.Models;
using System.Collections.Generic;

namespace AG.ServiceResponsePattern.Extensions
{
    /// <summary>Set of extension methods to help when using ServiceResponse.</summary>
    public static class ServiceResponseExtensions
    {
        /// <summary>Helper method to simplify response checks. Outputs response value to an inline out var if successful</summary>
        /// <param name="content">If successful, the content will be passe to the referenced out param</param>
        public static bool WasSuccessful<ResponseType>(this ServiceResponse<ResponseType> response, out ResponseType content)
        {
            content = response.WasSuccessful ? response.Content : default(ResponseType);
            return response.WasSuccessful;
        }

        /// <summary>Helper method to simplify response checks. Outputs response value to an inline out var if successful</summary>
        /// <param name="content">If successful, the content will be passe to the referenced out param</param>
        public static bool WasUnsuccessful<ResponseType>(this ServiceResponse<ResponseType> response, out ResponseType content)
        {
            content = response.WasSuccessful ? response.Content : default(ResponseType);
            return response.WasSuccessful;
        }

        /// <summary>Build a ServiceResponseAction object and add it to the the supplied list</summary>
        public static void Add(this IList<ServiceResponseAction> actions, string id, string title, string href, string method)
        {
            var actionToAdd = new ServiceResponseAction(id, title, href, method);
            actions.Add(actionToAdd);
        }


        /// <summary>Build a ServiceResponseError and add it to the the supplied list</summary>
        public static void Add(this IList<ServiceResponseError> serviceResponseError, string fieldName, object errorCode, string errorMessage)
        {
            var errorToAdd = new ServiceResponseError(fieldName, errorCode.ToString(), errorMessage);
            serviceResponseError.Add(errorToAdd);
        }
    }
}
