using System;
using Exercise.Helper;
using Exercise.ProcessHandlersInterfaces;

namespace Exercise
{
    public class ProcessCommandLineArgs
    {
        private readonly IDependencySorter _dependencySorter;

        public ProcessCommandLineArgs(IDependencySorter dependencySorter)
        {
            _dependencySorter = dependencySorter;
        }

        public bool ProcessArgs(string[] args)
         {
             string inFileName = String.Empty;
             string responseFileName = String.Empty;

             args.GetCommandLineSwitch(Constants.INFILE_SWITCH, ref inFileName);
             args.GetCommandLineSwitch(Constants.OUTFILE_SWITCH, ref responseFileName);

             if (inFileName.Length > 0 && responseFileName.Length > 0)
             {
                 _dependencySorter.MainDriver(inFileName, responseFileName);
             }
             else
             {
                 CommandLineHelper.ShowUsage();
                 return false;
             }
            return true;
         }
    }
}