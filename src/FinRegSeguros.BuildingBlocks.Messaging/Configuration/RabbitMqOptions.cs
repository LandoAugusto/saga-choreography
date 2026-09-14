namespace FinRegSeguros.BuildingBlocks.Messaging.Configuration;

/// <summary>
/// Representa as configurações necessárias para conexão e
/// comportamento dos componentes que utilizam o RabbitMQ.
///
/// Essa classe é normalmente preenchida a partir de uma seção
/// de configuração do appsettings.json.
/// </summary>
public sealed class RabbitMqOptions
{
    /// <summary>
    /// Nome da seção utilizada no appsettings.json
    /// para armazenar as configurações do RabbitMQ.
    /// </summary>
    public const string SectionName = "RabbitMQ";

    /// <summary>
    /// Endereço ou host onde o RabbitMQ está executando.
    ///
    /// Exemplos:
    /// - localhost
    /// - rabbitmq
    /// - rabbitmq.finregseguros.local
    /// </summary>
    public string Host { get; init; } = string.Empty;

    /// <summary>
    /// Virtual Host utilizado dentro do RabbitMQ.
    ///
    /// O valor "/" representa o Virtual Host padrão do RabbitMQ.
    /// </summary>
    public string VirtualHost { get; init; } = "/";

    /// <summary>
    /// Usuário utilizado para autenticação no RabbitMQ.
    /// </summary>
    public string Username { get; init; } = string.Empty;

    /// <summary>
    /// Senha utilizada para autenticação no RabbitMQ.
    /// </summary>
    public string Password { get; init; } = string.Empty;

    /// <summary>
    /// Quantidade máxima de mensagens que o consumidor pode
    /// receber antecipadamente do RabbitMQ sem confirmar o processamento.
    ///
    /// O valor 32 significa que o consumidor pode ter até
    /// 32 mensagens em processamento/pendentes de confirmação.
    /// </summary>
    public ushort PrefetchCount { get; init; } = 32;

    /// <summary>
    /// Define se a fila ou recurso configurado deve ser persistente.
    ///
    /// true:
    /// O RabbitMQ mantém a definição mesmo após reinicialização.
    /// </summary>
    public bool Durable { get; init; } = true;

    /// <summary>
    /// Define se a fila deve ser removida automaticamente
    /// quando não houver mais consumidores ou quando as
    /// condições de auto-delete forem atendidas.
    ///
    /// false é o comportamento recomendado para filas
    /// permanentes da aplicação.
    /// </summary>
    public bool AutoDelete { get; init; }

    /// <summary>
    /// Quantidade máxima de tentativas de processamento
    /// de uma mensagem antes de considerá-la como falha.
    ///
    /// O valor padrão é 3 tentativas.
    /// </summary>
    public int RetryCount { get; init; } = 3;

    /// <summary>
    /// Intervalo, em segundos, entre as tentativas de processamento
    /// de uma mensagem.
    ///
    /// O valor padrão é de 5 segundos.
    /// </summary>
    public int RetryIntervalSeconds { get; init; } = 5;
}