using FinRegSeguros.BuildingBlocks.Outbox.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinRegSeguros.BuildingBlocks.Outbox.Persistence;

/// <summary>
/// Representa o contexto de persistência da Outbox.
///
/// Responsável por mapear e persistir as mensagens que precisam
/// ser publicadas posteriormente no broker de mensageria.
///
/// O OutboxDbContext não é responsável por:
/// - Publicar mensagens no RabbitMQ;
/// - Executar o Outbox Worker;
/// - Orquestrar a Saga;
/// - Controlar o fluxo da transação de negócio.
///
/// Sua responsabilidade é exclusivamente a persistência dos
/// registros da Outbox.
/// </summary>
public sealed class OutboxDbContext : DbContext
{
    /// <summary>
    /// Inicializa uma nova instância do contexto da Outbox.
    /// </summary>
    /// <param name="options">
    /// Opções de configuração fornecidas pelo Dependency Injection.
    /// </param>
    public OutboxDbContext(
        DbContextOptions<OutboxDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Representa a coleção de mensagens armazenadas na Outbox.
    ///
    /// Cada registro representa uma mensagem que deverá ser
    /// publicada no broker.
    /// </summary>
    public DbSet<OutboxMessage> OutboxMessages
        => Set<OutboxMessage>();

    /// <summary>
    /// Configura o modelo do Entity Framework Core.
    ///
    /// As configurações das entidades são carregadas automaticamente
    /// a partir do assembly onde o OutboxDbContext está localizado.
    /// </summary>
    /// <param name="modelBuilder">
    /// Construtor utilizado pelo EF Core para configurar o modelo.
    /// </param>
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Localiza automaticamente todas as classes que implementam
        // IEntityTypeConfiguration<T> dentro deste assembly.
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(OutboxDbContext).Assembly);
    }
}