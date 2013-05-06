using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Exercise.ProcessHandlersInterfaces;

namespace Exercise.ProcessHandlers
{
    public class ReadInput : IReadInput
    {
        public bool ReadInputFile(string inFileName, ref List<WindsorExercise.Field> fields)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}