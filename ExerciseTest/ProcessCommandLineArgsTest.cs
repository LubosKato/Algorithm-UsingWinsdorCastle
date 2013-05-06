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
        public void Test_commandLine_Valid()
        {
            var cmd = _setupTool.SetUpValidCommand();
            ProcessCommandLineArgs args = new ProcessCommandLineArgs(new DependencySorter(new Algorithm(), new ReadInput(), new WriteOutput()));
            bool result = args.ProcessArgs(cmd);
            Assert.IsTrue(result);
        }

        [Test(Description = "Testing invalid entries to command line")]
        public void Test_commandLine_InValid()
        {
            var cmd = _setupTool.SetUpInValidCommand();
            ProcessCommandLineArgs args = new ProcessCommandLineArgs(new DependencySorter(new Algorithm(), new ReadInput(), new WriteOutput()));
            bool result = args.ProcessArgs(cmd);
            Assert.IsFalse(result);
        }
    }
}