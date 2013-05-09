using System.Collections.Generic;
using System.Linq;
using Exercise.ProcessHandlersInterfaces;

namespace Exercise.ProcessHandlers
{
    public class Algorithm : IAlgorithm
    {
        public List<string> ProcessInput(List<WindsorExercise.Field> fields)
        {
            var result = new Dictionary<string, string>();
            if (fields != null)
            {
                foreach (var field in fields)
                {
                    if (field.DependsOn != null && field.DependsOn.Any())
                    {
                        var entryPoint = field.Name;
                        var dependencies = new List<string>();
                        var entryPoints = new List<string>();
                        Recursion(entryPoint, entryPoints, fields, dependencies);
                        dependencies.Sort();
                        result.Add(entryPoint, string.Concat(dependencies.ToArray()));
                    }
                }
            }

            return result.Select(line => line.Key + "  " + line.Value).ToList();
        }

        private void Recursion(string entryPoint, List<string> entryPoints, List<WindsorExercise.Field> fields, List<string> dependencies)
        {
            if (entryPoints.Contains(entryPoint))
            {
                return;
            }
            entryPoints.Add(entryPoint);
            string[] temp = (from field in fields where entryPoint == field.Name select field.DependsOn).FirstOrDefault();
            if (temp != null && temp.Any())
            {
                foreach (var dependency in temp)
                {
                    entryPoint = temp[0];
                    if (!dependencies.Contains(dependency))
                    {
                        dependencies.Add(dependency);
                    }
                }
                Recursion(entryPoint, entryPoints, fields, dependencies);
            }
            else
            {
                foreach (var dependecy in dependencies.ToList())
                {
                    if (!entryPoints.Contains(dependecy))
                        Recursion(dependecy, entryPoints, fields, dependencies);
                }
            }
        } 
    }
}