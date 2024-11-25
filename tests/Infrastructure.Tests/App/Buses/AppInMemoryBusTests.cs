﻿namespace Fibonacci.Infrastructure.Tests.App.Buses;

public class AppInMemoryBusTests
{
  private readonly AppInMemoryBus _sut;

  public AppInMemoryBusTests()
  {
    _sut = new AppInMemoryBus();
  }

  [Fact]
  public async Task Publish_Message_ReturnsMessageOnCallbackFromSameSubscriber()
  {
    string subscriberId = Guid.NewGuid().ToString();

    string expected = Guid.NewGuid().ToString();

    string actual = string.Empty;

    TaskCompletionSource tcsOnCallback = new();

    Func<string, CancellationToken, Task> onMessage = (message, cancellationToken) =>
    {
      actual = message;

      tcsOnCallback.SetResult();

      return Task.CompletedTask;
    };

    await _sut.Subscribe(subscriberId, onMessage, CancellationToken.None);

    var timeoutTask = Task.Run(async () =>
    {
      await Task.Delay(100);
    });

    await _sut.Publish(subscriberId, expected, CancellationToken.None);

    await Task.WhenAny(tcsOnCallback.Task, timeoutTask);

    Assert.Equal(expected, actual);
  }

  [Fact]
  public async Task Publish_Message_ReturnsNoMessageOnCallbackFromOtherSubscriber()
  {
    string subscriberId = Guid.NewGuid().ToString();

    string otherSubscriberId = Guid.NewGuid().ToString();

    string expected = Guid.NewGuid().ToString();

    TaskCompletionSource tcsOnCallback = new();

    Func<string, CancellationToken, Task> onMessage = (message, cancellationToken) =>
    {
      tcsOnCallback.SetResult();

      return Task.CompletedTask;
    };

    await _sut.Subscribe(subscriberId, onMessage, CancellationToken.None);

    var timeoutTask = Task.Run(async () =>
    {
      await Task.Delay(100);
    });

    await _sut.Publish(otherSubscriberId, expected, CancellationToken.None);

    await Task.WhenAny(tcsOnCallback.Task, timeoutTask);

    Assert.True(timeoutTask.IsCompleted && !tcsOnCallback.Task.IsCompleted);
  }
}
