namespace Fibonacci.Infrastructure.Tests.Calculation.Result;

public class CalculationResultConsumerTests
{
  private readonly CalculationResultConsumer _sut;

  private readonly AppInMemoryBus _appBus = new();

  private readonly Mock<ICalculationNextResultPublisher> _calculationNextResultPublisherMock = new();

  private static readonly CalculationResult[] _previousCalculationResults =
    [new(0, 0), new(1, 1), new(2, 1), new(3, 2), new(4, 3), new(5, 5)];

  private static readonly CalculationResult[] _nextCalculationResults =
    [new(1, 1), new(2, 1), new(3, 2), new(4, 3), new(5, 5), new(6, 8)];

  private static readonly Guid _calculationId = Guid.NewGuid();

  public CalculationResultConsumerTests()
  {
    _sut = new CalculationResultConsumer(_calculationId, _appBus, _calculationNextResultPublisherMock.Object);
    
    for (int i = 0; i < _previousCalculationResults.Length; i++)
    {
      var calculationResultDTO = _nextCalculationResults[i].ToCalculationResultDTO(_calculationId);

      _calculationNextResultPublisherMock
        .Setup(x => x.PublishCalculationResult(_calculationId, _previousCalculationResults[i], CancellationToken.None))
        .Callback(() => _appBus.Publish(_calculationId.ToString(), calculationResultDTO, CancellationToken.None));
    }
  }

  [Theory]
  [ClassData(typeof(GetNextCalculationResultTestTheoryDataForCallsOnce))]
  public async Task GetNextCalculationResult_PreviousCalculationResult_CallsOnceServiceGetNextCalculationResult(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    await _sut.GetNextCalculationResult(previousCalculationResult, CancellationToken.None);

    _calculationNextResultPublisherMock.Verify(
      x => x.PublishCalculationResult(_calculationId, previousCalculationResult, CancellationToken.None),
      Times.Once());
  }

  [Theory]
  [ClassData(typeof(GetNextCalculationResultTestTheoryDataForReturns))]
  public async Task GetNextCalculationResult_PreviousCalculationResult_ReturnsNextCalculationResult(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput,
    BigInteger nextCalculationResultInput,
    BigInteger nextCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    var actual = await _sut.GetNextCalculationResult(previousCalculationResult, CancellationToken.None);

    CalculationResult expected = new(nextCalculationResultInput, nextCalculationResultOutput);

    Assert.Equal(expected, actual);
  }

  private class GetNextCalculationResultTestTheoryDataForCallsOnce : TheoryData<BigInteger, BigInteger>
  {
    public GetNextCalculationResultTestTheoryDataForCallsOnce()
    {
      for (int i = 0; i < _previousCalculationResults.Length; i++)
      {
        var _previousCalculationResult = _previousCalculationResults[i];

        Add(_previousCalculationResult.Input, _previousCalculationResult.Output);
      }
    }
  }

  private class GetNextCalculationResultTestTheoryDataForReturns :
    TheoryData<BigInteger, BigInteger, BigInteger, BigInteger>
  {
    public GetNextCalculationResultTestTheoryDataForReturns()
    {
      for (int i = 0; i < _previousCalculationResults.Length; i++)
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
