using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Exercise.ProcessHandlersInterfaces;

namespace Exercise.ProcessHandlers
{
    public class DependencySorter : IDependencySorter
    {
        private readonly IAlgorithm _algorithm;
        private readonly IReadInput _read;
        private readonly IWriteOutput _write;

        public DependencySorter(IAlgorithm algorithm, IReadInput read, IWriteOutput write)
        {
            _algorithm = algorithm;
            _read = read;
            _write = write;
        }

        public bool MainDriver(string inFileName, string responseFileName)
        {
            try
            {
                var input = new List<WindsorExercise.Field>();
                _read.ReadInputFile(inFileName, ref input);
                var output = _algorithm.ProcessInput(input).Select(line => line.Key + "  " + line.Value).ToList();
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