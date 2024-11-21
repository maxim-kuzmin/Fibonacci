﻿namespace Fibonacci.DomainModel.Tests.Calculation;

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
