namespace Fibonacci.DomainModel.Tests.Calculation;

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

    var actual = sut.ToCalculationResult();

    CalculationResult expected = new(input, output);

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

    var actual = sut.ToSerializableCalculationResult();

    SerializableCalculationResult expected = new(serializableInput, serializableOutput);

    Assert.Equal(expected, actual);
  }
}
