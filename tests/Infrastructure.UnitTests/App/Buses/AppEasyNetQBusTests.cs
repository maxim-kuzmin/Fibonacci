﻿namespace Fibonacci.Infrastructure.UnitTests.App.Buses;

public class AppEasyNetQBusTests
{
  private readonly AppEasyNetQBus _sut;

  private readonly Mock<IBus> _busMock = new();

  private readonly Mock<IPubSub> _pubSubMock = new();

  public AppEasyNetQBusTests()
  {
    _sut = new (_busMock.Object);

    _busMock.Setup(x => x.PubSub).Returns(_pubSubMock.Object);
  }

  [Fact]
  public async Task Publish_Always_CallsOncePubSubPublishAsync()
  {
    string subscriberId = Guid.NewGuid().ToString();

    string message = Guid.NewGuid().ToString();

    await _sut.Publish(subscriberId, message, default);

    _busMock.Verify(
      x => x.PubSub.PublishAsync(message, It.IsAny<Action<IPublishConfiguration>>(), default),
      Times.Once());
  }

  [Fact]
  public async Task Subscribe_Always_CallsOncePubSubSubscribeAsync()
  {
    string subscriberId = Guid.NewGuid().ToString();

    Func<string, CancellationToken, Task> onMessage = (message, cancellationToken) =>
    {
      return Task.CompletedTask;
    };

    await _sut.Subscribe(subscriberId, onMessage, default);

    _busMock.Verify(
      x => x.PubSub.SubscribeAsync(
        subscriberId,
        onMessage,
        It.IsAny<Action<ISubscriptionConfiguration>>(),
        default),
      Times.Once());
  }
}
