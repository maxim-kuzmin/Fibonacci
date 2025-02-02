namespace Fibonacci.DomainUseCases.UnitTests.Calculation.Result.Consumer;

public class CalculationResultConsumerFactoryTests
{
  private readonly CalculationResultConsumerFactory _sut;

  private readonly Mock<IAppBus> _appBusMock = new();

  private readonly Mock<ICalculationNextResultPublisher> _сalculationNextResultPublisherMock = new();

  private static readonly Guid _calculationId = Guid.NewGuid();

  public CalculationResultConsumerFactoryTests()
  {
    _sut = new(_appBusMock.Object, _сalculationNextResultPublisherMock.Object);
  }

  [Fact]
  public void CreateCalculationSubscriber_Always_ReturnsCalculationSubscriber()
  {
    var actual = _sut.CreateCalculationResultConsumer(_calculationId);

    Assert.IsAssignableFrom<ICalculationResultConsumer>(actual);
  }
}
