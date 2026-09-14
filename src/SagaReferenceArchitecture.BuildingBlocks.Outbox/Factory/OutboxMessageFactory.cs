using SagaReferenceArchitecture.BuildingBlocks.Outbox.Entities;
using SagaReferenceArchitecture.BuildingBlocks.Outbox.Interfaces;
using System.Text.Json;

namespace SagaReferenceArchitecture.BuildingBlocks.Outbox.Factory;

/// <summary>
/// Cria registros de Outbox a partir de mensagens de domínio
/// ou mensagens de integração.
/// </summary>
public sealed class OutboxMessageFactory
    : IOutboxMessageFactory
{
    /// <summary>
    /// Opções utilizadas para serialização das mensagens.
    /// </summary>
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <summary>
    /// Cria uma mensagem de Outbox.
    /// </summary>
    public OutboxMessage Create(
        object message,
        Guid correlationId)
    {
        ArgumentNullException.ThrowIfNull(message);

        var messageType = message.GetType();

        var payload = JsonSerializer.Serialize(
            message,
            messageType,
            JsonOptions);

        return new OutboxMessage
        {
            Id = Guid.NewGuid(),
            CorrelationId = correlationId,
            MessageType = messageType.AssemblyQualifiedName
                ?? messageType.FullName
                ?? messageType.Name,
            Payload = payload,
            CreatedAt = DateTime.UtcNow
        };
    }
}