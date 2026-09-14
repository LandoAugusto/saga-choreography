using SagaReferenceArchitecture.BuildingBlocks.Outbox.Entities;

namespace SagaReferenceArchitecture.BuildingBlocks.Outbox.Interfaces;

/// <summary>
/// Define o contrato responsável pela criação de mensagens
/// que serão armazenadas na Outbox.
/// </summary>
public interface IOutboxMessageFactory
{
    /// <summary>
    /// Cria uma mensagem de Outbox a partir de um objeto de mensagem.
    /// </summary>
    /// <param name="message">
    /// Mensagem que será posteriormente publicada.
    /// </param>
    /// <param name="correlationId">
    /// Identificador utilizado para correlacionar a mensagem
    /// com a transação de negócio.
    /// </param>
    /// <returns>
    /// Uma nova instância de <see cref="OutboxMessage"/>.
    /// </returns>
    OutboxMessage Create(
        object message,
        Guid correlationId);
}