using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Exercise.Helper;

[assembly: InternalsVisibleTo("ExerciseTest")]
namespace Exercise
{
    public class AlgorithmExercise
    {


        static void Main(string[] args)
        {
            var processor = new AlgorithmExercise();
            string inFileName = string.Empty;
            string responseFileName = string.Empty;

            args.GetCommandLineSwitch(Constants.INFILE_SWITCH, ref inFileName);
            args.GetCommandLineSwitch(Constants.OUTFILE_SWITCH, ref responseFileName);

            if (inFileName.Length > 0 && responseFileName.Length > 0)
            {
                processor.MainDriver(inFileName, responseFileName);
            }
            else
            {
                CommandLineHelper.ShowUsage();
            }
        }

        protected internal bool MainDriver(string inFileName, string responseFileName)
        {
            try
            {
                var input = new List<WindsorExercise.Field>();
                ReadInputFile(inFileName, input);
                var output = ProcessInput(input).Select(line => line.Key + "  " + line.Value).ToList();
                if (output.Count == 0) return false;
                WriteOutputFile(responseFileName, output);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Wrong input format : " + e.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong : " + e.Message);
                return false;
            }
            return true;
        }

        private Dictionary<string, string> ProcessInput(List<WindsorExercise.Field> fields)
        {
            var result = new Dictionary<string, string>();
            foreach (var field in fields)
            {
                if (field.DependsOn.Any())
                {
                    var entryPoint = field.Name;
                    var dependencies = new List<string>();
                    var entryPoints = new List<string>();
                    Recursion(entryPoint, entryPoints, fields, dependencies);
                    dependencies.Sort();
                    result.Add(entryPoint, string.Concat(dependencies.ToArray()));
                }
            }
            return result;
        }

        private void Recursion(string entryPoint, List<string> entryPoints, List<WindsorExercise.Field> fields, List<string> dependencies)
        {
            if (entryPoints.Contains(entryPoint))
            {
                return;
            }
            entryPoints.Add(entryPoint);
            string[] temp = (from field in fields where entryPoint == field.Name select field.DependsOn).FirstOrDefault();
            if (temp != null && temp.Any())
            {
                foreach (var dependency in temp)
                {
                    entryPoint = temp[0];
                    if (!dependencies.Contains(dependency))
                    {
                        dependencies.Add(dependency);
                    }
                }
                Recursion(entryPoint, entryPoints, fields, dependencies);
            }
            else
            {
                foreach (var dependecy in dependencies.ToList())
                {
                    if (!entryPoints.Contains(dependecy))
                        Recursion(dependecy, entryPoints, fields, dependencies);
                }
            }
        }

        protected internal void ReadInputFile(string inFileName, List<WindsorExercise.Field> fields)
        {
            var tempFields = new List<string>();
            using (var sr = new StreamReader(inFileName))
            {
                while (sr.Peek() >= 0)
                {
                    var readLine = sr.ReadLine();
                    if (readLine != null)
                    {
                        string newLine = readLine.Replace("\u0009", "").Replace(" ", "");
                        if (!newLine.All(c => Char.IsLetter(c) || c == ' '))
                            throw new FormatException();
                        if (newLine != string.Empty)
                        {
                            //populate name
                            var field = new WindsorExercise.Field { Name = newLine.Substring(0, 1) };
                            tempFields.Add(field.Name.ToUpper());
                            var dependencies = newLine.Substring(1).Select(c => c.ToString(new System.Globalization.CultureInfo("en-GB")).ToUpper()).ToArray();
                            //populate depends on
                            field.DependsOn = dependencies;
                            fields.Add(field);
                        }
                    }
                }
            }

            //iterate and populate object with missing field names
            for (int i = 0; i < fields.Count; i++)
            {
                if (fields[i].DependsOn != null)
                {
                    for (int j = 0; j < fields[i].DependsOn.Length; j++)
                    {
                        var name = fields[i].DependsOn[j];
                        if (fields[i].Name == name)
                        {
                            throw new FormatException(" Warning circular dependency !");
                        }
                        if (!tempFields.Contains(name))
                        {
                            var field = new WindsorExercise.Field { Name = name, DependsOn = new string[0] };
                            tempFields.Add(name);
                            fields.Add(field);
                        }
                    }
                }
            }
        }

        private void WriteOutputFile(string responseFileName, IEnumerable<string> output)
        {
            if (File.Exists(responseFileName))
            {
                File.Delete(responseFileName);
            }
            using (var writer = new StreamWriter(responseFileName, true))
            {
                foreach (var line in output)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}
