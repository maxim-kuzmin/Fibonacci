﻿namespace Fibonacci.DomainUseCases.Calculation;

/// <summary>
/// Расширения расчёта.
/// </summary>
public static class CalculationExtensions
{
  /// <summary>
  /// Преобразовать к результату расчёта.
  /// </summary>
  /// <param name="calculationResultDTO">Объект передачи данных результата расчёта.</param>
  /// <returns>Результат расчёта.</returns>
  public static CalculationResult ToCalculationResult(this CalculationResultDTO calculationResultDTO)
  {
    return calculationResultDTO.CalculationResult.ToCalculationResult();
  }

  /// <summary>
  /// Преобразовать к результату расчёта.
  /// </summary>
  /// <param name="calculationSendResultActionCommand">Команда на выполнение действия по отправке результата расчёта.</param>
  /// <returns>Результат расчёта.</returns>
  public static CalculationResult ToCalculationResult(this CalculationSendResultActionCommand calculationSendResultActionCommand)
  {
    return calculationSendResultActionCommand.CalculationResult.ToCalculationResult();
  }

  /// <summary>
  /// Преобразовать к объекту передачи данных результата расчёта.
  /// </summary>
  /// <param name="calculationResult">Результат расчёта.</param>
  /// <param name="calculationId">Идентификатор расчёта.</param>
  /// <returns>Объект передачи данных результата расчёта.</returns>
  public static CalculationResultDTO ToCalculationResultDTO(
    this CalculationResult calculationResult,
    Guid calculationId)
  {
    return new(calculationId, calculationResult.ToSerializableCalculationResult());
  }

  /// <summary>
  /// Преобразовать к команде на выполнение действия по отправке результата расчёта.
  /// </summary>
  /// <param name="calculationResult">Результат расчёта.</param>
  /// <param name="calculationId">Идентификатор расчёта.</param>
  /// <returns>Объект передачи данных результата расчёта.</returns>
  public static CalculationSendResultActionCommand ToCalculationSendResultActionCommand(
    this CalculationResult calculationResult,
    Guid calculationId)
  {
    return new(calculationId, calculationResult.ToSerializableCalculationResult());
  }
}
