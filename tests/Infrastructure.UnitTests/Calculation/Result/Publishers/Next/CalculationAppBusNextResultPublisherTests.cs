namespace Fibonacci.Infrastructure.UnitTests.Calculation.Result.Publishers.Next;

public class CalculationAppBusNextResultPublisherTests
{
  private readonly CalculationAppBusNextResultPublisher _sut;

  private readonly Mock<ICalculationService> _calculationServiceMock = new();

  private readonly Mock<ICalculationCurrentResultPublisher> _calculationCurrentResultPublisherMock = new();

  public CalculationAppBusNextResultPublisherTests()
  {
    _sut = new CalculationAppBusNextResultPublisher(
      _calculationServiceMock.Object,
      _calculationCurrentResultPublisherMock.Object);

    for (var i = 0; i < CalculationResultTestData.CalculationResultCount; i++)
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
  public async Task PublishCalculationResult_CalculationResult_CallsOnceServiceGetNextCalculationResult(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    var previousCalculationResultDTO = previousCalculationResult.ToCalculationResultDTO(
      CalculationResultTestData.CalculationId);

    await _sut.PublishCalculationResult(
      CalculationResultTestData.CalculationId,
      previousCalculationResult,
      CancellationToken.None);

    _calculationServiceMock.Verify(x => x.GetNextCalculationResult(previousCalculationResultDTO), Times.Once());
  }

  [Theory]
  [ClassData(typeof(CalculationResultTestTheoryFullData))]
  public async Task PublishCalculationResult_CalculationResult_CallsOnceCurrentResultPublisherPublishCalculationResult(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput,
    BigInteger nextCalculationResultInput,
    BigInteger nextCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    await _sut.PublishCalculationResult(
      CalculationResultTestData.CalculationId,
      previousCalculationResult,
      CancellationToken.None);

    CalculationResult nextCalculationResult = new(nextCalculationResultInput, nextCalculationResultOutput);

    _calculationCurrentResultPublisherMock.Verify(
      x => x.PublishCalculationResult(
        CalculationResultTestData.CalculationId,
        nextCalculationResult,
        CancellationToken.None),
      Times.Once());
  }
}
