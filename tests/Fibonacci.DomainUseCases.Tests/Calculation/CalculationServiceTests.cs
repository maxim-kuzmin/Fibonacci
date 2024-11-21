namespace Fibonacci.DomainUseCases.Tests.Calculation;

public class CalculationServiceTests
{
  private readonly Mock<ICalculationLogicService> _calculationLogicServiceMock = new();

  private readonly Mock<ICalculationLogicServiceFactory> _calculationLogicServiceFactoryMock = new();

  private readonly CalculationService _calculationService;

  private static readonly CalculationResult[] _previousCalculationResults =
    [new(0, 0), new(1, 1), new(2, 1), new(3, 2), new(4, 3), new(5, 5)];

  private static readonly CalculationResult[] _nextCalculationResults =
    [new(1, 1), new(2, 1), new(3, 2), new(4, 3), new(5, 5), new(6, 8)];

  public CalculationServiceTests()
  {
    _calculationService = new CalculationService(_calculationLogicServiceFactoryMock.Object);

    _calculationLogicServiceFactoryMock.Setup(x => x.CreateCalculationLogicService())
      .Returns(_calculationLogicServiceMock.Object);

    for (int i = 0; i < _previousCalculationResults.Length; i++)
    {
      _calculationLogicServiceMock.Setup(x => x.GetNextCalculationResult(_previousCalculationResults[i]))
        .Returns(_nextCalculationResults[i]);
    }
  }

  [Theory]
  [ClassData(typeof(GetNextCalculationResultTestTheoryData))]
  public void GetNextCalculationResult_ValidPreviousCalculationResultDTO_ReturnsNextCalculationResultDTO(
    string serializedData)
  {
    var data = GetNextCalculationResultTestTheoryData.ParseData(serializedData);

    var actual = _calculationService.GetNextCalculationResult(data.PreviousCalculationResultDTO);

    Assert.Equal(data.NextCalculationResultDTO, actual);
  }

  private class GetNextCalculationResultTestTheoryData : TheoryData<string>
  {
    public GetNextCalculationResultTestTheoryData()
    {
      var calculationId = Guid.NewGuid();

      for (int i = 0; i < _previousCalculationResults.Length; i++)
      {
        GetNextCalculationResultTestData data = new(
          _previousCalculationResults[i].ToCalculationResultDTO(calculationId),
          _nextCalculationResults[i].ToCalculationResultDTO(calculationId));

        Add(JsonSerializer.Serialize(data));
      }
    }

    public static GetNextCalculationResultTestData ParseData(string serializedData)
    {
      return JsonSerializer.Deserialize<GetNextCalculationResultTestData>(serializedData)!;
    }
  }

  private record GetNextCalculationResultTestData(
    CalculationResultDTO PreviousCalculationResultDTO,
    CalculationResultDTO NextCalculationResultDTO);
}
