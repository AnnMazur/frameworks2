using Microsoft.Extensions.DependencyInjection;
using Pr2.ModulesAndDi.Core;
using System.Collections.Generic;
using System;

namespace Pr2.ModulesAndDi.Modules;

public class RandomModule : IAppModule
{
	public string Name => "Random";
	public IEnumerable<string> Dependencies => ["Hello"];

	public void RegisterServices(IServiceCollection services)
	{
		services.AddSingleton<Random>();
	}

	public void Initialize(IServiceProvider provider)
	{
		var rnd = provider.GetRequiredService<Random>();
		Console.WriteLine($"Random: {rnd.Next(1, 100)}");
	}
}