namespace Fibonacci.Infrastructure.App;

/// <summary>
/// Шина приложения.
/// </summary>
/// <param name="_bus">Шина.</param>
public class AppBus(IBus _bus) : IAppBus
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
    await _bus.PubSub.SubscribeAsync(
      subscriberId,
      onMessage,
      (config) =>
      {
        config.WithAutoDelete().WithTopic(subscriberId);
      },
      cancellationToken);
  }
}
