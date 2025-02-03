namespace Fibonacci.DomainUseCases.App;

/// <summary>
/// Интерфейс шины приложения.
/// </summary>
public interface IAppBus
{
  /// <summary>
  /// Опубликовать.
  /// </summary>
  /// <typeparam name="TMessage">Тип сообщения.</typeparam>
  /// <param name="subscriberId">Идентификатор подписчика.</param>
  /// <param name="message">Сообщение.</param>
  /// <param name="cancellationToken">Токен отмены.</param>
  /// <returns>Задача.</returns>
  Task Publish<TMessage>(string subscriberId, TMessage message, CancellationToken cancellationToken);

  /// <summary>
  /// Подписаться.
  /// </summary>
  /// <typeparam name="TMessage">Тип сообщения.</typeparam>
  /// <param name="subscriberId">Идентификатор подписчика.</param>
  /// <param name="onMessage">Обработчик сообщения.</param>
  /// <param name="cancellationToken">Токен отмены.</param>
  /// <returns>Задача.</returns>
  Task Subscribe<TMessage>(
      string subscriberId,
      Func<TMessage, CancellationToken, Task> onMessage,
      CancellationToken cancellationToken);
}
