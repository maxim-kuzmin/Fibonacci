namespace Fibonacci.DomainUseCases.Calculation.Actions.SendResult;

/// <summary>
/// Команда на выполнение действия по отправке результата расчёта.
/// </summary>
/// <param name="CalculationId">Идентификатор расчёта.</param>
/// <param name="CalculationResult">Результат расчёта.</param>
public record CalculationSendResultActionCommand(Guid CalculationId, SerializableCalculationResult CalculationResult) :
  IRequest;
