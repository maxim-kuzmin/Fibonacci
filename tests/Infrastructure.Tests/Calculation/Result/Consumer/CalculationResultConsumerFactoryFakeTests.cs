namespace Fibonacci.Infrastructure.Tests.Calculation.Result.Consumer;

public class CalculationResultConsumerFactoryFakeTests
{
  private readonly CalculationResultConsumerFactoryFake _sut;

  private readonly Mock<ICalculationService> _calculationServiceMock = new();

  private static readonly Guid _calculationId = Guid.NewGuid();

  public CalculationResultConsumerFactoryFakeTests()
  {
    _sut = new(_calculationServiceMock.Object);
  }

  [Fact]
  public void CreateCalculationSubscriber_Always_ReturnsCalculationSubscriber()
  {
    var actual = _sut.CreateCalculationResultConsumer(_calculationId);

    Assert.IsAssignableFrom<ICalculationResultConsumer>(actual);
  }
}
