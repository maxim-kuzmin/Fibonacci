namespace Fibonacci.Apps.SecondApp.IntegrationTest.App;

public class AppTestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
  protected override IHost CreateHost(IHostBuilder builder)
  {
    builder.ConfigureHostConfiguration(config =>
    {
      config.AddInMemoryCollection(new Dictionary<string, string?> { { "EmailAddress", "test1@Contoso.com" } });
    });

    return base.CreateHost(builder);
  }
}
