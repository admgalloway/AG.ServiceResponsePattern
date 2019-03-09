namespace AG.ServiceResponsePattern.Enums
{
    /// <summary>Defines a list of possible results from a service response.</summary>
    /// <remarks>Details of failures are communicated via the ServiceResponseErrorCode enum.</remarks>
    public enum ServiceResponseResult
    {
        Successful              = 1,
        Failed                  = 2
    }
}
