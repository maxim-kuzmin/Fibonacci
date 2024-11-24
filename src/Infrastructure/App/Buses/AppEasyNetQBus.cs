namespace Fibonacci.Infrastructure.App.Buses;

/// <summary>
/// Шина приложения, хранящая сообщения в RabbitMQ с использованием библиотеки EasyNetQ.
/// </summary>
/// <param name="_bus">Шина.</param>
public class AppEasyNetQBus(IBus _bus) : IAppBus
{
  /// <inheritdoc/>
  public Task Publish<TMessage>(string subscriberId, TMessage message, CancellationToken cancellationToken)
  {
    return _bus.PubSub.PublishAsync(message, (config) =>
    {
      config.WithTopic(subscriberId);
    },
    cancellationToken);
  }

  /// <inheritdoc/>
  public async Task Subscribe<TMessage>(
    string subscriberId,
    Func<TMessage, CancellationToken, Task> onMessage,
    CancellationToken cancellationToken)
  {
    var task = _bus.PubSub.SubscribeAsync(
      subscriberId,
      onMessage,
      (config) =>
      {
        config.WithAutoDelete().WithTopic(subscriberId);
      },
      cancellationToken);

    await task.ConfigureAwait(false);
  }
}
