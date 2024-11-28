namespace Fibonacci.Apps.SecondApp.IntegrationTests.Calculation;

public class CalculationEndpointsTests(AppTestWebApplicationFactory<Program> _factory) :
  IClassFixture<AppTestWebApplicationFactory<Program>>
{
  private readonly HttpClient _httpClient = _factory.CreateClient();

  [Fact]
  public async Task SendResult_Always_ReturnsOk()
  {
    Guid calculationId = Guid.NewGuid();

    CalculationResult calculationResult = new(1, 1);

    var command = calculationResult.ToCalculationSendResultActionCommand(calculationId);

    using var requestContent = JsonContent.Create(command);

    var responseMessage = await _httpClient.PostAsync(
      AppSettings.CalculationApiSendResultUrl,
      requestContent,
      default);

    Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
  }
}
