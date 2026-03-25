using System.Collections.Generic;
using System;
using Pr2.ModulesAndDi.Core;

public class ModuleLoader
{
    public List<IAppModule> SortModules(List<IAppModule> modules)
    {
        var result = new List<IAppModule>();
        var visited = new HashSet<string>();
        var visiting = new HashSet<string>();

        void Visit(IAppModule module)
        {
            if (visited.Contains(module.Name)) return;
            if (visiting.Contains(module.Name))
                throw new Exception($"Cycle detected: {module.Name}");

            visiting.Add(module.Name);

            foreach (var dep in module.Dependencies)
            {
                var depModule = modules.FirstOrDefault(m => m.Name == dep)
                    ?? throw new Exception($"Missing module: {dep}");

                Visit(depModule);
            }

            visiting.Remove(module.Name);
            visited.Add(module.Name);
            result.Add(module);
        }

        foreach (var module in modules)
            Visit(module);

        return result;
    }
}