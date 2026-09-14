namespace FinRegSeguros.BuildingBlocks.Contracts.Events;

/// <summary>
/// Event representing the approval of a payment for a specific policy and client, including the payment value. 
/// </summary>
/// <param name="PolicyId"></param>
/// <param name="ClientId"></param>
/// <param name="Value"></param>
public record PaymentApprovedEvent(
Guid PolicyId,
Guid ClientId,
decimal Value);
