using SagaReferenceArchitecture.BuildingBlocks.Contracts.Events;
using SagaReferenceArchitecture.BuildingBlocks.Messaging.Abstractions;

namespace SagaReferenceArchitecture.Ingestion.API.Features.Policy
{
    /// <summary>
    /// Handles the issuance of policies and publishes an event indicating that a policy has been issued.
    /// </summary>
    /// <param name="messagePublisher"></param>
    public sealed class IssuePolicyHandler(IMessageBus messagePublisher)
    {
        /// <summary>
        /// Handles the issuance of a policy based on the provided command and publishes an event indicating that a policy has been issued.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Guid> Handle(
            IssuePolicyCommad command,
            CancellationToken cancellationToken)
        {
            // Simulate policy issuance logic
            var policyId = Guid.NewGuid();

            // Publish an event indicating that a policy has been issued
            await messagePublisher.PublishAsync(
                new IssuedPolicyEvent(
                    policyId,
                    command.ClientId,
                    command.Proposal,
                    command.Value),
                cancellationToken);
            return policyId;
        }
    }
}
