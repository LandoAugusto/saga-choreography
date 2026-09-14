using MassTransit;

namespace SagaReferenceArchitecture.BuildingBlocks.Messaging.Consumers;

/// <summary>
/// Configuração base dos consumers do MassTransit.
/// </summary>
/// <typeparam name="TConsumer">Tipo do consumer.</typeparam>
public abstract class ConsumerDefinitionBase<TConsumer> :
    ConsumerDefinition<TConsumer>
    where TConsumer : class, IConsumer
{
    /// <summary>
    /// Inicializa a definição do consumer.
    /// </summary>
    protected ConsumerDefinitionBase()
    {
        EndpointName = typeof(TConsumer).Name
            .Replace("Consumer", string.Empty)
            .ToLowerInvariant();
    }

    /// <summary>
    /// Configura o pipeline do consumer.
    /// </summary>
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<TConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        // Retry das mensagens que apresentarem falha.
        endpointConfigurator.UseMessageRetry(r =>
        {
            r.Interval(
                3,
                TimeSpan.FromSeconds(5));
        });

        // Publicações feitas durante o consumo
        // somente são liberadas após o processamento
        // ser concluído com sucesso.
        endpointConfigurator.UseInMemoryOutbox(context);
    }
}