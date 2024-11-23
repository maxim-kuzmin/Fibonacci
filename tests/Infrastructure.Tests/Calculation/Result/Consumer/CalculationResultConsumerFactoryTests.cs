namespace Fibonacci.Infrastructure.Tests.Calculation.Result.Consumer;

public class CalculationResultConsumerFactoryTests
{
  private readonly CalculationResultConsumerFactory _sut;

  private readonly Mock<IAppBus> _appBusMock = new();

  private readonly Mock<ICalculationCurrentResultPublisher> _сalculationPublisherMock = new();

  private static readonly Guid _calculationId = Guid.NewGuid();

  public CalculationResultConsumerFactoryTests()
  {
    _sut = new(_appBusMock.Object, _сalculationPublisherMock.Object);
  }

  [Fact]
  public void CreateCalculationSubscriber_Always_ReturnsCalculationSubscriber()
  {
    var actual = _sut.CreateCalculationResultConsumer(_calculationId);

    Assert.IsAssignableFrom<ICalculationResultConsumer>(actual);
  }
}
