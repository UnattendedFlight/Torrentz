using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace TorrentDownloader
{
    class Start
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void Download(string downLoc, string url)
        {
            const int SW_HIDE = 0;
            const int SW_SHOW = 5;
            var handle = GetConsoleWindow();

            Directory.SetCurrentDirectory(downLoc);
            string strCmdText;
            strCmdText = "/C torrent -o " + downLoc + " " + url;
            int st = Process.Start("CMD.exe", strCmdText).Id;
            ShowWindow(handle, SW_HIDE);
            Process.GetProcessById(st).WaitForExit();
            
            // Show
            ShowWindow(handle, SW_SHOW);
            Console.Clear();
            Console.WriteLine("Task ended.");
            return;
        }
    }
}
