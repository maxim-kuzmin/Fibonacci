namespace Fibonacci.DomainModel.UnitTests.Calculation;

public class CalculationExtensionsTests
{
  [Theory]
  [ClassData(typeof(CalculationExtensionsTestTheoryData))]
  public void ToCalculationResult_SerializableCalculationResult_ReturnsCalculationResult(
    string serializableInput,
    string serializableOutput,
    BigInteger input,
    BigInteger output)
  {
    SerializableCalculationResult sut = new(serializableInput, serializableOutput);

    CalculationResult expected = new(input, output);

    var actual = sut.ToCalculationResult();

    Assert.Equal(expected, actual);
  }

  [Theory]
  [ClassData(typeof(CalculationExtensionsTestTheoryData))]
  public void ToSerializableCalculationResult_CalculationResult_ReturnsSerializableCalculationResult(
    string serializableInput,
    string serializableOutput,
    BigInteger input,
    BigInteger output)
  {
    CalculationResult sut = new(input, output);

    SerializableCalculationResult expected = new(serializableInput, serializableOutput);

    var actual = sut.ToSerializableCalculationResult();

    Assert.Equal(expected, actual);
  }
}
