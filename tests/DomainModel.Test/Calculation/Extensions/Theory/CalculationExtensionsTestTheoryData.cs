namespace Fibonacci.DomainModel.Test.Calculation.Extensions.Theory;

public class CalculationExtensionsTestTheoryData : TheoryData<string, string, BigInteger, BigInteger>
{
  public CalculationExtensionsTestTheoryData()
  {
    for (int i = 0; i < CalculationExtensionsTestData.CalculationResultCount; i++)
    {
      var calculationResult = CalculationExtensionsTestData.GetCalculationResultByIndex(i);

      var serializableCalculationResult = calculationResult.ToSerializableCalculationResult();

      Add(
        serializableCalculationResult.Input,
        serializableCalculationResult.Output,
        calculationResult.Input,
        calculationResult.Output);
    }
  }
}
