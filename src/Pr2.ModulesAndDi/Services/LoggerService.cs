namespace Pr2.ModulesAndDi.Services;

public class LoggerService : ILoggerService
{ 
private int _count = 0;

public void Log(string message)
{
    Console.WriteLine($"[LOG] {message}");
}

public void Increment()
{
    _count++;
}

public void PrintSummary()
{
    Console.WriteLine($"[LOG] Отработало модулей: {_count}");
}
}
