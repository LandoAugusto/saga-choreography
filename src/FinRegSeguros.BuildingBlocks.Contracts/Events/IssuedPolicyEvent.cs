namespace FinRegSeguros.BuildingBlocks.Contracts.Events
{
    /// <summary>
    /// Event representing the issuance of a policy for a client based on a proposal and value. 
    /// </summary>
    /// <param name="PolicyId"></param>
    /// <param name="Proposal"></param>
    /// <param name="ClientId"></param>
    /// <param name="Value"></param>
    public record IssuedPolicyEvent(
    Guid PolicyId,
    Guid Proposal,
    Guid ClientId,
    decimal Value);

}
