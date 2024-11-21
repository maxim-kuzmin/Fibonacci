namespace Fibonacci.DomainModel.Tests.Calculation;

public class CalculationExtensionsTests
{
  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToCalculationResult_SerializableCalculationResult_ReturnsCalculationResult(
    string serializableInput,
    string serializableOutput,
    BigInteger input,
    BigInteger output)
  {
    SerializableCalculationResult serializableCalculationResult = new(serializableInput, serializableOutput);

    CalculationResult calculationResult = new(input, output);

    var actual = serializableCalculationResult.ToCalculationResult();

    Assert.Equal(calculationResult, actual);
  }

  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToSerializableCalculationResult_CalculationResult_ReturnsSerializableCalculationResult(
    string serializableInput,
    string serializableOutput,
    BigInteger input,
    BigInteger output)
  {
    SerializableCalculationResult serializableCalculationResult = new(serializableInput, serializableOutput);

    CalculationResult calculationResult = new(input, output);

    var actual = calculationResult.ToSerializableCalculationResult();

    Assert.Equal(serializableCalculationResult, actual);
  }

  private class TestTheoryData : TheoryData<string, string, BigInteger, BigInteger>
  {
    public TestTheoryData()
    {
      Add("0", "0", 0, 0);
      Add("1", "1", 1, 1);
      Add("-1", "1", -1, 1);
      Add("1", "-1", 1, -1);
      Add("-1", "-1", -1, -1);
    }
  }
}
