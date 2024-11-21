namespace Fibonacci.DomainUseCases.Tests.Calculation;

public class CalculationExtensionsTests
{
  private static readonly CalculationResult[] _calculationResults =
    [new(0, 0), new(1, 1), new(-1, 1), new(1, -1), new(-1, -1)];

  private static readonly Guid _calculationId = Guid.NewGuid();

  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToCalculationResult_CalculationResultDTO_ReturnsCalculationResult(
    string dtoInput,
    string dtoOutput,
    BigInteger input,
    BigInteger output)
  {
    CalculationResultDTO dto = new(_calculationId, new(dtoInput, dtoOutput));

    CalculationResult calculationResult = new(input, output);

    var actual = dto.ToCalculationResult();

    Assert.Equal(calculationResult, actual);
  }

  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToCalculationResult_CalculationSendResultActionCommand_ReturnsCalculationResult(
    string commandInput,
    string commandOutput,
    BigInteger input,
    BigInteger output)
  {
    CalculationSendResultActionCommand command = new(_calculationId, new(commandInput, commandOutput));

    CalculationResult calculationResult = new(input, output);

    var actual = command.ToCalculationResult();

    Assert.Equal(calculationResult, actual);
  }

  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToCalculationResultDTO_CalculationResult_ReturnsCalculationResultDTO(
    string dtoInput,
    string dtoOutput,
    BigInteger input,
    BigInteger output)
  {
    CalculationResultDTO dto = new(_calculationId, new(dtoInput, dtoOutput));

    CalculationResult calculationResult = new(input, output);

    var actual = calculationResult.ToCalculationResultDTO(_calculationId);

    Assert.Equal(dto, actual);
  }

  [Theory]
  [ClassData(typeof(TestTheoryData))]
  public void ToCalculationResultDTO_CalculationResult_ReturnsCalculationSendResultActionCommand(
    string commandInput,
    string commandOutput,
    BigInteger input,
    BigInteger output)
  {
    CalculationSendResultActionCommand command = new(_calculationId, new(commandInput, commandOutput));

    CalculationResult calculationResult = new(input, output);

    var actual = calculationResult.ToCalculationSendResultActionCommand(_calculationId);

    Assert.Equal(command, actual);
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
