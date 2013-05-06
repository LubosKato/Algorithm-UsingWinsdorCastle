using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace Exercise
{
    public class WindsorExercise
    {
        public class Field
        {
            public string Name { get; set; }
            public string[] DependsOn { get; set; }
        }

        static void Main(string[] args)
        {
            IWindsorContainer container = new WindsorContainer(new XmlInterpreter());
            ProcessCommandLineArgs process = container.Resolve<ProcessCommandLineArgs>();
            process.ProcessArgs(args);
            container.Release(process);
        }
    }
}
