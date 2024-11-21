namespace Fibonacci.DomainUseCases.Tests.Calculation;

public class CalculationExtensionsTests
{
  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToCalculationResult_CalculationResultDTO_ReturnsCalculationResult(
    string serializedCalculationId,
    string serializableInput,
    string serializableOutput,
    BigInteger input,
    BigInteger output)
  {
    var calculationId = Guid.Parse(serializedCalculationId);

    CalculationResultDTO calculationResultDTO = new(calculationId, new(serializableInput, serializableOutput));

    CalculationResult calculationResult = new(input, output);

    var actual = calculationResultDTO.ToCalculationResult();

    Assert.Equal(calculationResult, actual);
  }

  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToCalculationResultDTO_CalculationResult_ReturnsCalculationResultDTO(
    string serializedCalculationId,
    string serializableInput,
    string serializableOutput,
    BigInteger input,
    BigInteger output)
  {
    var calculationId = Guid.Parse(serializedCalculationId);

    CalculationResultDTO calculationResultDTO = new(calculationId, new(serializableInput, serializableOutput));

    CalculationResult calculationResult = new(input, output);

    var actual = calculationResult.ToCalculationResultDTO(calculationId);

    Assert.Equal(calculationResultDTO, actual);
  }

  private class TestTheoryData : TheoryData<string, string, string, BigInteger, BigInteger>
  {
    public TestTheoryData()
    {
      var calculationId = Guid.NewGuid().ToString();

      Add(calculationId, "0", "0", 0, 0);
      Add(calculationId, "1", "1", 1, 1);
      Add(calculationId, "-1", "1", -1, 1);
      Add(calculationId, "1", "-1", 1, -1);
      Add(calculationId, "-1", "-1", -1, -1);
    }
  }

  private class TestTheoryData1 : TheoryData<string>
  {
    public TestTheoryData1()
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
