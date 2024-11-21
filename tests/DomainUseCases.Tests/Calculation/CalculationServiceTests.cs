namespace Fibonacci.DomainUseCases.Tests.Calculation;

public class CalculationServiceTests
{
  private readonly CalculationService _sut;

  private readonly Mock<ICalculationLogicService> _calculationLogicServiceMock = new();

  private readonly Mock<ICalculationLogicServiceFactory> _calculationLogicServiceFactoryMock = new();  

  private static readonly CalculationResult[] _previousCalculationResults =
    [new(0, 0), new(1, 1), new(2, 1), new(3, 2), new(4, 3), new(5, 5)];

  private static readonly CalculationResult[] _nextCalculationResults =
    [new(1, 1), new(2, 1), new(3, 2), new(4, 3), new(5, 5), new(6, 8)];

  private static readonly Guid _calculationId = Guid.NewGuid();

  public CalculationServiceTests()
  {
    _sut = new CalculationService(_calculationLogicServiceFactoryMock.Object);

    _calculationLogicServiceFactoryMock.Setup(x => x.CreateCalculationLogicService())
      .Returns(_calculationLogicServiceMock.Object);

    for (int i = 0; i < _previousCalculationResults.Length; i++)
    {
      _calculationLogicServiceMock.Setup(x => x.GetNextCalculationResult(_previousCalculationResults[i]))
        .Returns(_nextCalculationResults[i]);
    }
  }

  [Theory]
  [ClassData(typeof(GetNextCalculationResultTestTheoryDataForCallsOnce))]
  public void GetNextCalculationResult_PreviousCalculationResultDTO_CallsOnceLogicServiceGetNextCalculationResult(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    var previousCalculationResultDTO = previousCalculationResult.ToCalculationResultDTO(_calculationId);

    _sut.GetNextCalculationResult(previousCalculationResultDTO);

    _calculationLogicServiceMock.Verify(x => x.GetNextCalculationResult(previousCalculationResult), Times.Once());
  }

  [Theory]
  [ClassData(typeof(GetNextCalculationResultTestTheoryDataForReturns))]
  public void GetNextCalculationResult_PreviousCalculationResultDTO_ReturnsNextCalculationResultDTO(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput,
    BigInteger nextCalculationResultInput,
    BigInteger nextCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    CalculationResult nextCalculationResult = new(nextCalculationResultInput, nextCalculationResultOutput);

    var previousCalculationResultDTO = previousCalculationResult.ToCalculationResultDTO(_calculationId);

    var actual = _sut.GetNextCalculationResult(previousCalculationResultDTO);

    var expected = nextCalculationResult.ToCalculationResultDTO(_calculationId);

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
