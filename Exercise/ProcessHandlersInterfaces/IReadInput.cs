using System.Collections.Generic;

namespace Exercise.ProcessHandlersInterfaces
{
    public interface IReadInput
    {
        bool ReadInputFile(string inFileName, ref List<WindsorExercise.Field> fields);
    }
}