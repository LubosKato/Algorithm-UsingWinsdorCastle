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
        public void Test_EmptyFile()
        {
            var field = new List<WindsorExercise.Field>();
            IReadInput readInput = new ReadInput();
            bool passed = readInput.ReadInputFile(_setupTool.inputFile_Empty, ref field);
            Assert.IsTrue(passed);
            Assert.IsNotNull(field);
            Assert.AreEqual(0, field.Count);
        }

        [Test(Description = "Testing invalid data")]
        [ExpectedException(typeof(FormatException))]
        public void Test_with_Invalid_Data()
        {
            var field = new List<WindsorExercise.Field>();
            IReadInput readInput = new ReadInput();
            readInput.ReadInputFile(_setupTool.inputFile_InValidData, ref field);
        }

        [Test(Description = "Testing valid data")]
        public void Test_with_Valid_Data()
        {
            var field = new List<WindsorExercise.Field>();
            IReadInput readInput = new ReadInput();
            bool passed = readInput.ReadInputFile(_setupTool.inputFile_Valid, ref field);
            Assert.IsTrue(passed);
            Assert.IsNotNull(field);
            Assert.AreEqual(8, field.Count);
        }
    }
}