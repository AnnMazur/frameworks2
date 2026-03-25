using Microsoft.Extensions.DependencyInjection;
using Pr2.ModulesAndDi.Core;
using System.Collections.Generic;
using System;

namespace Pr2.ModulesAndDi.Modules;

public class HelloModule : IAppModule
{
    public string Name => "Hello";
    public IEnumerable<string> Dependencies => [];

    public void RegisterServices(IServiceCollection services) { }

    public void Initialize(IServiceProvider provider)
    {
        Console.WriteLine("Hello from HelloModule");
    }
}