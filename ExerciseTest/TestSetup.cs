using System.Collections.Generic;
using System.Linq;
using Exercise;

namespace ExerciseTest
{
    class TestSetup
    {
        public readonly string inputFile_Valid;
        public readonly string outputFile_Valid;

        public readonly string inputFile_InValid;
        public readonly string outputFile_InValid;

        public readonly string inputFile_InValidData;
        public readonly string outputFile_InValidData;

        public readonly string inputFile_Empty;

        public readonly string inputFile_ValidCircular;

        public readonly Dictionary<string, string> processedInput;

        public readonly List<WindsorExercise.Field> field;

        public readonly List<string> outputList; 

        public TestSetup()
        {
            inputFile_Valid = @"..\..\TestData\Data.txt";
            outputFile_Valid = @"..\..\TestData\Output.txt";
            inputFile_InValid = @"..\..\TestData\Data1.txt";
            outputFile_InValid = @"../../TestData// :?\Output1.txt";
            inputFile_InValidData = @"..\..\TestData\DataInvalid.txt";
            outputFile_InValidData = @"..\..\TestData\OutputInvalid.txt";
            inputFile_Empty = @"..\..\TestData\DataEmpty.txt";
            inputFile_ValidCircular = @"..\..\TestData\DataCircular.txt";

            processedInput = new Dictionary<string, string>();
            processedInput.Add("A", "BCEFGH");
            processedInput.Add("B", "CEFGH");
            processedInput.Add("C", "G");
            processedInput.Add("D", "ABCEFGH");
            processedInput.Add("E", "FH");
            processedInput.Add("F", "H");

            field = new List<WindsorExercise.Field>();
            field.Add(new WindsorExercise.Field(){Name = "A", DependsOn = new string[] { "B", "C" }});
            field.Add(new WindsorExercise.Field() {Name = "B", DependsOn = new string[] { "C", "E" }});
            field.Add(new WindsorExercise.Field() { Name = "C", DependsOn = new string[] { "G" }});
            field.Add(new WindsorExercise.Field() { Name = "D", DependsOn = new string[] { "A", "F" }});
            field.Add(new WindsorExercise.Field() { Name = "E", DependsOn = new string[] { "F" } });
            field.Add(new WindsorExercise.Field() { Name = "F", DependsOn = new string[] { "H" } });
            field.Add(new WindsorExercise.Field() { Name = "G", DependsOn = new string[] {}});
            field.Add(new WindsorExercise.Field() { Name = "H", DependsOn = new string[] {}});

            outputList = new List<string>();
            outputList.Add("A BCEFGH");
            outputList.Add("B CEFGH");
            outputList.Add("C G");
            outputList.Add("D ABCEFGH");
            outputList.Add("E FH");
            outputList.Add("F H");
        }

        public string[] SetUpValidCommand()
        {
            return new[] { "-i", inputFile_Valid, "-o", outputFile_Valid };
        }

        public string[] SetUpInValidCommand()
        {
            return new[] { "-b", inputFile_InValid, "o", outputFile_InValid };
        }
    }
}
