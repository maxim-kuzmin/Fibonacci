namespace Fibonacci.DomainUseCases.Tests.Calculation;

public class CalculationServiceTests
{
  private readonly CalculationService _sut;

  private readonly Mock<ICalculationLogicService> _calculationLogicServiceMock = new();

  private readonly Mock<ICalculationLogicServiceFactory> _calculationLogicServiceFactoryMock = new();  

  public CalculationServiceTests()
  {
    _sut = new CalculationService(_calculationLogicServiceFactoryMock.Object);

    _calculationLogicServiceFactoryMock.Setup(x => x.CreateCalculationLogicService())
      .Returns(_calculationLogicServiceMock.Object);
    
    for (int i = 0; i < CalculationResultTestData.CalculationResultCount; i++)
    {
      var previousCalculationResult = CalculationResultTestData.GetPreviousCalculationResultByIndex(i);

      var nextCalculationResult = CalculationResultTestData.GetNextCalculationResultByIndex(i);

      _calculationLogicServiceMock.Setup(x => x.GetNextCalculationResult(previousCalculationResult))
        .Returns(nextCalculationResult);
    }
  }

  [Theory]
  [ClassData(typeof(CalculationResultTestTheoryPreviousData))]
  public void GetNextCalculationResult_PreviousCalculationResultDTO_CallsOnceLogicServiceGetNextCalculationResult(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    var previousCalculationResultDTO = previousCalculationResult.ToCalculationResultDTO(
      CalculationResultTestData.CalculationId);

    _sut.GetNextCalculationResult(previousCalculationResultDTO);

    _calculationLogicServiceMock.Verify(x => x.GetNextCalculationResult(previousCalculationResult), Times.Once());
  }

  [Theory]
  [ClassData(typeof(CalculationResultTestTheoryFullData))]
  public void GetNextCalculationResult_PreviousCalculationResultDTO_ReturnsNextCalculationResultDTO(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput,
    BigInteger nextCalculationResultInput,
    BigInteger nextCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    CalculationResult nextCalculationResult = new(nextCalculationResultInput, nextCalculationResultOutput);

    var previousCalculationResultDTO = previousCalculationResult.ToCalculationResultDTO(
      CalculationResultTestData.CalculationId);

    var actual = _sut.GetNextCalculationResult(previousCalculationResultDTO);

    var expected = nextCalculationResult.ToCalculationResultDTO(CalculationResultTestData.CalculationId);

    Assert.Equal(expected, actual);
  }
}
