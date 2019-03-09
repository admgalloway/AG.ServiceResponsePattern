namespace AG.ServiceResponsePattern.Models
{
    /// <summary>Defines an an action which a caller can carry out following the request they have just made. For example, this may
    /// a retry action for a failed request, or a follow-up action for new resources that has just been created, etc.
    /// </summary>
    public class ServiceResponseAction
    {
        public ServiceResponseAction(string id, string title, string href, string method)
        {
            Id = id;
            Title = title;
            Href = href;
            Method = method;
        }

        // this needs some thought. These are very specifically api-driven properties, which a service might not know.

        /// <summary>A unique identity for the action.</summary>
        public string Id { get; set; }

        /// <summary>A user friendly label for the action.</summary>
        public string Title { get; set; }

        /// <summary>An absoulte url identifiy the address of the resource.</summary>
        public string Href { get; set; }

        /// <summary>The method type supported by the resource.</summary>
        public string Method { get; set; }
    }
}