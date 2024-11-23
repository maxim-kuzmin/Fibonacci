namespace Fibonacci.DomainUseCases.Tests.Calculation.Actions.SendResult;

public class CalculationSendResultActionHandlerTests
{
  private readonly CalculationSendResultActionHandler _sut;

  private readonly Mock<ICalculationClient> _calculationClientMock = new();

  private readonly Mock<ICalculationCurrentResultPublisher> _calculationCurrentResultPublisherMock = new();

  private static readonly CalculationResult _previousCalculationResult = new(0, 0);
  
  private static readonly CalculationResult _nextCalculationResult = new(1, 1);

  private static readonly Guid _calculationId = Guid.NewGuid();

  public CalculationSendResultActionHandlerTests()
  {
    _sut = new(_calculationClientMock.Object, _calculationCurrentResultPublisherMock.Object);

    _calculationClientMock.Setup(x => x.GetNextCalculationResult(_calculationId, _previousCalculationResult))
      .Returns(_nextCalculationResult);
  }

  [Theory]
  [ClassData(typeof(GetNextCalculationResultTestTheoryDataForCallsOnceClientGetNextCalculationResult))]
  public async Task Handle_CalculationSendResultActionCommand_CallsOnceClientGetNextCalculationResult(
    string commandCalculationResultInput,
    string commandCalculationResultOutput)
  {
    CalculationSendResultActionCommand command = new(
      _calculationId,
      new(commandCalculationResultInput, commandCalculationResultOutput));

    CalculationResult previousCalculationResult = command.ToCalculationResult();

    await _sut.Handle(command, CancellationToken.None);

    _calculationClientMock.Verify(
      x => x.GetNextCalculationResult(_calculationId, previousCalculationResult),
      Times.Once());
  }

  [Theory]
  [ClassData(typeof(GetNextCalculationResultTestTheoryDataForCallsOncePublisherPublishNextCalculationResult))]
  public async Task Handle_CalculationSendResultActionCommand_CallsOncePublisherPublishNextCalculationResult(
    string commandCalculationResultInput,
    string commandCalculationResultOutput,
    BigInteger nextCalculationResultInput,
    BigInteger nextCalculationResultOutput)
  {
    CalculationSendResultActionCommand command = new(
      _calculationId,
      new(commandCalculationResultInput, commandCalculationResultOutput));

    CalculationResult nextCalculationResult = new(nextCalculationResultInput, nextCalculationResultOutput);

    await _sut.Handle(command, CancellationToken.None);

    _calculationCurrentResultPublisherMock.Verify(
      x => x.PublishCalculationResult(_calculationId, nextCalculationResult, CancellationToken.None),
      Times.Once());
  }

  private class GetNextCalculationResultTestTheoryDataForCallsOnceClientGetNextCalculationResult : TheoryData<string, string>
  {
    public GetNextCalculationResultTestTheoryDataForCallsOnceClientGetNextCalculationResult()
    {
      var previousCalculationResult = _previousCalculationResult.ToSerializableCalculationResult();

      Add(previousCalculationResult.Input, previousCalculationResult.Output);
    }
  }
  private class GetNextCalculationResultTestTheoryDataForCallsOncePublisherPublishNextCalculationResult :
    TheoryData<string, string, BigInteger, BigInteger>
  {
    public GetNextCalculationResultTestTheoryDataForCallsOncePublisherPublishNextCalculationResult()
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
