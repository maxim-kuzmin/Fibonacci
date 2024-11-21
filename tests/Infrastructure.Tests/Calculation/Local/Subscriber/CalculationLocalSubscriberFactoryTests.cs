namespace Fibonacci.Infrastructure.Tests.Calculation.Local.Subscriber;

public class CalculationLocalSubscriberFactoryTests
{
  private readonly CalculationLocalSubscriberFactory _sut;

  private readonly Mock<ICalculationService> _calculationServiceMock = new();

  private static readonly Guid _calculationId = Guid.NewGuid();

  public CalculationLocalSubscriberFactoryTests()
  {
    _sut = new(_calculationServiceMock.Object);
  }

  [Fact]
  public void CreateCalculationSubscriber_Always_ReturnsCalculationSubscriber()
  {
    var actual = _sut.CreateCalculationSubscriber(_calculationId);

    Assert.IsAssignableFrom<ICalculationSubscriber>(actual);
  }
}
