namespace SagaReferenceArchitecture.BuildingBlocks.Outbox.Interfaces;

/// <summary>
/// Define o contrato responsável pelo processamento das mensagens
/// pendentes armazenadas na Outbox.
///
/// O processor coordena o fluxo entre a persistência da Outbox
/// e o mecanismo de publicação de mensagens.
///
/// Ele não deve ser responsável por:
/// - Controlar o ciclo de vida do Worker;
/// - Implementar a conexão com o RabbitMQ;
/// - Implementar diretamente o acesso ao banco de dados.
///
/// Essas responsabilidades pertencem às abstrações específicas
/// de cada componente.
/// </summary>
public interface IOutboxProcessor
{
    /// <summary>
    /// Processa as mensagens pendentes da Outbox.
    ///
    /// O método deve buscar um lote de mensagens pendentes,
    /// tentar publicá-las e atualizar o estado de cada mensagem
    /// de acordo com o resultado da operação.
    /// </summary>
    /// <param name="cancellationToken">
    /// Token utilizado para solicitar o cancelamento do processamento.
    /// </param>
    /// <returns>
    /// Uma Task que representa a operação assíncrona.
    /// </returns>
    Task ProcessAsync(
        CancellationToken cancellationToken = default);
}