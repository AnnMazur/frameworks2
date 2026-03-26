using Microsoft.Extensions.DependencyInjection;
using Pr2.ModulesAndDi.Core;
using Pr2.ModulesAndDi.Modules;
using Pr2.ModulesAndDi.Services;
using System.Collections.Generic;
using System.Linq;
using System;
using Xunit;

// ПОРЯДОК МОДУЛЕЙ
public class OrderTests
{
    [Fact]
    public void Should_Order_Modules_Correctly()
    {
        var modules = new List<IAppModule>
        {
            new HelloModule(),
            new TimeModule(),
            new RandomModule()
        };

        var loader = new ModuleLoader();
        var ordered = loader.SortModules(modules);

        Assert.Equal("Hello", ordered[0].Name);
    }

    [Fact]
    public void Logging_Should_Be_Last()
    {
        var modules = new List<IAppModule>
        {
            new HelloModule(),
            new TimeModule(),
            new RandomModule(),
            new LoggingModule()
        };

        var loader = new ModuleLoader();
        var ordered = loader.SortModules(modules);

        // Logging зависит от всех → должен быть последним
        Assert.Equal("Logging", ordered.Last().Name);
    }
}

//  ОТСУТСТВУЮЩИЙ МОДУЛЬ
public class MissingModuleTests
{
    [Fact]
    public void Should_Throw_If_Dependency_Missing()
    {
        var modules = new List<IAppModule>
        {
            new TimeModule() // требует Hello
        };

        var loader = new ModuleLoader();

        var ex = Assert.Throws<Exception>(() => loader.SortModules(modules));

        Assert.Contains("Missing module", ex.Message);
    }
}

// ЦИКЛ ЗАВИСИМОСТЕЙ
public class CycleTests
{
    class AModule : IAppModule
    {
        public string Name => "A";
        public IEnumerable<string> Dependencies => new[] { "B" };

        public void RegisterServices(IServiceCollection s) { }
        public void Initialize(IServiceProvider p) { }
    }

    class BModule : IAppModule
    {
        public string Name => "B";
        public IEnumerable<string> Dependencies => new[] { "A" };

        public void RegisterServices(IServiceCollection s) { }
        public void Initialize(IServiceProvider p) { }
    }

    [Fact]
    public void Should_Detect_Cycle()
    {
        var modules = new List<IAppModule>
        {
            new AModule(),
            new BModule()
        };

        var loader = new ModuleLoader();

        var ex = Assert.Throws<Exception>(() => loader.SortModules(modules));

        Assert.Contains("Cycle detected", ex.Message);
    }
}

// ПРОВЕРКА DI
public class DiTests
{
    [Fact]
    public void Logger_Should_Be_Singleton()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ILoggerService, LoggerService>();

        var provider = services.BuildServiceProvider();

        var logger1 = provider.GetRequiredService<ILoggerService>();
        var logger2 = provider.GetRequiredService<ILoggerService>();

        Assert.Same(logger1, logger2);
    }

    [Fact]
    public void Clock_Should_Be_Injected()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IClock, SystemClock>();

        var provider = services.BuildServiceProvider();

        var c1 = provider.GetRequiredService<IClock>();
        var c2 = provider.GetRequiredService<IClock>();

        Assert.Same(c1, c2);
    }
}