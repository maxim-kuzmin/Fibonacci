namespace Fibonacci.DomainUseCases.UnitTests.Calculation.Result;

public class CalculationResultConsumerTests
{
  private readonly CalculationResultConsumer _sut;

  private readonly AppInMemoryBus _appBus = new();

  private readonly Mock<ICalculationNextResultPublisher> _calculationNextResultPublisherMock = new();

  public CalculationResultConsumerTests()
  {
    _sut = new CalculationResultConsumer(
      CalculationResultTestData.CalculationId,
      _appBus,
      _calculationNextResultPublisherMock.Object);
    
    for (int i = 0; i < CalculationResultTestData.CalculationResultCount; i++)
    {
      var nextCalculationResult = CalculationResultTestData.GetNextCalculationResultByIndex(i);

      var previousCalculationResult = CalculationResultTestData.GetPreviousCalculationResultByIndex(i);

      var calculationResultDTO = nextCalculationResult.ToCalculationResultDTO(CalculationResultTestData.CalculationId);

      _calculationNextResultPublisherMock
        .Setup(x => x.PublishCalculationResult(
          CalculationResultTestData.CalculationId,
          previousCalculationResult,
          default))
        .Callback(() => _appBus.Publish(
          CalculationResultTestData.CalculationId.ToString(),
          calculationResultDTO,
          default));
    }
  }

  [Theory]
  [ClassData(typeof(CalculationResultTestTheoryPreviousData))]
  public async Task GetNextCalculationResult_PreviousCalculationResult_CallsOnceServiceGetNextCalculationResult(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    await _sut.GetNextCalculationResult(previousCalculationResult, default);

    _calculationNextResultPublisherMock.Verify(
      x => x.PublishCalculationResult(
        CalculationResultTestData.CalculationId,
        previousCalculationResult,
        default),
      Times.Once());
  }

  [Theory]
  [ClassData(typeof(CalculationResultTestTheoryFullData))]
  public async Task GetNextCalculationResult_PreviousCalculationResult_ReturnsNextCalculationResult(
    BigInteger previousCalculationResultInput,
    BigInteger previousCalculationResultOutput,
    BigInteger nextCalculationResultInput,
    BigInteger nextCalculationResultOutput)
  {
    CalculationResult previousCalculationResult = new(previousCalculationResultInput, previousCalculationResultOutput);

    CalculationResult expected = new(nextCalculationResultInput, nextCalculationResultOutput);

    var actual = await _sut.GetNextCalculationResult(previousCalculationResult, default);

    Assert.Equal(expected, actual);
  }
}
