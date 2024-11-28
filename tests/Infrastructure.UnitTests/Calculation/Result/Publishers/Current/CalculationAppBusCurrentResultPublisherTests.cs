namespace Fibonacci.Infrastructure.UnitTests.Calculation.Result.Publishers.Current;

public class CalculationAppBusCurrentResultPublisherTests
{
  private readonly CalculationAppBusCurrentResultPublisher _sut;

  private readonly Mock<IAppBus> _appBusMock = new();

  public CalculationAppBusCurrentResultPublisherTests()
  {
    _sut = new CalculationAppBusCurrentResultPublisher(_appBusMock.Object);
  }

  [Fact]
  public async Task PublishCalculationResult_Always_CallsOnceAppBusPublish()
  {
    var calculationId = Guid.NewGuid();

    CalculationResult calculationResult = new(0, 0);

    var calculationResultDTO = calculationResult.ToCalculationResultDTO(calculationId);

    await _sut.PublishCalculationResult(calculationId, calculationResult, default);

    _appBusMock.Verify(
      x => x.Publish(calculationId.ToString(), calculationResultDTO, default),
      Times.Once());
  }
}
