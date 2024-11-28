namespace Fibonacci.DomainModel.Test.Calculation.Result.Theory;

public class CalculationResultTestTheoryPreviousData : TheoryData<BigInteger, BigInteger>
{
  public CalculationResultTestTheoryPreviousData()
  {
    for (var i = 0; i < CalculationResultTestData.CalculationResultCount; i++)
    {
      var previousCalculationResult = CalculationResultTestData.GetPreviousCalculationResultByIndex(i);

      Add(previousCalculationResult.Input, previousCalculationResult.Output);
    }
  }
}
