namespace Fibonacci.DomainUseCases.Tests.Calculation;

public class CalculationExtensionsTests
{
  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToCalculationResult_ValidCalculationResultDTO_ReturnsCalculationResult(string serializedData)
  {
    var data = TestTheoryData.ParseData(serializedData);

    var actual = data.CalculationResultDTO.ToCalculationResult();

    Assert.Equal(data.CalculationResult.ToCalculationResult(), actual);
  }

  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToCalculationResultDTO_ValidCalculationResult_ReturnsCalculationResultDTO(string serializedData)
  {
    var data = TestTheoryData.ParseData(serializedData);

    var actual = data.CalculationResult
      .ToCalculationResult()
      .ToCalculationResultDTO(data.CalculationResultDTO.CalculationId);

    Assert.Equal(data.CalculationResultDTO, actual);
  }

  private class TestTheoryData : TheoryData<string>
  {
    public TestTheoryData()
    {
      var calculationId = Guid.NewGuid();

      CalculationResult calculationResult = new(1, 1);

      var data = new TestData(
        calculationResult.ToSerializableCalculationResult(),
        calculationResult.ToCalculationResultDTO(calculationId));

      Add(JsonSerializer.Serialize(data));
    }

    public static TestData ParseData(string serializedData)
    {
      return JsonSerializer.Deserialize<TestData>(serializedData)!;
    }
  }

  private record TestData(
    SerializableCalculationResult CalculationResult,
    CalculationResultDTO CalculationResultDTO);
}
