using MassTransit;
using SagaReferenceArchitecture.BuildingBlocks.Messaging.Configuration;

namespace SagaReferenceArchitecture.BuildingBlocks.Messaging.Retry;

/// <summary>
/// Provides methods to configure retry policies for message processing in MassTransit.
/// </summary>
public static class RetryPolicy
{
  /// <summary>
  /// Configures the retry policy for message processing in MassTransit using the specified options.
  /// </summary>
  /// <param name="configurator"></param>
  /// <param name="options"></param>
  public static void Configure(
      IConsumePipeConfigurator configurator,
      RetryOptions options)
  {
    configurator.UseMessageRetry(retry =>
    {
      retry.Interval(
          options.Count,
          TimeSpan.FromSeconds(
              options.IntervalSeconds));
    });
  }
}
