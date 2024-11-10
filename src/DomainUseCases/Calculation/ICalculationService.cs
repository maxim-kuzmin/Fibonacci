namespace Fibonacci.DomainUseCases.Calculation;

/// <summary>
/// Интерфейс сервиса расчёта.
/// </summary>
public interface ICalculationService
{
  /// <summary>
  /// Получить результат следующего расчёта.
  /// </summary>
  /// <param name="previousCalculationResultDTO">Объект передачи данных результата предыдущего расчёта.</param>
  /// <returns>Объект передачи данных результата следующего расчёта.</returns>
  CalculationResultDTO GetNextCalculationResult(CalculationResultDTO previousCalculationResultDTO);
}
