namespace SagaReferenceArchitecture.BuildingBlocks.Outbox.Configuration;

/// <summary>
/// Define as configurações utilizadas pelo mecanismo de Outbox.
///
/// Essas configurações determinam como o Outbox Worker consulta,
/// processa e tenta novamente publicar as mensagens pendentes.
/// </summary>
public sealed class OutboxOptions
{
    /// <summary>
    /// Nome da seção correspondente no appsettings.json.
    /// </summary>
    public const string SectionName = "Outbox";

    /// <summary>
    /// Intervalo, em segundos, entre cada ciclo de processamento
    /// da Outbox.
    ///
    /// Exemplo:
    /// 5 = o Worker verifica novas mensagens a cada 5 segundos.
    /// </summary>
    public int PollingIntervalSeconds { get; init; } = 5;

    /// <summary>
    /// Quantidade máxima de mensagens processadas em cada ciclo.
    ///
    /// Esse valor evita que o Worker tente processar uma quantidade
    /// muito grande de mensagens de uma única vez.
    /// </summary>
    public int BatchSize { get; init; } = 100;

    /// <summary>
    /// Quantidade máxima de tentativas para publicação de uma
    /// mensagem antes de ela ser considerada como falha.
    /// </summary>
    public int MaxRetryCount { get; init; } = 3;

    /// <summary>
    /// Intervalo, em segundos, entre as tentativas de publicação
    /// de uma mensagem que apresentou falha.
    /// </summary>
    public int RetryIntervalSeconds { get; init; } = 5;

    /// <summary>
    /// Define se o Worker deve processar mensagens da Outbox.
    ///
    /// Pode ser utilizado para habilitar ou desabilitar o processamento
    /// em determinados ambientes.
    /// </summary>
    public bool Enabled { get; init; } = true;
}