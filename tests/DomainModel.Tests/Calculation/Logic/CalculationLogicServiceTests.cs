namespace Fibonacci.DomainModel.Tests.Calculation.Logic;

public class CalculationLogicServiceTests
{
  private readonly CalculationLogicService _calculationLogicService = new();

  [Theory]
  [InlineData(-1, 0)]
  [InlineData(0, -1)]
  public void GetNextCalculationResult_NegativeInputOrOutput_ThrowsArgumentOutOfRangeException(
    BigInteger input,
    BigInteger output)
  {
    var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
      _calculationLogicService.GetNextCalculationResult(new CalculationResult(input, output)));

    if (input < 0)
    {
      Assert.Equal(nameof(CalculationResult.Input), ex.ParamName);
    }

    if (output < 0)
    {
      Assert.Equal(nameof(CalculationResult.Output), ex.ParamName);
    }
  }

  [Theory]
  [ClassData(typeof(GetNextCalculationResultFirstAppTheoryData))]
  [ClassData(typeof(GetNextCalculationResultSecondAppTheoryData))]
  public void GetNextCalculationResult_ValidPreviousCalculationResult_ReturnsNextCalculationResult(
    string serializedData)
  {
    var data = GetNextCalculationResultTheoryData.ParseData(serializedData);

    ArrangeCalculationLogicService(data.PreviousCalculationResults.Select(x => x.ToCalculationResult()));

    var actual = _calculationLogicService.GetNextCalculationResult(data.PreviousCalculationResult.ToCalculationResult());

    Assert.Equal(data.NextCalculationResult.ToCalculationResult(), actual);
  }

  private void ArrangeCalculationLogicService(IEnumerable<CalculationResult> previousCalculationResults)
  {
    foreach (var previousCalculationResult in previousCalculationResults)
    {
      _calculationLogicService.GetNextCalculationResult(previousCalculationResult);
    }
  }

  private class GetNextCalculationResultFirstAppTheoryData : GetNextCalculationResultTheoryData
  {
    public GetNextCalculationResultFirstAppTheoryData()
    {
      AddData(
        [new(0, 0), new(2, 1), new(4, 3)],
        [new(1, 1), new(3, 2), new(5, 5)]);
    }
  }

  private class GetNextCalculationResultSecondAppTheoryData : GetNextCalculationResultTheoryData
  {
    public GetNextCalculationResultSecondAppTheoryData()
    {
      AddData(
        [new(1, 1), new(3, 2), new(5, 5)],
        [new(2, 1), new(4, 3), new(6, 8)]);
    }
  }

  private class GetNextCalculationResultTheoryData : TheoryData<string>
  {
    protected void AddData(CalculationResult[] previousCalculationResults, CalculationResult[] nextCalculationResults)
    {
      for (int i = 0; i < previousCalculationResults.Length; i++)
      {
        GetNextCalculationResultData data = new(
          TestCalculationResult.FromCalculationResult(previousCalculationResults[i]),
          TestCalculationResult.FromCalculationResult(nextCalculationResults[i]),
          previousCalculationResults[..i].Select(x => TestCalculationResult.FromCalculationResult(x)).ToArray());

        Add(JsonSerializer.Serialize(data));
      }
    }

    public static GetNextCalculationResultData ParseData(string serializedData)
    {
      return JsonSerializer.Deserialize<GetNextCalculationResultData>(serializedData)!;
    }
  }

  private record GetNextCalculationResultData(
    TestCalculationResult PreviousCalculationResult,
    TestCalculationResult NextCalculationResult,
    IEnumerable<TestCalculationResult> PreviousCalculationResults);

  private record TestCalculationResult(string Input, string Output)
  {
    public CalculationResult ToCalculationResult()
    {
      return new CalculationResult(BigInteger.Parse(Input), BigInteger.Parse(Output));
    }

    public static TestCalculationResult FromCalculationResult(CalculationResult calculationResult)
    {
      return new TestCalculationResult(calculationResult.Input.ToString(), calculationResult.Output.ToString());
    }
  }
}
