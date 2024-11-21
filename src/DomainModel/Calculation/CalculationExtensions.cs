namespace Fibonacci.DomainModel.Calculation;

/// <summary>
/// Расширения расчёта.
/// </summary>
public static class CalculationExtensions
{
  /// <summary>
  /// Преобразовать к результату расчёта.
  /// </summary>
  /// <param name="serializableCalculationResult">Сериализуемый результат расчёта.</param>
  /// <returns>Результат расчёта.</returns>
  public static CalculationResult ToCalculationResult(this SerializableCalculationResult serializableCalculationResult)
  {
    return new CalculationResult(
      BigInteger.Parse(serializableCalculationResult.Input),
      BigInteger.Parse(serializableCalculationResult.Output));
  }

  /// <summary>
  /// Преобразовать к сериализуемому результату расчёта.
  /// </summary>
  /// <param name="calculationResult">Результат расчёта.</param>
  /// <returns>Сериализуемый результат расчёта.</returns>
  public static SerializableCalculationResult ToSerializableCalculationResult(this CalculationResult calculationResult)
  {
    return new SerializableCalculationResult(
      calculationResult.Input.ToString(),
      calculationResult.Output.ToString());
  }
}
