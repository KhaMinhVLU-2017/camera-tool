using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CameraTool.Helpers
{
    public class ADBHelper
    {
        private string TAP_DEVICES = "adb -s {0} shell input tap {1} {2}";

        public IEnumerable<string> GetDevices()
        {
            List<string> list = new List<string>();
            string input = Execute("adb devices");
            string pattern = "(?<=List of devices attached)([^\\n]*\\n+)+";
            MatchCollection matchCollection = Regex.Matches(input, pattern, RegexOptions.Singleline);
            if (matchCollection.Count > 0)
            {
                string value = matchCollection[0].Groups[0].Value;
                string[] array = Regex.Split(value, "\r\n");
                string[] array2 = array;
                foreach (string text in array2)
                {
                    if (string.IsNullOrEmpty(text) || !(text != " "))
                    {
                        continue;
                    }
                    string[] array3 = text.Trim().Split('\t');
                    string text2 = array3[0];
                    string text3 = "";
                    try
                    {
                        text3 = array3[1];
                        if (text3 != "device")
                        {
                            continue;
                        }
                    }
                    catch
                    {
                    }
                    list.Add(text2.Trim());
                }
            }
            return list;
        }

        public void Tap(string deviceId, double x, double y)
        {
            string shell = $"adb -s {deviceId} shell input tap {x} {y}";
            Execute(shell);
        }

        public async Task<Point> GetScreenResolutionAsync(string deviceID)
        {
            string cmdCommand = string.Format("adb -s {0} shell dumpsys display ", deviceID);
            string text = Execute(cmdCommand);
            text = text.Substring(text.IndexOf("mCurrentDisplayRect=Rect"));
            text = text.Substring(text.IndexOf(' '), text.IndexOf(')') - text.IndexOf(' '));
            text = text.Substring(text.IndexOf('-') + 1);
            Console.WriteLine(text);
            string[] array = text.Split(',');
            int x = Convert.ToInt32(array[0].Trim());
            int y = Convert.ToInt32(array[1].Trim());
            return new Point(x, y);
        }

        public void TapByPercent(string deviceID, double x, double y, int count = 1)
        {
            Point screenResolution = GetScreenResolutionAsync(deviceID).GetAwaiter().GetResult();
            int num = (int)(x * ((double)screenResolution.X * 1.0 / 100.0));
            int num2 = (int)(y * ((double)screenResolution.Y * 1.0 / 100.0));
            string text = string.Format(TAP_DEVICES, deviceID, num, num2);
            for (int i = 1; i < count; i++)
            {
                text = text + " && " + string.Format(TAP_DEVICES, deviceID, x, y);
            }
            Execute(text);
        }

        public string Execute(string cmdCommand)
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "cmd.exe";
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.Verb = "runas";
                process.StartInfo = processStartInfo;
                process.Start();
                process.StandardInput.WriteLine(cmdCommand);
                process.StandardInput.Flush();
                process.StandardInput.Close();
                string str = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                process.Kill();
                process.Dispose();
                return str;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}