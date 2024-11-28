namespace Fibonacci.Infrastructure.UnitTests.Calculation.Result.Publishers.Next;

public class CalculationHttpClientNextResultPublisherTests
{
  private readonly CalculationHttpClientNextResultPublisher _sut;

  private readonly Mock<IHttpClientFactory> _httpClientFactoryMock = new();

  private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock = new();

  public CalculationHttpClientNextResultPublisherTests()
  {
    _sut = new(_httpClientFactoryMock.Object);

    _httpMessageHandlerMock.Protected()
        .Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>())
        .ReturnsAsync(new HttpResponseMessage());

    var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
    {
      BaseAddress = new Uri("https://localhost")
    };

    _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
      .Returns(httpClient);
  }

  [Fact]
  public async Task PublishCalculationResult_Always_CallsOnceHttpClientFactoryCreateClient()
  {
    var calculationId = Guid.NewGuid();

    CalculationResult calculationResult = new(0, 0);

    await _sut.PublishCalculationResult(calculationId, calculationResult, default);

    _httpClientFactoryMock.Verify(
      x => x.CreateClient(AppSettings.CalculationNextResultPublisherHttpClientName),
      Times.Once);
  }

  [Fact]
  public async Task PublishCalculationResult_Always_CallsOnceHttpMessageHandlerSendAsync()
  {
    var calculationId = Guid.NewGuid();

    CalculationResult calculationResult = new(0, 0);

    await _sut.PublishCalculationResult(calculationId, calculationResult, default);

    _httpMessageHandlerMock.Protected().Verify(
      "SendAsync",
      Times.Once(),
      ItExpr.IsAny<HttpRequestMessage>(),
      ItExpr.IsAny<CancellationToken>());
  }
}
