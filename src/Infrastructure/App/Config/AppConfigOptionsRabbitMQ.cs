namespace Fibonacci.Infrastructure.App.Config;

/// <summary>
/// Параметры конфигурации приложения для RabbitMQ.
/// </summary>
/// <param name="Hostname">Имя хоста.</param>
/// <param name="Port">Порт.</param>
/// <param name="Username">Имя пользователя.</param>
/// <param name="Password">Пароль.</param>
public record AppConfigOptionsRabbitMQ(string Hostname, int Port, string Username, string Password);
