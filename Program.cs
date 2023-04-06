using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiInstanceDiscord
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Discord";
            String[] dirs = Directory.GetDirectories(dir).Where(cur => cur.Contains("app")).ToArray();

            String curdir = dirs[0];
            String earliest = dirs[0];

            for (int i = 0; i < dirs.Count(); i++)
            {

                if (Directory.GetCreationTime(dirs[i]) > Directory.GetCreationTime(earliest))
                {
                    earliest = dirs[i];
                }

                curdir = dirs[i];
            }
            LaunchDiscord(earliest);

        }
        static void LaunchDiscord(string earliest)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = earliest + "\\Discord.exe";
            startInfo.Arguments = "--multi-instance";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }
        }
    }
}
