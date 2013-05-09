using System;
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

        [Test(Description = "Testing Dependency Sorter logic positiv")]
        public void TestMainLogicPositive()
        {
            IAlgorithm algorithm = _mocks.StrictMock<IAlgorithm>();
            IReadInput input = _mocks.StrictMock<IReadInput>();
            IWriteOutput output = _mocks.StrictMock<IWriteOutput>();
            var field = new List<Field>();

            using (_mocks.Record())
            {
                Expect.Call(algorithm.ProcessInput(_setupTool.Field)).Return(_setupTool.OutputList);
                Expect.Call(input.ReadInputFile(_setupTool.InputFileValid, ref field)).Return(true).OutRef(_setupTool.Field);
                Expect.Call(output.WriteOutputFile(_setupTool.OutputFileValid, _setupTool.OutputList)).Return(true);
            }

            using (_mocks.Playback())
            {
                IDependencySorter sorter = new DependencySorter(input, algorithm, output);
                Assert.IsTrue(sorter.MainDriver(_setupTool.InputFileValid, _setupTool.OutputFileValid));
            }
        }

        [Test(Description = "Testing Dependency Sorter logic negativ")]
        public void TestMainLogicNegative()
        {
            IAlgorithm algorithm = _mocks.StrictMock<IAlgorithm>();
            IReadInput input = _mocks.StrictMock<IReadInput>();
            IWriteOutput output = _mocks.StrictMock<IWriteOutput>();
            var field = new List<Field>();

            input.Stub(x => x.ReadInputFile(_setupTool.InputFileValid, ref field)).Throw(new FormatException());
            algorithm.Stub(x => x.ProcessInput(_setupTool.Field)).Return(_setupTool.OutputList);
            output.Stub(x => x.WriteOutputFile(_setupTool.OutputFileValid, _setupTool.OutputList)).Return(true);

            IDependencySorter sorter = new DependencySorter(input, algorithm, output);
            Assert.IsFalse(sorter.MainDriver(_setupTool.InputFileValid, _setupTool.OutputFileValid));
        }
    }
}