using MassTransit;
using SagaReferenceArchitecture.BuildingBlocks.Contracts.Events;

namespace SagaReferenceArchitecture.Ingestion.API.Features.Payment;

/// <summary>
/// Consumer that listens for IssuedPolicyEvent messages and processes payments accordingly. 
/// It uses the ProcessPaymentHandler to handle the payment processing logic.  
/// </summary>
public class ProcessPaymentConsumer(ProcessPaymentHandler _handler)
    : IConsumer<IssuedPolicyEvent>
{

  /// <summary>
  /// Initializes a new instance of the ProcessPaymentConsumer class with the specified ProcessPaymentHandler.    
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>

  public async Task Consume(
      ConsumeContext<IssuedPolicyEvent> context)
  {
    await _handler.Handle(
        context.Message,
        context.CancellationToken);
  }
}
