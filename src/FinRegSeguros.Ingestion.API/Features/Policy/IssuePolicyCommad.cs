namespace FinRegSeguros.Ingestion.API.Features.Policy;

/// <summary>
/// Command to issue a policy for a client based on a proposal and value.
/// </summary>
/// <param name="ClientId"></param>
/// <param name="Proposal"></param>
/// <param name="Value"></param>
public sealed record IssuePolicyCommad(
Guid ClientId,
Guid Proposal,
decimal Value);
