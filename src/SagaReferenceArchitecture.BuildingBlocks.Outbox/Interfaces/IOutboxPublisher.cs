using SagaReferenceArchitecture.BuildingBlocks.Outbox.Entities;

namespace SagaReferenceArchitecture.BuildingBlocks.Outbox.Interfaces;

/// <summary>
/// Define o contrato responsável pela publicação de mensagens
/// armazenadas na Outbox no broker de mensageria.
///
/// A implementação dessa interface deve conhecer os detalhes
/// necessários para realizar a publicação, como MassTransit,
/// RabbitMQ e serialização da mensagem.
///
/// O processor não precisa conhecer esses detalhes.
/// </summary>
public interface IOutboxPublisher
{
    /// <summary>
    /// Publica uma mensagem da Outbox no broker de mensageria.
    /// </summary>
    /// <param name="message">
    /// Mensagem persistida na Outbox que deverá ser publicada.
    /// </param>
    /// <param name="cancellationToken">
    /// Token utilizado para cancelar a operação de publicação.
    /// </param>
    /// <returns>
    /// Uma <see cref="Task"/> que representa a operação assíncrona.
    /// </returns>
    Task PublishAsync(
        OutboxMessage message,
        CancellationToken cancellationToken = default);
}