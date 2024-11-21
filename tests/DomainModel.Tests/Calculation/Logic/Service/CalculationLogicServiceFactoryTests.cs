namespace Fibonacci.DomainModel.Tests.Calculation.Logic.Service;

public class CalculationLogicServiceFactoryTests
{
  private readonly CalculationLogicServiceFactory _sut = new();

  [Fact]
  public void CreateCalculationLogicService_Always_ReturnsCalculationLogicService()
  {
    var actual = _sut.CreateCalculationLogicService();

    Assert.IsAssignableFrom<ICalculationLogicService>(actual);
  }
}
