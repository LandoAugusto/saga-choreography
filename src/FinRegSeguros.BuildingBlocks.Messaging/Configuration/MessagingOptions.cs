namespace FinRegSeguros.BuildingBlocks.Messaging.Configuration;

/// <summary>
/// Represents the options for configuring messaging.   
/// </summary>
public sealed class MessagingOptions
{
    /// <summary>
    /// Represents the section name for messaging configuration in the application settings.
    /// </summary>
    public const string SectionName = "Messaging";

    /// <summary>
    /// Indicates whether to use the outbox pattern for message processing.
    /// </summary>
    public bool UseOutbox { get; init; } = true;

    /// <summary>
    /// Indicates whether to use an inbox for message processing.
    /// </summary>
    public bool UseInbox { get; init; } = true;

    /// <summary>
    /// Indicates whether to use correlation IDs for message tracking and correlation.
    /// </summary>
    public bool UseCorrelationId { get; init; } = true;

    /// <summary>
    /// Indicates whether to use message retry logic.
    /// </summary>
    public bool UseRetry { get; init; } = true;

    /// <summary>
    /// Indicates whether to use a dead letter queue for message handling.
    /// </summary>
    public bool UseDeadLetterQueue { get; init; } = true;
}