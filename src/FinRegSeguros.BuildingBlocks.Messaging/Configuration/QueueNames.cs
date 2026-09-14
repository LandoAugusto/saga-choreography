namespace SagaReferenceArchitecture.BuildingBlocks.Messaging.Configuration;

/// <summary>
/// Provides constant values for queue names used in messaging. 
/// </summary>
public static class QueueNames
{
    /// <summary>
    /// Represents the queue name for saga messages.
    /// </summary>
    public const string Saga = "movement.saga";

    /// <summary>
    /// Represents the queue name for regulatory messages.
    /// </summary>
    public const string Regulatory = "movement.regulatory";

    /// <summary>
    /// Represents the queue name for financial messages.
    /// </summary>  
    public const string Financial = "movement.financial";

    /// <summary>
    /// Represents the queue name for accounting messages.  
    /// </summary>
    public const string Accounting = "movement.accounting";

    /// <summary>
    /// Represents the queue name for integration messages.
    /// </summary>      
    public const string Integration = "movement.integration";
}