using FinRegSeguros.BuildingBlocks.Messaging.Abstractions;
using MassTransit;

namespace FinRegSeguros.BuildingBlocks.Messaging.Publishers;

internal sealed class MassTransitPublisher :
    IMessagePublisher
{
    private readonly ISendEndpointProvider _provider;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    public MassTransitPublisher(
        ISendEndpointProvider provider)
    {
        _provider = provider;
    }

    public async Task SendAsync<T>(
        Uri endpoint,
        T message,
        CancellationToken cancellationToken = default)
        where T : class
    {
        var sendEndpoint =
            await _provider.GetSendEndpoint(endpoint);

        await sendEndpoint.Send(
            message,
            cancellationToken);
    }
}