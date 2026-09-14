using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FinRegSeguros.BuildingBlocks.Messaging.Configuration;

/// <summary>
/// Fornece métodos de extensão para registrar e configurar
/// o MassTransit utilizando o RabbitMQ como message broker.
///
/// Essa classe centraliza a configuração de mensageria compartilhada
/// pelos diferentes projetos da solução.
///
/// Exemplos de consumidores:
/// - Ingestion.Worker
/// - Outbox.Worker
/// - Inbox.Worker
/// - Saga.Orchestrator.Worker
/// </summary>
public static class MassTransitExtensions
{
    /// <summary>
    /// Registra e configura o MassTransit utilizando as configurações
    /// do RabbitMQ e das políticas de mensageria definidas no
    /// appsettings.json.
    ///
    /// Também permite que o projeto consumidor forneça configurações
    /// adicionais do MassTransit através do parâmetro <paramref name="configure"/>.
    /// </summary>
    /// <param name="services">
    /// Container de injeção de dependência da aplicação.
    /// </param>
    /// <param name="configuration">
    /// Configuração da aplicação, normalmente proveniente do
    /// appsettings.json, variáveis de ambiente e demais providers.
    /// </param>
    /// <param name="configure">
    /// Ação opcional utilizada pelo projeto consumidor para registrar
    /// consumidores, sagas e outras configurações específicas.
    /// </param>
    /// <returns>
    /// A própria coleção de serviços para permitir encadeamento.
    /// </returns>
    public static IServiceCollection AddMessaging(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<IBusRegistrationConfigurator>? configure = null)
    {
        // ------------------------------------------------------------
        // Registra as configurações do RabbitMQ.
        //
        // O MassTransit poderá posteriormente obter essas configurações
        // através de IOptions<RabbitMqOptions>.
        // ------------------------------------------------------------
        services.Configure<RabbitMqOptions>(
            configuration.GetSection(RabbitMqOptions.SectionName));

        // ------------------------------------------------------------
        // Registra as configurações gerais de mensageria.
        //
        // Exemplo:
        // Messaging:
        // {
        //     ...
        // }
        // ------------------------------------------------------------
        services.Configure<MessagingOptions>(
            configuration.GetSection(MessagingOptions.SectionName));

        // ------------------------------------------------------------
        // Registra o MassTransit no Dependency Injection.
        // ------------------------------------------------------------
        services.AddMassTransit(cfg =>
        {
            // --------------------------------------------------------
            // Permite que a aplicação consumidora adicione configurações
            // específicas do MassTransit.
            //
            // Por exemplo:
            // - Consumers
            // - Sagas
            // - Activities
            // - Definitions
            // --------------------------------------------------------
            configure?.Invoke(cfg);

            // --------------------------------------------------------
            // Define o padrão de nomenclatura dos endpoints.
            //
            // Exemplo:
            //
            // CreateMovementConsumer
            //
            // será convertido para algo semelhante a:
            //
            // create-movement
            // --------------------------------------------------------
            cfg.SetKebabCaseEndpointNameFormatter();

            // --------------------------------------------------------
            // Configura o RabbitMQ como transporte do MassTransit.
            // --------------------------------------------------------
            cfg.UsingRabbitMq((context, rabbit) =>
            {
                // ----------------------------------------------------
                // Obtém as configurações do RabbitMQ através do
                // padrão Options do .NET.
                // ----------------------------------------------------
                var options =
                    context
                        .GetRequiredService<IOptions<RabbitMqOptions>>()
                        .Value;

                // ----------------------------------------------------
                // Configura a conexão com o RabbitMQ.
                //
                // Host:
                //     endereço do RabbitMQ
                //
                // VirtualHost:
                //     namespace lógico dentro do RabbitMQ
                //
                // Username / Password:
                //     credenciais de autenticação
                // ----------------------------------------------------
                rabbit.Host(
                    options.Host,
                    options.VirtualHost,
                    host =>
                    {
                        host.Username(options.Username);
                        host.Password(options.Password);
                    });

                // ----------------------------------------------------
                // Configura a política de retry do MassTransit.
                //
                // Exemplo:
                //
                // RetryCount = 3
                // RetryIntervalSeconds = 5
                //
                // A mensagem poderá ser tentada novamente de acordo
                // com essa política antes de ser considerada como
                // falha.
                // ----------------------------------------------------
                rabbit.UseMessageRetry(r =>
                {
                    r.Interval(
                        options.RetryCount,
                        TimeSpan.FromSeconds(
                            options.RetryIntervalSeconds));
                });

                // ----------------------------------------------------
                // Configura automaticamente os endpoints registrados
                // no container de Dependency Injection.
                //
                // Os Consumers registrados através do configure
                // terão seus endpoints configurados aqui.
                // ----------------------------------------------------
                rabbit.ConfigureEndpoints(context);
            });
        });

        // ------------------------------------------------------------
        // Retorna o IServiceCollection para permitir:
        //
        // services
        //     .AddMessaging(...)
        //     .AddControllers();
        //
        // ------------------------------------------------------------
        return services;
    }
}