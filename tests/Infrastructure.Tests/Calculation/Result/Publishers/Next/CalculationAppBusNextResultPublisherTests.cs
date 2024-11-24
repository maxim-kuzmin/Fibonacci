namespace Fibonacci.Infrastructure.Tests.Calculation.Result.Publishers.Next;

public class CalculationAppBusNextResultPublisherTests
{
  private readonly CalculationAppBusNextResultPublisher _sut;

  private readonly Mock<ICalculationService> _calculationServiceMock = new();

  private readonly Mock<ICalculationCurrentResultPublisher> _calculationCurrentResultPublisherMock = new();

  private static readonly CalculationResult[] _previousCalculationResults =
    [new(0, 0), new(1, 1), new(2, 1), new(3, 2), new(4, 3), new(5, 5)];

  private static readonly CalculationResult[] _nextCalculationResults =
    [new(1, 1), new(2, 1), new(3, 2), new(4, 3), new(5, 5), new(6, 8)];

  private static readonly Guid _calculationId = Guid.NewGuid();

  public CalculationAppBusNextResultPublisherTests()
  {
    _sut = new CalculationAppBusNextResultPublisher(
      _calculationServiceMock.Object,
      _calculationCurrentResultPublisherMock.Object);

    for (var i = 0; i < _previousCalculationResults.Length; i++)
    {
      var previousCalculationResultDTO = _previousCalculationResults[i].ToCalculationResultDTO(_calculationId);

      var nextCalculationResultDTO = _nextCalculationResults[i].ToCalculationResultDTO(_calculationId);

      _calculationServiceMock.Setup(x => x.GetNextCalculationResult(previousCalculationResultDTO))
        .Returns(nextCalculationResultDTO);
    }
  }

  [Theory]
  [ClassData(typeof(GetNextCalculationResultTestTheoryDataForCallsOnceServiceGetNextCalculationResult))]
  public async Task PublishCalculationResult_CalculationResult_CallsOnceServiceGetNextCalculationResult(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    var previousCalculationResultDTO = previousCalculationResult.ToCalculationResultDTO(_calculationId);

    await _sut.PublishCalculationResult(_calculationId, previousCalculationResult, CancellationToken.None);

    _calculationServiceMock.Verify(x => x.GetNextCalculationResult(previousCalculationResultDTO), Times.Once());
  }

  [Theory]
  [ClassData(typeof(GetNextCalculationResultTestTheoryDataForCallsOnceCurrentResultPublisherPublishCalculationResult))]
  public async Task PublishCalculationResult_CalculationResult_CallsOnceCurrentResultPublisherPublishCalculationResult(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput,
    BigInteger nextCalculationResultInput,
    BigInteger nextCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    await _sut.PublishCalculationResult(_calculationId, previousCalculationResult, CancellationToken.None);

    CalculationResult nextCalculationResult = new(nextCalculationResultInput, nextCalculationResultOutput);

    _calculationCurrentResultPublisherMock.Verify(
      x => x.PublishCalculationResult(_calculationId, nextCalculationResult, CancellationToken.None),
      Times.Once());
  }

  private class GetNextCalculationResultTestTheoryDataForCallsOnceServiceGetNextCalculationResult :
    TheoryData<BigInteger, BigInteger>
  {
    public GetNextCalculationResultTestTheoryDataForCallsOnceServiceGetNextCalculationResult()
    {
      for (var i = 0; i < _previousCalculationResults.Length; i++)
      {
        var _previousCalculationResult = _previousCalculationResults[i];

        Add(_previousCalculationResult.Input, _previousCalculationResult.Output);
      }
    }
  }

  private class GetNextCalculationResultTestTheoryDataForCallsOnceCurrentResultPublisherPublishCalculationResult :
    TheoryData<BigInteger, BigInteger, BigInteger, BigInteger>
  {
    public GetNextCalculationResultTestTheoryDataForCallsOnceCurrentResultPublisherPublishCalculationResult()
    {
      for (var i = 0; i < _previousCalculationResults.Length; i++)
      {
        var _previousCalculationResult = _previousCalculationResults[i];

        var _nextCalculationResult = _nextCalculationResults[i];

        Add(
          _previousCalculationResult.Input,
          _previousCalculationResult.Output,
          _nextCalculationResult.Input,
          _nextCalculationResult.Output);
      }
    }
  }
}
