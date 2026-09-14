using FinRegSeguros.BuildingBlocks.Outbox.Factory;
using FinRegSeguros.BuildingBlocks.Outbox.Interfaces;
using FinRegSeguros.BuildingBlocks.Outbox.Persistence;
using FinRegSeguros.BuildingBlocks.Outbox.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinRegSeguros.BuildingBlocks.Outbox.Extensions;

/// <summary>
/// Fornece métodos de extensão para registrar e configurar
/// a infraestrutura de persistência da Outbox.
///
/// Responsabilidades:
/// - Registrar o OutboxDbContext no Dependency Injection;
/// - Configurar o provider do banco de dados;
/// - Obter a connection string da configuração da aplicação.
/// </summary>
public static class OutboxExtensions
{
    /// <summary>
    /// Nome da connection string utilizada pela Outbox.
    /// </summary>
    private const string ConnectionStringName = "OutboxDatabase";

    /// <summary>
    /// Registra o <see cref="OutboxDbContext"/> no container
    /// de Dependency Injection.
    /// </summary>
    /// <param name="services">
    /// Container de serviços da aplicação.
    /// </param>
    /// <param name="configuration">
    /// Configuração da aplicação.
    /// </param>
    /// <returns>
    /// O próprio <see cref="IServiceCollection"/>, permitindo
    /// encadeamento de configurações.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Lançada quando a connection string da Outbox não estiver configurada.
    /// </exception>
    public static IServiceCollection AddOutboxPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddScoped<IOutboxWriter, OutboxWriter>();
        services.AddScoped<IOutboxProcessor, OutboxProcessor>();
        services.AddSingleton<IOutboxMessageFactory, OutboxMessageFactory>();

        var connectionString =
            configuration.GetConnectionString(ConnectionStringName);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"Connection string '{ConnectionStringName}' não foi configurada.");
        }

        services.AddDbContext<OutboxDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }
}