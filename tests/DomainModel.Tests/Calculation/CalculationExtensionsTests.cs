namespace Fibonacci.DomainModel.Tests.Calculation;

public class CalculationExtensionsTests
{
  private static readonly CalculationResult[] _calculationResults =
    [new(0, 0), new(1, 1), new(-1, 1), new(1, -1), new(-1, -1)];

  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToCalculationResult_SerializableCalculationResult_ReturnsCalculationResult(
    string serializableInput,
    string serializableOutput,
    BigInteger input,
    BigInteger output)
  {
    SerializableCalculationResult sut = new(serializableInput, serializableOutput);

    var actual = sut.ToCalculationResult();

    CalculationResult expected = new(input, output);

    Assert.Equal(expected, actual);
  }

  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToSerializableCalculationResult_CalculationResult_ReturnsSerializableCalculationResult(
    string serializableInput,
    string serializableOutput,
    BigInteger input,
    BigInteger output)
  {
    CalculationResult sut = new(input, output);

    var actual = sut.ToSerializableCalculationResult();

    SerializableCalculationResult expected = new(serializableInput, serializableOutput);

    Assert.Equal(expected, actual);
  }

  private class TestTheoryData : TheoryData<string, string, BigInteger, BigInteger>
  {
    public TestTheoryData()
    {
      foreach (var calculationResult in _calculationResults)
      {
        var serializableCalculationResult = calculationResult.ToSerializableCalculationResult();

        Add(
          serializableCalculationResult.Input,
          serializableCalculationResult.Output,
          calculationResult.Input,
          calculationResult.Output);
      }
    }
  }
}
