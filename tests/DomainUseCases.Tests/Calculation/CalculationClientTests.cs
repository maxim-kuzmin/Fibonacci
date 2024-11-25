namespace Fibonacci.DomainUseCases.Tests.Calculation;

public class CalculationClientTests
{
  private readonly CalculationClient _sut;

  private readonly Mock<ICalculationService> _calculationServiceMock = new();

  public CalculationClientTests()
  {
    _sut = new(_calculationServiceMock.Object);

    for (int i = 0; i < CalculationResultTestData.CalculationResultCount; i++)
    {
      var previousCalculationResult = CalculationResultTestData.GetPreviousCalculationResultByIndex(i);

      var nextCalculationResult = CalculationResultTestData.GetNextCalculationResultByIndex(i);

      var previousCalculationResultDTO = previousCalculationResult.ToCalculationResultDTO(
        CalculationResultTestData.CalculationId);

      var nextCalculationResultDTO = nextCalculationResult.ToCalculationResultDTO(
        CalculationResultTestData.CalculationId);

      _calculationServiceMock.Setup(x => x.GetNextCalculationResult(previousCalculationResultDTO))
        .Returns(nextCalculationResultDTO);
    }
  }

  [Theory]
  [ClassData(typeof(CalculationResultTestTheoryPreviousData))]
  public void GetNextCalculationResult_PreviousCalculationResult_CallsOnceServiceGetNextCalculationResult(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    var previousCalculationResultDTO = previousCalculationResult.ToCalculationResultDTO(
      CalculationResultTestData.CalculationId);

    _sut.GetNextCalculationResult(CalculationResultTestData.CalculationId, previousCalculationResult);

    _calculationServiceMock.Verify(x => x.GetNextCalculationResult(previousCalculationResultDTO), Times.Once());
  }

  [Theory]
  [ClassData(typeof(CalculationResultTestTheoryFullData))]
  public void GetNextCalculationResult_PreviousCalculationResult_ReturnsNextCalculationResult(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput,
    BigInteger nextCalculationResultInput,
    BigInteger nextCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    var actual = _sut.GetNextCalculationResult(CalculationResultTestData.CalculationId, previousCalculationResult);

    CalculationResult expected = new(nextCalculationResultInput, nextCalculationResultOutput);

    Assert.Equal(expected, actual);
  }
}
