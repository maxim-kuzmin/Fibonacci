namespace Fibonacci.Apps.FirstApp.Calculation;

/// <summary>
/// Интерфейс сервиса расчёта.
/// </summary>
public interface ICalculationService
{
  /// <summary>
  /// Рассчитать.
  /// </summary>
  /// <param name="cancellationToken">Токен отмены.</param>
  /// <returns></returns>
  Task Calculate(CancellationToken cancellationToken);
}
