using System.Collections.Generic;
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
        public void TestAlgorithm()
        {
            IAlgorithm algorithm = new Algorithm();
            List<string> result = algorithm.ProcessInput(_setupTool.Field);
            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(result[0], _setupTool.OutputList[0]);
            Assert.AreEqual(result[1], _setupTool.OutputList[1]);
        }

        [Test(Description = "Testing empty input algorithm")]
        public void TestAlgorithmEmpty()
        {
            IAlgorithm algorithm = new Algorithm();
            List<string> result = algorithm.ProcessInput(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}