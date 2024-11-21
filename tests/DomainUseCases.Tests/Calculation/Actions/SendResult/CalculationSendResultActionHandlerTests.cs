namespace Fibonacci.DomainUseCases.Tests.Calculation.Actions.SendResult;

public class CalculationSendResultActionHandlerTests
{
  private readonly Mock<ICalculationClient> _calculationClientMock = new();

  private readonly Mock<ICalculationPublisher> _calculationPublisherMock = new();

  private readonly CalculationSendResultActionHandler _calculationSendResultActionHandler;

  private static readonly CalculationResult _previousCalculationResult = new(0, 0);

  private static readonly CalculationResult _nextCalculationResult = new(1, 1);

  private static readonly Guid _calculationId = Guid.NewGuid();

  public CalculationSendResultActionHandlerTests()
  {
    _calculationSendResultActionHandler = new(_calculationClientMock.Object, _calculationPublisherMock.Object);

    _calculationClientMock.Setup(x => x.GetNextCalculationResult(_calculationId, _previousCalculationResult))
      .Returns(_nextCalculationResult);
  }

  [Theory]
  [ClassData(typeof(GetNextCalculationResultTestTheoryDataForCallsOnceGetNextCalculationResult))]
  public async Task Handle_CalculationSendResultActionCommand_CallsOnceGetNextCalculationResult(
    string commandCalculationResultInput,
    string commandCalculationResultOutput)
  {
    CalculationSendResultActionCommand command = new(
      _calculationId,
      new(commandCalculationResultInput, commandCalculationResultOutput));

    CalculationResult previousCalculationResult = command.ToCalculationResult();

    await _calculationSendResultActionHandler.Handle(command, CancellationToken.None);

    _calculationClientMock.Verify(
      x => x.GetNextCalculationResult(_calculationId, previousCalculationResult),
      Times.Once());
  }

  [Theory]
  [ClassData(typeof(GetNextCalculationResultTestTheoryDataForCallsOncePublishNextCalculationResult))]
  public async Task Handle_CalculationSendResultActionCommand_CallsOncePublishNextCalculationResult(
    string commandCalculationResultInput,
    string commandCalculationResultOutput,
    BigInteger nextCalculationResultInput,
    BigInteger nextCalculationResultOutput)
  {
    CalculationSendResultActionCommand command = new(
      _calculationId,
      new(commandCalculationResultInput, commandCalculationResultOutput));

    CalculationResult nextCalculationResult = new(nextCalculationResultInput, nextCalculationResultOutput);

    await _calculationSendResultActionHandler.Handle(command, CancellationToken.None);

    _calculationPublisherMock.Verify(
      x => x.PublishNextCalculationResult(_calculationId, nextCalculationResult),
      Times.Once());
  }

  private class GetNextCalculationResultTestTheoryDataForCallsOnceGetNextCalculationResult : TheoryData<string, string>
  {
    public GetNextCalculationResultTestTheoryDataForCallsOnceGetNextCalculationResult()
    {
      var previousCalculationResult = _previousCalculationResult.ToSerializableCalculationResult();

      Add(previousCalculationResult.Input, previousCalculationResult.Output);
    }
  }
  private class GetNextCalculationResultTestTheoryDataForCallsOncePublishNextCalculationResult :
    TheoryData<string, string, BigInteger, BigInteger>
  {
    public GetNextCalculationResultTestTheoryDataForCallsOncePublishNextCalculationResult()
    {
      var previousCalculationResult = _previousCalculationResult.ToSerializableCalculationResult();

      Add(
        previousCalculationResult.Input,
        previousCalculationResult.Output,
        _nextCalculationResult.Input,
        _nextCalculationResult.Output);
    }
  }
}
