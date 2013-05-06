namespace Exercise.Helper
{
    public interface ICommandLineHelper
    {
        string GetArg(string[] args, int place, string argNeeded, string defaultArg);
        bool GetCommandLineSwitch(string[] args, string switchName);
        void GetCommandLineSwitch(string[] args, string switchName, ref string switchValue);
        void ShowUsage();
    }
}
