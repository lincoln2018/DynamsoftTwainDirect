﻿using System;
using System.IO;
using System.Windows.Forms;
using Dynamsoft.TwainDirect.Cloud.Support;

namespace TwainDirect.Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="a_aszArgs">interesting arguments</param>
        [STAThread]
        static void Main(string[] a_aszArgs)
       {
			// TODO: Handle this correctly - this is a temporary workaround.
	        // Trust all certificates
	        System.Net.ServicePointManager.ServerCertificateValidationCallback =
		        ((sender, certificate, chain, sslPolicyErrors) => true);

			FormScan formscan;

            // Basic initialization stuff...
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Load our configuration information and our arguments,
            // so that we can access them from anywhere in the code...
            if (!Config.Load(Application.ExecutablePath, a_aszArgs, "appdata.txt"))
            {
                MessageBox.Show("Error starting.  Try uninstalling and reinstalling this software.", "TWAIN Direct: Application");
                Application.Exit();
            }

            // If we don't have a tasks folder in the user's folder, make one...
            string szTasksSrc = Path.Combine(Config.Get("readFolder", ""), Path.Combine("data", "tasks"));
            string szTasksDst = Path.Combine(Config.Get("writeFolder", ""), "tasks");
            if (!Directory.Exists(szTasksDst))
            {
                try
                {
                    Directory.CreateDirectory(szTasksDst);
                }
                catch (Exception exception)
                {
                    // Oh well...
                    Log.Error("FormScan: create directory failed - " + exception.Message);
                }
            }

            // If we got our folders, see about copying stuff...
            if (Directory.Exists(szTasksSrc) && Directory.Exists(szTasksDst))
            {
                // .tdt is TWAIN Direct Task, if we don't have any in our
                // user's folder, then copy over the ones installed with
                // the binary, otherwise leave them alone...
                string[] aszFiles = Directory.GetFiles(szTasksDst, "*.tdt");
                if ((aszFiles == null) || (aszFiles.Length == 0))
                {
                    aszFiles = Directory.GetFiles(szTasksSrc);
                    foreach (string szFile in aszFiles)
                    {
                        try
                        {
                            File.Copy(szFile, Path.Combine(szTasksDst, Path.GetFileName(szFile)), true);
                        }
                        catch
                        {
                            // Just keep going...
                        }
                    }
                }
            }

            // Run our form...
            formscan = new FormScan();
            try
            {
                if (!formscan.ExitRequested())
                {
                    Application.Run(formscan);
                }
            }
            catch (Exception exception)
            {
                Log.Error("Main exception - " + exception.Message);
            }
            finally
            {
                formscan.Dispose();
                formscan = null;
            }
        }
    }
}
