namespace Fibonacci.DomainUseCases.UnitTests.Calculation;

public class CalculationExtensionsTests
{
  [Theory]
  [ClassData(typeof(CalculationExtensionsTestTheoryData))]
  public void ToCalculationResult_CalculationResultDTO_ReturnsCalculationResult(
    string sutInput,
    string sutOutput,
    BigInteger expectedInput,
    BigInteger expectedOutput)
  {
    CalculationResultDTO sut = new(CalculationExtensionsTestData.CalculationId, new(sutInput, sutOutput));

    CalculationResult expected = new(expectedInput, expectedOutput);

    var actual = sut.ToCalculationResult();

    Assert.Equal(expected, actual);
  }

  [Theory]
  [ClassData(typeof(CalculationExtensionsTestTheoryData))]
  public void ToCalculationResult_CalculationSendResultActionCommand_ReturnsCalculationResult(
    string sutInput,
    string sutOutput,
    BigInteger expectedInput,
    BigInteger expectedOutput)
  {
    CalculationSendResultActionCommand sut = new(
      CalculationExtensionsTestData.CalculationId,
      new(sutInput, sutOutput));

    CalculationResult expected = new(expectedInput, expectedOutput);

    var actual = sut.ToCalculationResult();

    Assert.Equal(expected, actual);
  }

  [Theory]
  [ClassData(typeof(CalculationExtensionsTestTheoryData))]
  public void ToCalculationResultDTO_CalculationResult_ReturnsCalculationResultDTO(
    string expectedInput,
    string expectedOutput,
    BigInteger sutInput,
    BigInteger sutOutput)
  {
    CalculationResult sut = new(sutInput, sutOutput);

    CalculationResultDTO expected = new(
      CalculationExtensionsTestData.CalculationId,
      new(expectedInput, expectedOutput));

    var actual = sut.ToCalculationResultDTO(CalculationExtensionsTestData.CalculationId);

    Assert.Equal(expected, actual);
  }

  [Theory]
  [ClassData(typeof(CalculationExtensionsTestTheoryData))]
  public void ToCalculationResultDTO_CalculationResult_ReturnsCalculationSendResultActionCommand(
    string expectedInput,
    string expectedOutput,
    BigInteger sutInput,
    BigInteger sutOutput)
  {
    CalculationResult sut = new(sutInput, sutOutput);

    CalculationSendResultActionCommand expected = new(
      CalculationExtensionsTestData.CalculationId,
      new(expectedInput, expectedOutput));

    var actual = sut.ToCalculationSendResultActionCommand(CalculationExtensionsTestData.CalculationId);

    Assert.Equal(expected, actual);
  }
}
