namespace Fibonacci.DomainModel.UnitTests.Calculation.Logic;

public class CalculationLogicServiceTests
{
  private readonly CalculationLogicService _sut = new();

  [Theory]
  [InlineData(-1, 0)]
  [InlineData(0, -1)]
  public void GetNextCalculationResult_InvalidPreviousCalculationResult_ThrowsArgumentOutOfRangeException(
    BigInteger input,
    BigInteger output)
  {
    var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
      _sut.GetNextCalculationResult(new CalculationResult(input, output)));

    var actual = ex.ParamName;

    if (input < 0)
    {
      Assert.Equal(nameof(CalculationResult.Input), actual);
    }

    if (output < 0)
    {
      Assert.Equal(nameof(CalculationResult.Output), actual);
    }
  }

  [Theory]
  [ClassData(typeof(GetNextCalculationResultTestTheoryDataForFirstApp))]
  [ClassData(typeof(GetNextCalculationResultTestTheoryDataForSecondApp))]
  public void GetNextCalculationResult_ValidPreviousCalculationResult_ReturnsNextCalculationResult(
    string serializedData)
  {
    var data = GetNextCalculationResultTestTheoryData.ParseData(serializedData);

    ArrangeCalculationLogicService(data.PreviousCalculationResults.Select(x => x.ToCalculationResult()));

    var expected = data.NextCalculationResult.ToCalculationResult();

    var actual = _sut.GetNextCalculationResult(data.PreviousCalculationResult.ToCalculationResult());

    Assert.Equal(expected, actual);
  }

  private void ArrangeCalculationLogicService(IEnumerable<CalculationResult> previousCalculationResults)
  {
    foreach (var previousCalculationResult in previousCalculationResults)
    {
      _sut.GetNextCalculationResult(previousCalculationResult);
    }
  }

  private class GetNextCalculationResultTestTheoryDataForFirstApp : GetNextCalculationResultTestTheoryData
  {
    public GetNextCalculationResultTestTheoryDataForFirstApp()
    {
      AddData(
        [new(0, 0), new(2, 1), new(4, 3)],
        [new(1, 1), new(3, 2), new(5, 5)]);
    }
  }

  private class GetNextCalculationResultTestTheoryDataForSecondApp : GetNextCalculationResultTestTheoryData
  {
    public GetNextCalculationResultTestTheoryDataForSecondApp()
    {
      AddData(
        [new(1, 1), new(3, 2), new(5, 5)],
        [new(2, 1), new(4, 3), new(6, 8)]);
    }
  }

  private class GetNextCalculationResultTestTheoryData : TheoryData<string>
  {
    protected void AddData(CalculationResult[] previousCalculationResults, CalculationResult[] nextCalculationResults)
    {
      for (int i = 0; i < previousCalculationResults.Length; i++)
      {
        GetNextCalculationResultTestData data = new(
          previousCalculationResults[i].ToSerializableCalculationResult(),
          nextCalculationResults[i].ToSerializableCalculationResult(),
          previousCalculationResults[..i].Select(x => x.ToSerializableCalculationResult()).ToArray());

        Add(JsonSerializer.Serialize(data));
      }
    }

    public static GetNextCalculationResultTestData ParseData(string serializedData)
    {
      return JsonSerializer.Deserialize<GetNextCalculationResultTestData>(serializedData)!;
    }
  }

  private record GetNextCalculationResultTestData(
    SerializableCalculationResult PreviousCalculationResult,
    SerializableCalculationResult NextCalculationResult,
    IEnumerable<SerializableCalculationResult> PreviousCalculationResults);
}
