using FinRegSeguros.BuildingBlocks.Messaging.Abstractions;
using FinRegSeguros.BuildingBlocks.Messaging.Configuration;
using FinRegSeguros.BuildingBlocks.Messaging.Publishers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FinRegSeguros.BuildingBlocks.Messaging.DependencyInjection;

/// <summary>
/// 
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public static IServiceCollection AddMessaging(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<IBusRegistrationConfigurator>? configureConsumers = null)
    {
        services.AddMassTransit(cfg =>
        {
            // Permite que a aplicação consumidora adicione configurações
            // específicas do MassTransit.
            configureConsumers?.Invoke(cfg);

            // Define o padrão de nomenclatura dos endpoints.
            cfg.SetKebabCaseEndpointNameFormatter();

            // Configura o RabbitMQ como transporte do MassTransit.
            cfg.UsingRabbitMq((context, rabbit) =>
            {

                // Obtém as configurações do RabbitMQ através do
                // padrão Options do .NET.
                var options =
                  context
                      .GetRequiredService<IOptions<RabbitMqOptions>>()
                      .Value;

                // Configura a conexão com o RabbitMQ.
                rabbit.Host(
                   options.Host,
                   options.VirtualHost,
                   host =>
                   {
                       host.Username(options.Username);
                       host.Password(options.Password);
                   });

                // Configura a política de retry do MassTransit.
                rabbit.UseMessageRetry(r =>
                {
                    r.Interval(
                        options.RetryCount,
                        TimeSpan.FromSeconds(
                            options.RetryIntervalSeconds));
                });

                // Configura automaticamente os endpoints registrados
                // no container de Dependency Injection.
                // Os Consumers registrados através do configure
                // terão seus endpoints configurados aqui.
                rabbit.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IMessageBus, MassTransitMessageBus>();
        services.AddScoped<IMessagePublisher, MassTransitPublisher>();

        return services;
    }
}