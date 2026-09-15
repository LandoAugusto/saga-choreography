using SagaReferenceArchitecture.BuildingBlocks.Messaging.Consumers;

namespace SagaReferenceArchitecture.Ingestion.API.Features.Payment.Process
{
  /// <summary>
  /// Defines the configuration for the ProcessPaymentConsumer, including its dependencies and settings.  
  /// </summary>
  public sealed class ProcessPaymentConsumerDefinition :
     ConsumerDefinitionBase<ProcessPaymentConsumer>
  {
  }
}
