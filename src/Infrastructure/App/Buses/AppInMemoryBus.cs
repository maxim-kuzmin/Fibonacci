namespace Fibonacci.Infrastructure.App.Buses;

/// <summary>
/// Шина приложения, хранящая сообщения в памяти.
/// </summary>
public class AppInMemoryBus : IAppBus
{
  private readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, object>> _channelsLookup = new();

  public Task Publish<TMessage>(string subscriberId, TMessage message, CancellationToken cancellationToken)
  {
    var channel = GetChannel<TMessage>(subscriberId);

    var valueTask = channel.Writer.WriteAsync(message, cancellationToken);

    return valueTask.AsTask();
  }

  public Task Subscribe<TMessage>(
    string subscriberId,
    Func<TMessage, CancellationToken, Task> onMessage,
    CancellationToken cancellationToken)
  {
    var channel = GetChannel<TMessage>(subscriberId);

    Task.Run(() => Consume(channel.Reader, onMessage, cancellationToken), cancellationToken);
    
    return Task.CompletedTask;
  }

  private static async Task Consume<TMessage>(
    ChannelReader<TMessage> reader,
    Func<TMessage, CancellationToken, Task> onMessage,
    CancellationToken cancellationToken)
  {
    await foreach (var message in reader.ReadAllAsync(cancellationToken))
    {
      await onMessage(message, cancellationToken);
    }
  }

  private Channel<TMessage> GetChannel<TMessage>(string subscriptionId)
  {
    var channelLookup = _channelsLookup.GetOrAdd(typeof(TMessage), _ => new ConcurrentDictionary<string, object>());

    return (Channel<TMessage>)channelLookup.GetOrAdd(
      subscriptionId,
      _ => Channel.CreateUnbounded<TMessage>(new()
      {
        SingleWriter = false,
        SingleReader = false,
        AllowSynchronousContinuations = true
      }));
  }
}
