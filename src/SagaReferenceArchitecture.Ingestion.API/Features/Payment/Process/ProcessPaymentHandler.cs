using SagaReferenceArchitecture.BuildingBlocks.Contracts.Events;
using SagaReferenceArchitecture.BuildingBlocks.Messaging.Abstractions;

namespace SagaReferenceArchitecture.Ingestion.API.Features.Payment.Process;

/// <summary>
/// Handles the processing of payment events and publishes a new event upon successful payment approval.    
/// </summary>
/// <param name="messageBus"></param>
public class ProcessPaymentHandler(IMessageBus messageBus)
{

    /// <summary>
    /// Handles the IssuedPolicyEvent, processes the payment, and publishes a PaymentApproved event if the payment is approved.        
    /// </summary>
    /// <param name="evento"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(
        IssuedPolicyEvent evento,
        CancellationToken cancellationToken)
    {
        var paymentApproved = true;
        if (!paymentApproved)
            return;

        var newEvent = new PaymentApprovedEvent(
            evento.PolicyId,
            evento.ClientId,
            evento.Value);

        await messageBus.PublishAsync(
            newEvent,
            cancellationToken);
    }
}
