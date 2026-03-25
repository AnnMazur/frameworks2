using Microsoft.Extensions.DependencyInjection;

namespace Pr2.ModulesAndDi.Core;

public interface IAppModule
{
    string Name { get; }

    IEnumerable<string> Dependencies { get; }

    void RegisterServices(IServiceCollection services);

    void Initialize(IServiceProvider serviceProvider);
}
