namespace FinRegSeguros.BuildingBlocks.Messaging.Abstractions;

/// <summary>
/// 
/// </summary>
public interface IMessagePublisher
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="endpoint"></param>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendAsync<T>(
        Uri endpoint,
        T message,
        CancellationToken cancellationToken = default)
        where T : class;
}