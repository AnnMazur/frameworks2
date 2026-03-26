using Microsoft.Extensions.DependencyInjection;
using Pr2.ModulesAndDi.Core;
using Pr2.ModulesAndDi.Services;

namespace Pr2.ModulesAndDi.Modules;

public class LoggingModule : IAppModule
{
    public string Name => "Logging";
    public IEnumerable<string> Dependencies => ["Hello", "Time", "Random"];

    public void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<ILoggerService, LoggerService>();
    }

    public void Initialize(IServiceProvider provider)
    {
        var logger = provider.GetRequiredService<ILoggerService>();
        logger.PrintSummary();
    }
}