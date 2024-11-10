namespace Fibonacci.DomainUseCases.Calculation.Actions.SendResult;

/// <summary>
/// Интерфейс обработчика действия по отправке результата расчёта.
/// </summary>
public interface ICalculationSendResultActionHandler : IRequestHandler<CalculationSendResultActionCommand>;
