using System.Collections.Generic;
using Exercise;
using Exercise.ProcessHandlers;
using Exercise.ProcessHandlersInterfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace ExerciseTest
{
    [TestFixture]
    public class DependencySorterTest
    {
        private readonly TestSetup _setupTool;
        private readonly MockRepository _mocks; 

        public DependencySorterTest()
        {
            _setupTool = new TestSetup();
            _mocks = new MockRepository();
        }

        [Test(Description = "Testing Dependency Sorter logic")]
        public void Test_Main_Logic()
        {
            IAlgorithm algorithm = _mocks.StrictMock<IAlgorithm>();
            IReadInput input = _mocks.StrictMock<IReadInput>();
            IWriteOutput output = _mocks.StrictMock<IWriteOutput>();
            var field = new List<WindsorExercise.Field>();

            using (_mocks.Record())
            {
                Expect.Call(algorithm.ProcessInput(_setupTool.field)).Return(_setupTool.processedInput);
                Expect.Call(input.ReadInputFile(_setupTool.inputFile_Valid, ref field)).Return(true).OutRef(_setupTool.field);
                Expect.Call(output.WriteOutputFile(_setupTool.outputFile_Valid, _setupTool.outputList)).Return(true).IgnoreArguments();
            }

            using (_mocks.Playback())
            {
                IDependencySorter sorter = new DependencySorter(algorithm, input, output);
                bool expectedResult = sorter.MainDriver(_setupTool.inputFile_Valid, _setupTool.outputFile_Valid);
                Assert.AreEqual(expectedResult, true);
            }
        } 
    }
}