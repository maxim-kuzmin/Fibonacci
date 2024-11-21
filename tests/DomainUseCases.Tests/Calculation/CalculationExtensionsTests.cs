namespace Fibonacci.DomainUseCases.Tests.Calculation;

public class CalculationExtensionsTests
{
  private static readonly CalculationResult[] _calculationResults =
    [new(0, 0), new(1, 1), new(-1, 1), new(1, -1), new(-1, -1)];

  private static readonly Guid _calculationId = Guid.NewGuid();

  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToCalculationResult_CalculationResultDTO_ReturnsCalculationResult(
    string sutInput,
    string sutOutput,
    BigInteger expectedInput,
    BigInteger expectedOutput)
  {
    CalculationResultDTO sut = new(_calculationId, new(sutInput, sutOutput));

    var actual = sut.ToCalculationResult();

    CalculationResult expected = new(expectedInput, expectedOutput);

    Assert.Equal(expected, actual);
  }

  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToCalculationResult_CalculationSendResultActionCommand_ReturnsCalculationResult(
    string sutInput,
    string sutOutput,
    BigInteger expectedInput,
    BigInteger expectedOutput)
  {
    CalculationSendResultActionCommand sut = new(_calculationId, new(sutInput, sutOutput));

    var actual = sut.ToCalculationResult();

    CalculationResult expected = new(expectedInput, expectedOutput);

    Assert.Equal(expected, actual);
  }

  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToCalculationResultDTO_CalculationResult_ReturnsCalculationResultDTO(
    string expectedInput,
    string expectedOutput,
    BigInteger sutInput,
    BigInteger sutOutput)
  {
    CalculationResult sut = new(sutInput, sutOutput);

    var actual = sut.ToCalculationResultDTO(_calculationId);

    CalculationResultDTO expected = new(_calculationId, new(expectedInput, expectedOutput));

    Assert.Equal(expected, actual);
  }

  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToCalculationResultDTO_CalculationResult_ReturnsCalculationSendResultActionCommand(
    string expectedInput,
    string expectedOutput,
    BigInteger sutInput,
    BigInteger sutOutput)
  {
    CalculationResult sut = new(sutInput, sutOutput);

    var actual = sut.ToCalculationSendResultActionCommand(_calculationId);

    CalculationSendResultActionCommand expected = new(_calculationId, new(expectedInput, expectedOutput));

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
