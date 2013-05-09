using System;
using System.Collections.Generic;
using Exercise;
using Exercise.ProcessHandlers;
using Exercise.ProcessHandlersInterfaces;
using NUnit.Framework;

namespace ExerciseTest
{
    [TestFixture]
    public class ReadInputTest
    {
        private readonly TestSetup _setupTool;

        public ReadInputTest()
        {
            _setupTool = new TestSetup();
        }

        [Test(Description = "Testing input with empty file")]
        public void TestEmptyFile()
        {
            var field = new List<Field>();
            IReadInput readInput = new ReadInput();
            bool passed = readInput.ReadInputFile(_setupTool.InputFileEmpty, ref field);
            Assert.IsTrue(passed);
            Assert.IsNotNull(field);
            Assert.AreEqual(0, field.Count);
        }

        [Test(Description = "Testing invalid data")]
        [ExpectedException(typeof(FormatException))]
        public void TestWithInvalidData()
        {
            var field = new List<Field>();
            IReadInput readInput = new ReadInput();
            readInput.ReadInputFile(_setupTool.InputFileInValidData, ref field);
        }

        [Test(Description = "Testing valid data")]
        public void TestWithValidData()
        {
            var field = new List<Field>();
            IReadInput readInput = new ReadInput();
            bool passed = readInput.ReadInputFile(_setupTool.InputFileValid, ref field);
            Assert.IsTrue(passed);
            Assert.IsNotNull(field);
            Assert.AreEqual(8, field.Count);
        }
    }
}