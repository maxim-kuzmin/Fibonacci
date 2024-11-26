namespace Fibonacci.Apps.FirstApp.Tests.Calculation;

public class CalculationServiceTests
{
  private readonly CalculationService _sut;

  private readonly CalculationOptions _calculationOptions = new(1, 1);

  private readonly Mock<ILogger<CalculationWorker>> _loggerMock = new();

  private readonly Mock<ICalculationMonitor> _calculationMonitorMock = new();  

  private readonly Mock<ICalculationClient> _calculationClientMock = new();

  private readonly Mock<ICalculationResultConsumerFactory> _calculationConsumerFactoryMock = new();

  private readonly Mock<ICalculationResultConsumer> _calculationConsumerMock = new();

  public CalculationServiceTests()
  {
    _sut = new(
      _loggerMock.Object,
      _calculationOptions,
      _calculationMonitorMock.Object,      
      _calculationClientMock.Object,
      _calculationConsumerFactoryMock.Object);

    _calculationConsumerFactoryMock.Setup(x => x.CreateCalculationResultConsumer(It.IsAny<Guid>()))
      .Returns(_calculationConsumerMock.Object);

    _calculationClientMock.Setup(x => x.GetNextCalculationResult(It.IsAny<Guid>(), It.IsAny<CalculationResult>()))
      .Returns(new CalculationResult(1, 1));

    _calculationConsumerMock.Setup(x => x.GetNextCalculationResult(
        It.IsAny<CalculationResult>(),
        It.IsAny<CancellationToken>()))
      .Returns(Task.FromResult(new CalculationResult(2, 1)));
  }

  [Fact]
  public async Task StartAsync_Always_CallsCalculationCountCreateCalculationResultConsumer()
  {
    await _sut.Calculate(CancellationToken.None);

    _calculationConsumerFactoryMock.Verify(
      x => x.CreateCalculationResultConsumer(It.IsAny<Guid>()),
      Times.Exactly(_calculationOptions.CalculationCount));
  }
}
