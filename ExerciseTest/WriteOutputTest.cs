using System;
using Exercise.ProcessHandlers;
using Exercise.ProcessHandlersInterfaces;
using NUnit.Framework;

namespace ExerciseTest
{
    public class WriteOutputTest
    {
        private readonly TestSetup _setupTool;

        public WriteOutputTest()
        {
            _setupTool = new TestSetup();
        }

        [Test(Description = "Testing valid output file path")]
        public void TestOutputFileValid()
        {
            IWriteOutput output = new WriteOutput();
            bool passed = output.WriteOutputFile(_setupTool.OutputFileValid, _setupTool.OutputList);
            Assert.IsTrue(passed);
        }

        [Test(Description = "Testing write ivalid format output file")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestOutputFileInvalid()
        {
            IWriteOutput output = new WriteOutput();
            output.WriteOutputFile(_setupTool.OutputFileInValid, _setupTool.OutputList);
        }
    }
}