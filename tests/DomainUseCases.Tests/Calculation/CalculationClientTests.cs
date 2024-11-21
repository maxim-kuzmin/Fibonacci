namespace Fibonacci.DomainUseCases.Tests.Calculation;

public class CalculationClientTests
{
  private readonly CalculationClient _sut;

  private readonly Mock<ICalculationService> _calculationServiceMock = new();

  private static readonly CalculationResult[] _previousCalculationResults =
    [new(0, 0), new(1, 1), new(2, 1), new(3, 2), new(4, 3), new(5, 5)];

  private static readonly CalculationResult[] _nextCalculationResults =
    [new(1, 1), new(2, 1), new(3, 2), new(4, 3), new(5, 5), new(6, 8)];

  private static readonly Guid _calculationId = Guid.NewGuid();

  public CalculationClientTests()
  {
    _sut = new(_calculationServiceMock.Object);

    for (int i = 0; i < _previousCalculationResults.Length; i++)
    {
      var previousCalculationResultDTO = _previousCalculationResults[i].ToCalculationResultDTO(_calculationId);

      var nextCalculationResultDTO = _nextCalculationResults[i].ToCalculationResultDTO(_calculationId);

      _calculationServiceMock.Setup(x => x.GetNextCalculationResult(previousCalculationResultDTO))
        .Returns(nextCalculationResultDTO);
    }
  }

  [Theory]
  [ClassData(typeof(GetNextCalculationResultTestTheoryDataForCallsOnce))]
  public void GetNextCalculationResult_PreviousCalculationResult_CallsOnceServiceGetNextCalculationResult(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    var previousCalculationResultDTO = previousCalculationResult.ToCalculationResultDTO(_calculationId);

    _sut.GetNextCalculationResult(_calculationId, previousCalculationResult);

    _calculationServiceMock.Verify(x => x.GetNextCalculationResult(previousCalculationResultDTO), Times.Once());
  }

  [Theory]
  [ClassData(typeof(GetNextCalculationResultTestTheoryDataForReturns))]
  public void GetNextCalculationResult_PreviousCalculationResult_ReturnsNextCalculationResult(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput,
    BigInteger nextCalculationResultInput,
    BigInteger nextCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    var actual = _sut.GetNextCalculationResult(_calculationId, previousCalculationResult);

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
