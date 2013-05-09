using System;
using System.Collections.Generic;
using System.Diagnostics;
using Exercise.ProcessHandlersInterfaces;

namespace Exercise.ProcessHandlers
{
    public class BenchmarkingAlgorithmDecorator : IAlgorithm
    {
        private readonly IAlgorithm _inner;

        private readonly Stopwatch _watch = new Stopwatch();

        public BenchmarkingAlgorithmDecorator(IAlgorithm inner)
        {
            _inner = inner;
        }

        public List<string> ProcessInput(List<Field> fields)
        {
            _watch.Start();
            List<string> result = _inner.ProcessInput(fields);
            _watch.Stop();

            Console.WriteLine("Process via {0} took {1} ticks to complete.", _inner.GetType(), _watch.ElapsedTicks);

            _watch.Reset();
            return result;
        }


        public List<string> ProcessInput()
        {
            throw new NotImplementedException();
        }
    }
}