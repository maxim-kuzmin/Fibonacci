namespace Fibonacci.Apps.SecondApp.Calculation;

/// <summary>
/// Конечные точки расчёта.
/// </summary>
public static class CalculationEndpoints
{
  /// <summary>
  /// Сопоставить API расчёта.
  /// </summary>
  /// <param name="group">Построитель группы маршрутов.</param>
  /// <returns>Построитель группы маршрутов.</returns>
  public static RouteGroupBuilder MapCalculationApi(this RouteGroupBuilder group)
  {
    group.MapPost($"/{AppSettings.CalculationApiSendResultPath}", SendResult)
      .WithName(nameof(SendResult))
      .WithOpenApi();

    return group;
  }

  /// <summary>
  /// Отправить результат.
  /// </summary>
  /// <param name="mediator">Посредник.</param>
  /// <param name="command">Команда.</param>
  /// <returns>HTTP 200 OK.</returns>
  public static async Task<Ok> SendResult(IMediator mediator, CalculationSendResultActionCommand command)
  {
    await mediator.Send(command);

    return TypedResults.Ok();
  }
}
