using System.Collections.Generic;
using System.Linq;
using Exercise.ProcessHandlersInterfaces;

namespace Exercise.ProcessHandlers
{
    public class AlgorithmNew : IAlgorithm
    {
        private List<Field> _fields;

        public List<string> ProcessInput(List<Field> fields)
        {
            _fields = fields;
            var result = new List<string>();
            if (_fields != null)
            {
                foreach (var field in _fields)
                {
                    if (field.DependsOn != null && field.DependsOn.Any())
                    {
                        var entryPoint = field.Name;
                        var dependencies = new List<string>();
                        var checkedEntryPoints = new List<string>();
                        Recursion(entryPoint, checkedEntryPoints, dependencies);
                        dependencies.Sort();
                        result.Add(entryPoint + "  " + string.Concat(dependencies.Select(dependency => dependency + " ").ToArray()).TrimEnd(' '));
                    }
                }
            }
            return result;
        }

        private void Recursion(string entryPoint, List<string> entryPointsToCheck, List<string> dependencies)
        {
            entryPointsToCheck.Remove(entryPoint);
            string[] dependenciesTemp = (from field in _fields where entryPoint == field.Name select field.DependsOn).FirstOrDefault();
            if (dependenciesTemp != null && dependenciesTemp.Any())
            {
                foreach (var dependency in dependenciesTemp)
                {
                    if (!dependencies.Contains(dependency))
                    {
                        dependencies.Add(dependency);
                        entryPointsToCheck.Add(dependency);
                    }
                }
                Recursion(dependenciesTemp[0], entryPointsToCheck, dependencies);
            }
            else
            {
                if (entryPointsToCheck.Count == 0)
                    return;
                foreach (var dependecy in entryPointsToCheck.ToList())
                {
                    Recursion(dependecy, entryPointsToCheck, dependencies);
                }
            }
        }
    }
}