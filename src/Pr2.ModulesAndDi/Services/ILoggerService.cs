namespace Pr2.ModulesAndDi.Services;

public interface ILoggerService
{
    void Log(string message);

    void Increment();

    void PrintSummary();
}