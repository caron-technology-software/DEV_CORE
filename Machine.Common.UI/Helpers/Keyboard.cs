using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.UI
{
    public class Keyboard
    {
        public static void ShowOnScreenKeyboard()
        {
            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "Sysnative", "cmd.exe");
                process.StartInfo.Arguments = "/c osk.exe";
                process.StartInfo.CreateNoWindow = true;

                process.Start();
            }
        }
    }
}
