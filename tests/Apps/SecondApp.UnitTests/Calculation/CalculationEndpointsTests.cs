namespace Fibonacci.Apps.SecondApp.UnitTests.Calculation;

public class CalculationEndpointsTests
{
  private readonly Mock<IMediator> _mediatorMock = new();

  private readonly CalculationSendResultActionCommand _sendResultActionCommand;

  public CalculationEndpointsTests()
  {
    var calculationId = Guid.NewGuid();

    CalculationResult calculationResult = new(0, 0);

    _sendResultActionCommand = new(
      calculationId,
      calculationResult.ToSerializableCalculationResult());

    _mediatorMock.Setup(x => x.Send(_sendResultActionCommand, default)).Returns(Task.CompletedTask);
  }

  [Fact]
  public async Task SendResult_Always_ReturnsOk()
  {
    var actual = await CalculationEndpoints.SendResult(_mediatorMock.Object, _sendResultActionCommand);

    Assert.IsType<Ok>(actual);
  }
}
