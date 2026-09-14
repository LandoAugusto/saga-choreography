namespace SagaReferenceArchitecture.BuildingBlocks.Messaging.Headers;


/// <summary>
/// Provides constant values for header names used in messaging.
/// </summary>
public static class HeaderNames
{
    /// <summary>
    /// Represents the header name for the correlation ID.
    /// </summary>
    public const string CorrelationId = "CorrelationId";

    /// <summary>
    /// Represents the header name for the tenant ID.
    /// </summary>
    public const string TenantId = "TenantId";

    /// <summary>
    /// Represents the header name for the user ID.
    /// </summary>  
    public const string UserId = "UserId";
    /// <summary>
    /// Represents the header name for the trace ID.    
    /// </summary>
    public const string TraceId = "TraceId";
}