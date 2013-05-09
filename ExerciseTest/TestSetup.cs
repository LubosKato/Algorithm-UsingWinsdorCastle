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

        public readonly List<Field> Field;

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

            Field = new List<Field>
                        {
                            new Field {Name = "A", DependsOn = new[] {"B", "C"}},
                            new Field {Name = "B", DependsOn = new[] {"C", "E"}},
                            new Field {Name = "C", DependsOn = new[] {"G"}},
                            new Field {Name = "D", DependsOn = new[] {"A", "F"}},
                            new Field {Name = "E", DependsOn = new[] {"F"}},
                            new Field {Name = "F", DependsOn = new[] {"H"}},
                            new Field {Name = "G", DependsOn = new string[] {}},
                            new Field {Name = "H", DependsOn = new string[] {}}
                        };

            OutputList = new List<string> {"A  B C E F G H", "B  C E F G H", "C  G", "D  A B C E F G H", "E  F H", "F  H"};
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
