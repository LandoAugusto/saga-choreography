namespace SagaReferenceArchitecture.BuildingBlocks.Outbox.Interfaces;

/// <summary>
/// Define o contrato de uma mensagem armazenada na Outbox.
///
/// A Outbox permite persistir uma mensagem juntamente com a
/// alteração de estado da aplicação e publicá-la posteriormente
/// no broker de mensageria.
/// </summary>
public interface IOutboxMessage
{
    /// <summary>
    /// Identificador único da mensagem.
    ///
    /// Pode ser utilizado para garantir idempotência e evitar
    /// que a mesma mensagem seja publicada/processada mais de uma vez.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Identificador utilizado para correlacionar a mensagem
    /// com a transação ou fluxo de negócio ao qual ela pertence.
    /// </summary>
    Guid CorrelationId { get; }

    /// <summary>
    /// Tipo da mensagem que será publicada.
    ///
    /// Exemplo:
    /// SagaReferenceArchitecture.Contracts.Transactions.IniciarTransacao
    /// </summary>
    string MessageType { get; }

    /// <summary>
    /// Conteúdo serializado da mensagem.
    ///
    /// Normalmente armazenado em JSON.
    /// </summary>
    string Payload { get; }

    /// <summary>
    /// Data e hora em que a mensagem foi criada e registrada
    /// na Outbox.
    /// </summary>
    DateTime CreatedAt { get; }

    /// <summary>
    /// Data e hora em que a mensagem foi publicada com sucesso.
    ///
    /// Null indica que a mensagem ainda não foi processada.
    /// </summary>
    DateTime? ProcessedAt { get; }

    /// <summary>
    /// Quantidade de tentativas realizadas para publicar a mensagem.
    /// </summary>
    int RetryCount { get; }

    /// <summary>
    /// Última mensagem de erro ocorrida durante a publicação.
    /// </summary>
    string? Error { get; }
}