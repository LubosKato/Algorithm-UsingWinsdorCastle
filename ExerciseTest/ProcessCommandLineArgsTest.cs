using Exercise;
using Exercise.ProcessHandlers;
using NUnit.Framework;

namespace ExerciseTest
{
    [TestFixture]
    public class ProcessCommandLineArgsTest
    {
        private readonly TestSetup _setupTool;

        public ProcessCommandLineArgsTest()
        {
            _setupTool = new TestSetup();
        }

        [Test(Description = "Testing valid entries to command line")]
        public void TestCommandLineValid()
        {
            var cmd = _setupTool.SetUpValidCommand();
            var args = new ProcessCommandLineArgs(new DependencySorter(new ReadInput(), new Algorithm(), new WriteOutput()));
            bool result = args.ProcessArgs(cmd);
            Assert.IsTrue(result);
        }

        [Test(Description = "Testing invalid entries to command line")]
        public void TestCommandLineInValid()
        {
            var cmd = _setupTool.SetUpInValidCommand();
            var args = new ProcessCommandLineArgs(new DependencySorter(new ReadInput(), new Algorithm(), new WriteOutput()));
            bool result = args.ProcessArgs(cmd);
            Assert.IsFalse(result);
        }
    }
}