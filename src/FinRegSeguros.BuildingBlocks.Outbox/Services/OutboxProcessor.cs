using SagaReferenceArchitecture.BuildingBlocks.Outbox.Interfaces;
using SagaReferenceArchitecture.BuildingBlocks.Outbox.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SagaReferenceArchitecture.BuildingBlocks.Outbox.Services;

/// <summary>
/// Implementa o processamento das mensagens pendentes da Outbox.
///
/// Responsável por coordenar:
/// - Leitura das mensagens;
/// - Publicação;
/// - Atualização do estado;
/// - Registro de falhas.
/// </summary>
public sealed class OutboxProcessor(
    OutboxDbContext dbContext,
    IOutboxPublisher publisher,
    ILogger<OutboxProcessor> logger)
    : IOutboxProcessor
{
    /// <summary>
    /// Processa um lote de mensagens pendentes.
    /// </summary>
    public async Task ProcessAsync(
        CancellationToken cancellationToken = default)
    {
        var messages = await dbContext.OutboxMessages
            .Where(x => x.ProcessedAt == null)
            .OrderBy(x => x.CreatedAt)
            .Take(100)
            .ToListAsync(cancellationToken);

        foreach (var message in messages)
        {
            try
            {
                await publisher.PublishAsync(
                    message,
                    cancellationToken);

                message.MarkAsProcessed();

                await dbContext.SaveChangesAsync(
                    cancellationToken);
            }
            catch (Exception exception)
            {
                logger.LogError(
                    exception,
                    "Erro ao processar mensagem da Outbox. " +
                    "MessageId: {MessageId}, CorrelationId: {CorrelationId}",
                    message.Id,
                    message.CorrelationId);

                message.RegisterFailure(
                    exception.Message);

                await dbContext.SaveChangesAsync(
                    cancellationToken);
            }
        }
    }
}