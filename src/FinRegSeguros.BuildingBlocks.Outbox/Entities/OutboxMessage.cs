namespace FinRegSeguros.BuildingBlocks.Outbox.Entities;

/// <summary>
/// Representa uma mensagem persistida na tabela Outbox.
/// A mensagem é armazenada de forma transacional e posteriormente
/// processada pelo Outbox Worker para publicação no broker.
/// </summary>
public class OutboxMessage
{
    /// <summary>
    /// Identificador único da mensagem na Outbox.
    /// Pode ser utilizado como chave primária do registro.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Identificador de correlação da transação.
    /// Permite relacionar a mensagem da Outbox com a transação
    /// e com os demais eventos/comandos do fluxo distribuído.
    /// </summary>
    public Guid CorrelationId { get; set; }

    /// <summary>
    /// Identifica o tipo da mensagem que será publicada.
    /// Normalmente corresponde ao tipo do comando ou evento,
    /// permitindo que o Outbox Worker saiba qual mensagem deve
    /// ser reconstruída/publicada.
    /// </summary>
    public string MessageType { get; set; } = string.Empty;

    /// <summary>
    /// Conteúdo da mensagem serializado, normalmente em JSON.
    /// Contém os dados necessários para reconstruir o comando
    /// ou evento antes de sua publicação no broker.
    /// </summary>
    public string Payload { get; set; } = string.Empty;

    /// <summary>
    /// Data e hora em que a mensagem foi criada e persistida
    /// na Outbox.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Data e hora em que a mensagem foi processada com sucesso.
    /// Enquanto estiver nulo, a mensagem ainda pode estar pendente
    /// de processamento.
    /// </summary>
    public DateTime? ProcessedAt { get; set; }

    /// <summary>
    /// Quantidade de vezes que o processamento da mensagem
    /// foi tentado.
    /// É utilizado para controlar políticas de retry.
    /// </summary>
    public int RetryCount { get; set; }

    /// <summary>
    /// Armazena a última mensagem de erro ocorrida durante
    /// o processamento ou publicação da mensagem.
    /// Permanece nulo quando não ocorreu erro.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Marca a mensagem como processada.
    /// </summary>
    public void MarkAsProcessed()
    {
        ProcessedAt = DateTime.UtcNow;
        ErrorMessage = null;
    }

    /// <summary>
    /// Registra uma falha durante o processamento.
    /// </summary>
    public void RegisterFailure(string error)
    {
        RetryCount++;
        ErrorMessage = error;
    }
}