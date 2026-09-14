using FinRegSeguros.BuildingBlocks.Outbox.Entities;
using FinRegSeguros.BuildingBlocks.Outbox.Interfaces;
using MassTransit;
using System.Text.Json;

namespace FinRegSeguros.BuildingBlocks.Messaging.Publishers;

/// <summary>
/// Publica mensagens da Outbox utilizando MassTransit.
/// </summary>
public sealed class MassTransitOutboxPublisher(
    IPublishEndpoint publishEndpoint)
    : IOutboxPublisher
{
    /// <summary>
    /// Publica uma mensagem persistida na Outbox.
    /// </summary>
    public async Task PublishAsync(
        OutboxMessage message,
        CancellationToken cancellationToken = default)
    {
        var messageType =
            Type.GetType(message.MessageType)
            ?? throw new InvalidOperationException(
                $"Tipo de mensagem '{message.MessageType}' não encontrado.");

        var payload =
            JsonSerializer.Deserialize(
                message.Payload,
                messageType)
            ?? throw new InvalidOperationException(
                $"Não foi possível desserializar a mensagem '{message.Id}'.");

        await publishEndpoint.Publish(
            payload,
            messageType,
            cancellationToken);
    }
}