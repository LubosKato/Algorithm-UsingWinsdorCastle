using System.Collections.Generic;
using Exercise;

namespace ExerciseTest
{
    class TestSetup
    {
        public readonly string InputFileValid;
        public readonly string OutputFileValid;

        public readonly string InputFileInValid;
        public readonly string OutputFileInValid;

        public readonly string InputFileInValidData;
        public readonly string OutputFileInValidData;

        public readonly string InputFileEmpty;

        public readonly string InputFileValidCircular;

        public readonly List<WindsorExercise.Field> Field;

        public readonly List<string> OutputList; 

        public TestSetup()
        {
            InputFileValid = @"..\..\TestData\Data.txt";
            OutputFileValid = @"..\..\TestData\Output.txt";
            InputFileInValid = @"..\..\TestData\Data1.txt";
            OutputFileInValid = @"../../TestData// :?\Output1.txt";
            InputFileInValidData = @"..\..\TestData\DataInvalid.txt";
            OutputFileInValidData = @"..\..\TestData\OutputInvalid.txt";
            InputFileEmpty = @"..\..\TestData\DataEmpty.txt";
            InputFileValidCircular = @"..\..\TestData\DataCircular.txt";

            Field = new List<WindsorExercise.Field>
                        {
                            new WindsorExercise.Field {Name = "A", DependsOn = new[] {"B", "C"}},
                            new WindsorExercise.Field {Name = "B", DependsOn = new[] {"C", "E"}},
                            new WindsorExercise.Field {Name = "C", DependsOn = new[] {"G"}},
                            new WindsorExercise.Field {Name = "D", DependsOn = new[] {"A", "F"}},
                            new WindsorExercise.Field {Name = "E", DependsOn = new[] {"F"}},
                            new WindsorExercise.Field {Name = "F", DependsOn = new[] {"H"}},
                            new WindsorExercise.Field {Name = "G", DependsOn = new string[] {}},
                            new WindsorExercise.Field {Name = "H", DependsOn = new string[] {}}
                        };

            OutputList = new List<string> {"A  BCEFGH", "B  CEFGH", "C  G", "D  ABCEFGH", "E  FH", "F  H"};
        }

        public string[] SetUpValidCommand()
        {
            return new[] { "-i", InputFileValid, "-o", OutputFileValid };
        }

        public string[] SetUpInValidCommand()
        {
            return new[] { "-b", InputFileInValid, "o", OutputFileInValid };
        }
    }
}
