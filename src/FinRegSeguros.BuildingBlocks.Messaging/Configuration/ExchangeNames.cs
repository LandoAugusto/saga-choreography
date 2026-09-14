namespace FinRegSeguros.BuildingBlocks.Messaging.Configuration;

/// <summary>
/// Provides constant values for exchange names.
/// </summary>
public static class ExchangeNames
{
    /// <summary>
    /// Represents the exchange name for regulatory messages.
    /// </summary>
    public const string Regulatory = "regulatory.exchange";

    /// <summary>
    /// Represents the exchange name for financial messages.
    /// </summary>
    public const string Financial = "financial.exchange";

    /// <summary>
    /// Represents the exchange name for accounting messages.
    /// </summary>
    public const string Accounting = "accounting.exchange";

    /// <summary>
    /// Represents the exchange name for integration messages.
    /// </summary>
    public const string Integration = "integration.exchange";

    /// <summary>
    /// Represents the exchange name for saga messages.
    /// </summary>
    public const string Saga = "saga.exchange";

    /// <summary>
    /// Represents the exchange name for dead letter messages.
    /// </summary>  
    public const string DeadLetter = "deadletter.exchange";
}