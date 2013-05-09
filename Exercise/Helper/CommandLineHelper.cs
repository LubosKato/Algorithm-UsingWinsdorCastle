using System;
using System.IO;
using System.Linq;

namespace Exercise.Helper
{
    internal static class CommandLineHelper 
    {
        /// <summary>
        /// Check if command line switch is present
        /// </summary>
        /// <param name="args">The args array</param>
        /// <param name="switchName">Name of the switch.</param>
        /// <returns>bool</returns>
        public static bool GetCommandLineSwitch(this string[] args, string switchName)
        {
            return args.Any(t => t.ToUpperInvariant() == switchName.ToUpperInvariant());
        }

        /// <summary>
        /// Get value of command line switch
        /// </summary>
        /// <param name="args">The args array.</param>
        /// <param name="switchName">Name of the switch.</param>
        /// <param name="switchValue">The switch value.</param>
        public static void GetCommandLineSwitch(this string[] args, string switchName, ref string switchValue)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToUpperInvariant() == switchName.ToUpperInvariant())
                {
                    switchValue = GetArg(args, i, switchName, switchValue);
                }
            }
        }

        /// <summary>
        /// Return the arg value for an arg tag first performing checks:
        /// 1. the arg exists
        /// 2. not grabbing the next arg tag
        /// </summary>
        /// <param name="args"></param>
        /// <param name="place"></param>
        /// <param name="argNeeded"></param>
        /// <param name="defaultArg"></param>
        /// <returns>The arg value or the default if it was missing</returns>
        public static string GetArg(this string[] args, int place, string argNeeded, string defaultArg)
        {
            if ((place + 1) < args.Length)              //make sure the arg list is long enough
            {
                if (!args[place + 1].StartsWith("-", StringComparison.OrdinalIgnoreCase))    //make not just grabbing the next arg tag
                    return args[place + 1];
            }

            return defaultArg;
        }

        /// <summary>
        /// Outputs application usage text to console.
        /// </summary>
        public static void ShowUsage()
        {
            Console.WriteLine("- i input file name here -o output file name here");
        }

        /// <summary>
        /// cleans path from invalid chars
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string CleanFileName(this string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }
    }
}