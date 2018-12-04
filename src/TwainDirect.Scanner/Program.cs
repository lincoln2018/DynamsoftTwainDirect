// Helpers...
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Dynamsoft.TwainDirect.Cloud.Support;

namespace TwainDirect.Scanner
{
    /// <summary>
    /// Our entry point.  From here we'll dispatch to the mode: window, terminal
    /// or service, along with any interesting arguments...
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="a_aszArgs">interesting arguments</param>
        [STAThread]
        static void Main(string[] a_aszArgs)
        {
            string szExecutableName;
            string szWriteFolder;

            // Are we already running?
            Process[] aprocess = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location));
            foreach (Process process in aprocess)
            {
                // If it ain't us, it's somebody else...
                if (process.Id != Process.GetCurrentProcess().Id)
                {
                    MessageBox.Show("This program is already running.  If you don't see it on the screen, check the system tray for the TWAIN Direct icon, and right click on it for the list of options.", "TWAIN Direct on TWAIN Bridge");
                    Environment.Exit(1);
                }
            }

            // Load our configuration information and our arguments,
            // so that we can access them from anywhere in the code...
            if (!Config.Load(Application.ExecutablePath, a_aszArgs, "appdata.txt"))
            {
                MessageBox.Show("Error starting.  Try uninstalling and reinstalling this software.", "Error");
                Environment.Exit(1);
            }

            // Set up our data folders...
            szWriteFolder = Config.Get("writeFolder", "");
            szExecutableName = Config.Get("executableName", "");

            // Turn on logging...
            Log.Open(szExecutableName, szWriteFolder, 1);
            Log.SetLevel((int)Config.Get("logLevel", 1));
            Log.Info(szExecutableName + " Log Started...");

            // Make sure that any stale TwainDirectOnTwain processes are gone...
            foreach (Process processTwainDirectOnTwain in Process.GetProcessesByName("TwainDirect.OnTwain"))
            {
                try
                {
                    processTwainDirectOnTwain.Kill();
                }
                catch (Exception exception)
                {
                    Log.Error("unable to kill TwainDirect.OnTwain - " + exception.Message);
                }
            }

            // Figure out what we're doing...

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Form1 form1 = new Form1();
                    Application.Run(form1);
                    form1.Dispose();

            // All done...
            Log.Info(szExecutableName + " Log Ended...");
            Log.Close();
            Environment.Exit(0);
        }

    }
}
