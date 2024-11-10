namespace Fibonacci.DomainUseCases.Calculation.Actions.SendResult;

/// <summary>
/// Команда на выполнение действия по отправке результата расчёта.
/// </summary>
/// <param name="CalculationId">Идентификатор расчёта.</param>
/// <param name="Input">Входные данные.</param>
/// <param name="Output">Выходные данные.</param>
public record CalculationSendResultActionCommand(Guid CalculationId, string Input, string Output) :
  CalculationResultDTO(CalculationId, Input, Output),
  IRequest;
