using System.Collections.Generic;

namespace Exercise.ProcessHandlersInterfaces
{
    public interface IAlgorithm
    {
        Dictionary<string, string> ProcessInput(List<WindsorExercise.Field> fields);
    }
}