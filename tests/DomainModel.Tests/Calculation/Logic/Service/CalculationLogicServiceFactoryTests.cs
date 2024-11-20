namespace Fibonacci.DomainModel.Tests.Calculation.Logic.Service;

public class CalculationLogicServiceFactoryTests
{
  private readonly CalculationLogicServiceFactory _calculationLogicServiceFactory = new();

  [Fact]
  public void CreateCalculationLogicService_Always_ReturnsCalculationLogicService()
  {
    var actual = _calculationLogicServiceFactory.CreateCalculationLogicService();

    Assert.IsAssignableFrom<ICalculationLogicService>(actual);
  }
}
