using System;
using System.Collections.Generic;
using System.IO;
using Exercise.Helper;
using Exercise.ProcessHandlersInterfaces;

namespace Exercise.ProcessHandlers
{
    public class WriteOutput : IWriteOutput
    {
        public bool WriteOutputFile(string responseFileName, List<string> output)
        {
            try
            {
                responseFileName.CleanFileName();
                if (File.Exists(responseFileName))
                {
                    File.Delete(responseFileName);
                }
                using (var writer = new StreamWriter(responseFileName, true))
                {
                    foreach (var line in output)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            catch
            {
                throw;
            }
            return true;
        }
    }
}