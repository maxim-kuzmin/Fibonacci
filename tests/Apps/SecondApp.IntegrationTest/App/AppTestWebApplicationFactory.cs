namespace Fibonacci.Apps.SecondApp.IntegrationTest.App;

public class AppTestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services =>
    {
      // Здесь можно переопределить зависимости, которые устанавливаются в файле Program.cs.
    });
  }
}
