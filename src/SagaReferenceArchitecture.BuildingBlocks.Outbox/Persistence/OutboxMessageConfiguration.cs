using SagaReferenceArchitecture.BuildingBlocks.Outbox.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SagaReferenceArchitecture.BuildingBlocks.Outbox.Persistence;

/// <summary>
/// Configuração de persistência da entidade OutboxMessage.
/// </summary>
public sealed class OutboxMessageConfiguration
    : IEntityTypeConfiguration<OutboxMessage>
{
    /// <summary>
    /// Configura o mapeamento da OutboxMessage.
    /// </summary>
    public void Configure(
        EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.CorrelationId)
            .IsRequired();

        builder.Property(x => x.MessageType)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Payload)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.ProcessedAt);

        builder.Property(x => x.RetryCount)
            .IsRequired();

        builder.Property(x => x.ErrorMessage)
            .HasMaxLength(4000);

        builder.HasIndex(x => new
        {
            x.ProcessedAt,
            x.CreatedAt
        });
    }
}