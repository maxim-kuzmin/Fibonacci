namespace Fibonacci.Apps.SecondApp.App.Middlewares;

public class AppTracingMiddleware(RequestDelegate next)
{
  private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));

  [DebuggerStepThrough]
  public async Task Invoke(HttpContext context)
  {
    string traceId = Activity.Current!.TraceId.ToString();

    const string key = "TraceId";

    using (LogContext.PushProperty(key, traceId))
    {
      context.Response.Headers.Append(key, traceId);

      await _next(context);
    }
  }
}
