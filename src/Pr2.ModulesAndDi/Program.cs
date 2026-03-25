using Microsoft.Extensions.DependencyInjection;
using Pr2.ModulesAndDi.Core;
using Pr2.ModulesAndDi.Modules;
using Pr2.ModulesAndDi.Services;

var services = new ServiceCollection();

var modules = new List<IAppModule>
{
    new HelloModule(),
    new LoggingModule(),
    new TimeModule(),
    new RandomModule()
};

var loader = new ModuleLoader();
var ordered = loader.SortModules(modules);

foreach (var module in ordered)
{
    module.RegisterServices(services);
}

var provider = services.BuildServiceProvider();
var logger = provider.GetService<ILoggerService>();

foreach (var module in ordered)
{
    logger?.Log($"Модуль {module.Name} начал свою работу");

    module.Initialize(provider);

    logger.Increment();

    logger?.Log($"Модуль {module.Name} завершил свою работу");
}
