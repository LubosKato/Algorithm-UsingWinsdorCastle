using System.Collections.Generic;
using Exercise;
using Exercise.ProcessHandlers;
using Exercise.ProcessHandlersInterfaces;
using NUnit.Framework;

namespace ExerciseTest
{
    [TestFixture]
    public class AlgorithmTest
    {
        private readonly TestSetup _setupTool;

        public AlgorithmTest()
        {
            _setupTool = new TestSetup();
        }

        [Test(Description = "Testing algorithm")]
        public void Test_Algorithm()
        {
            var field = new List<WindsorExercise.Field>();
            IAlgorithm algorithm = new Algorithm();
            Dictionary<string, string> result = algorithm.ProcessInput(_setupTool.field);
            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(result["A"], _setupTool.processedInput["A"]);
            Assert.AreEqual(result["D"], _setupTool.processedInput["D"]);
        }
    }
}