namespace Fibonacci.DomainUseCases.App.Buses;

/// <summary>
/// Шина приложения для обмена сообщениями в памяти.
/// </summary>
public class AppInMemoryBus : IAppBus
{
  private readonly Dictionary<Type, Dictionary<string, object>> _channelsLookup = [];

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
    TaskCompletionSource tcs = new();

    Task.Run(() => Consume(tcs, subscriberId, onMessage, cancellationToken), cancellationToken);

    return tcs.Task;
  }

  private async Task Consume<TMessage>(
    TaskCompletionSource tcs,
    string subscriberId,
    Func<TMessage, CancellationToken, Task> onMessage,
    CancellationToken cancellationToken)
  {
    var channel = GetChannel<TMessage>(subscriberId);

    tcs.SetResult();

    await foreach (var message in channel.Reader.ReadAllAsync(cancellationToken).ConfigureAwait(false))
    {
      await onMessage(message, cancellationToken).ConfigureAwait(false);
    }
  }

  private Channel<TMessage> GetChannel<TMessage>(string subscriptionId)
  {
    lock (_channelsLookup)
    {
      var key = typeof(TMessage);

      if (!_channelsLookup.TryGetValue(key, out var channelLookup))
      {
        channelLookup = [];

        _channelsLookup[key] = channelLookup;
      }

      Channel<TMessage> result;

      if (channelLookup.TryGetValue(subscriptionId, out var channel))
      {
        result = (Channel<TMessage>)channel;
      }
      else
      {
        result = Channel.CreateUnbounded<TMessage>(new()
        {
          SingleWriter = false,
          SingleReader = false,
          AllowSynchronousContinuations = true
        });

        channelLookup[subscriptionId] = result;
      }

      return result;
    }
  }
}
