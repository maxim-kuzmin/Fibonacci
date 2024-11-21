namespace Fibonacci.Infrastructure.Tests.Calculation.Subscriber;

public class CalculationSubscriberFactoryTests
{
  private readonly CalculationSubscriberFactory _sut;

  private readonly Mock<IBus> _busMock = new();

  private readonly Mock<IHttpClientFactory> _httpClientFactoryMock = new();

  private static readonly Guid _calculationId = Guid.NewGuid();

  public CalculationSubscriberFactoryTests()
  {
    _sut = new(_busMock.Object, _httpClientFactoryMock.Object);
  }

  [Fact]
  public void CreateCalculationSubscriber_Always_ReturnsCalculationSubscriber()
  {
    var actual = _sut.CreateCalculationSubscriber(_calculationId);

    Assert.IsAssignableFrom<ICalculationSubscriber>(actual);
  }
}
