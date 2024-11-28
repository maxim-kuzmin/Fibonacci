namespace Fibonacci.DomainModel.Test.Calculation.Result.Theory;

public class CalculationResultTestTheoryFullData : TheoryData<BigInteger, BigInteger, BigInteger, BigInteger>
{
  public CalculationResultTestTheoryFullData()
  {
    for (var i = 0; i < CalculationResultTestData.CalculationResultCount; i++)
    {
      var previousCalculationResult = CalculationResultTestData.GetPreviousCalculationResultByIndex(i);

      var nextCalculationResult = CalculationResultTestData.GetNextCalculationResultByIndex(i);

      Add(
        previousCalculationResult.Input,
        previousCalculationResult.Output,
        nextCalculationResult.Input,
        nextCalculationResult.Output);
    }
  }
}
