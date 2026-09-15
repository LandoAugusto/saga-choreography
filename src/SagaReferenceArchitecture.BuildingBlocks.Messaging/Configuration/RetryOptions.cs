namespace SagaReferenceArchitecture.BuildingBlocks.Messaging.Configuration;


/// <summary>
/// Represents the retry policy configuration for message processing.
/// </summary>
public sealed record RetryOptions
{
  /// <summary>
  /// Gets or sets the number of retry attempts for message processing.
  /// </summary>
  public int Count { get; set; } = 3;

  /// <summary>
  /// Gets or sets the interval in seconds between retry attempts for message processing. 
  /// </summary>
  public int IntervalSeconds { get; set; } = 5;
}
