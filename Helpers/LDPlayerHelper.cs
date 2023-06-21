using System.Diagnostics;

namespace CameraTool.Helpers
{
    public class LDPlayerHelper
    {
        private string ABSOLUTE_PATH = @"E:\LDPlayer\LDPlayer9\ldconsole.exe";

        public void SetResolution(string name, int w, int h, int dpi)
        {
            string cmd = $"modify --name {name} --lockwindow 1 --resolution {w},{h},{dpi}";
            Execute(cmd);
        }

        public void OpenAppWithName(string name)
        {
            OpenApp("name", name);
        }

        public void OpenAppWithIndex(string index)
        {
            OpenApp("index", index);
        }

        public void SortTabs()
        {
            string shell = $"sortWnd";
            Execute(shell);
        }

        private void OpenApp(string name, string nameOrId)
        {
            string cmd = $"launch --{name} {nameOrId}";
            Execute(cmd);
        }

        public void Execute(string cmd)
        {
            Process process = new Process();
            process.StartInfo.FileName = ABSOLUTE_PATH;
            process.StartInfo.Arguments = cmd;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.EnableRaisingEvents = true;
            process.Start();
            process.WaitForExit();
            process.Close();
        }
    }
}