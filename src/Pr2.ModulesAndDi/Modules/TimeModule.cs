using Microsoft.Extensions.DependencyInjection;
using Pr2.ModulesAndDi.Core;
using Pr2.ModulesAndDi.Services;
using System.Collections.Generic;
using System;

namespace Pr2.ModulesAndDi.Modules;

public class TimeModule : IAppModule
{
    public string Name => "Time";
    public IEnumerable<string> Dependencies => ["Hello"];

    public void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IClock, SystemClock>();
    }

    public void Initialize(IServiceProvider provider)
    {
        var clock = provider.GetRequiredService<IClock>();
        Console.WriteLine($"Time: {clock.Now}");
    }
}