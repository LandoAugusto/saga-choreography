namespace SagaReferenceArchitecture.BuildingBlocks.Outbox.Interfaces;

/// <summary>
/// Define o contrato responsável por registrar mensagens
/// na Outbox durante uma transação de negócio.
///
/// A responsabilidade deste componente é apenas persistir
/// a intenção de publicação da mensagem.
/// </summary>
public interface IOutboxWriter
{
    /// <summary>
    /// Adiciona uma mensagem à Outbox.
    ///
    /// A mensagem ainda não é publicada no broker.
    /// Ela será publicada posteriormente pelo Outbox Worker.
    /// </summary>
    /// <param name="message">
    /// Mensagem que deverá ser armazenada na Outbox.
    /// </param>
    /// <param name="correlationId">
    /// ID de correlação da mensagem.
    /// </param>
    void Add(object message, Guid correlationId);
}