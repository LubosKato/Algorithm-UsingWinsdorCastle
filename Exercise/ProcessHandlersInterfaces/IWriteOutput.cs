using System.Collections.Generic;

namespace Exercise.ProcessHandlersInterfaces
{
    public interface IWriteOutput
    {
        bool WriteOutputFile(string responseFileName, List<string> output);
    }
}