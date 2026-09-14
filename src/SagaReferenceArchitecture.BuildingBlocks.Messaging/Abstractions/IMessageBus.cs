namespace SagaReferenceArchitecture.BuildingBlocks.Messaging.Abstractions;

/// <summary>
/// 
/// </summary>
public interface IMessageBus
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PublishAsync<T>(
        T message,
        CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="correlationId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PublishAsync<T>(
        T message,
        Guid correlationId,
        CancellationToken cancellationToken = default)
        where T : class;
}