using FinRegSeguros.BuildingBlocks.Messaging.Abstractions;
using MassTransit;

namespace FinRegSeguros.BuildingBlocks.Messaging.Publishers;

internal sealed class MassTransitMessageBus : IMessageBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="publishEndpoint"></param>
    public MassTransitMessageBus(
        IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task PublishAsync<T>(
        T message,
        CancellationToken cancellationToken = default)
        where T : class
    {
        return _publishEndpoint.Publish(
            message,
            cancellationToken);
    }

    public Task PublishAsync<T>(
        T message,
        Guid correlationId,
        CancellationToken cancellationToken = default)
        where T : class
    {
        return _publishEndpoint.Publish(message,
            context =>
            {
                context.CorrelationId = correlationId;
            },
            cancellationToken);
    }
}