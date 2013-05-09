using System;
using System.Collections.Generic;
using System.IO;
using Exercise.ProcessHandlersInterfaces;

namespace Exercise.ProcessHandlers
{
    public class DependencySorter : IDependencySorter
    {
        private readonly IReadInput _read;
        private readonly IAlgorithm _algorithm;
        private readonly IWriteOutput _write;

        public DependencySorter(IReadInput read, IAlgorithm algorithm, IWriteOutput write)
        {
            _read = read;
            _algorithm = algorithm;
            _write = write;
        }

        public bool MainDriver(string inFileName, string responseFileName)
        {
            try
            {
                var input = new List<WindsorExercise.Field>();
                _read.ReadInputFile(inFileName, ref input);
                var output = _algorithm.ProcessInput(input);
                if (output.Count == 0) return false;
                _write.WriteOutputFile(responseFileName, output);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Wrong input format : " + e.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong : " + e.Message);
                return false;
            }
            return true;
        } 
    }
}