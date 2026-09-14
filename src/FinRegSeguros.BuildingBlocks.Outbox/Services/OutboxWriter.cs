using FinRegSeguros.BuildingBlocks.Outbox.Interfaces;
using FinRegSeguros.BuildingBlocks.Outbox.Persistence;

namespace FinRegSeguros.BuildingBlocks.Outbox.Services;

/// <summary>
/// Implementa a gravação de mensagens na Outbox.
///
/// Este componente não publica mensagens no RabbitMQ.
/// Ele apenas adiciona a mensagem ao DbContext atual,
/// permitindo que ela participe da mesma transação da
/// operação de negócio.
/// </summary>
public sealed class OutboxWriter(
    IOutboxMessageFactory factory,
    OutboxDbContext dbContext)
    : IOutboxWriter
{
    private readonly IOutboxMessageFactory _factory = factory;
    /// <summary>
    /// Adiciona uma mensagem ao contexto da Outbox.
    ///
    /// A persistência definitiva ocorrerá quando o DbContext
    /// realizar o SaveChanges dentro da transação da aplicação.
    /// </summary>
    public void Add(object message, Guid correlationId)
    {
        var outboxMessage =
            _factory.Create(
                message,
                correlationId);

        dbContext.OutboxMessages.Add(outboxMessage);
    }
}