﻿///////////////////////////////////////////////////////////////////////////////////////
//
//  TwainDirect.App.FormSetup
//
//  This class helps us configure a TWAIN driver prior to scanning.
//
///////////////////////////////////////////////////////////////////////////////////////
//  Author          Date            Comment
//  M.McLaughlin    21-Oct-2013     Initial Release
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) 2013-2018 Kodak Alaris Inc.
//
//  Permission is hereby granted, free of charge, to any person obtaining a
//  copy of this software and associated documentation files (the "Software"),
//  to deal in the Software without restriction, including without limitation
//  the rights to use, copy, modify, merge, publish, distribute, sublicense,
//  and/or sell copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
//  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//  DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////

// Helpers...
using System;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Dynamsoft.TwainDirect.Cloud.Support;

namespace TwainDirect.Client
{
    /// <summary>
    /// Select the image destination folder.  If supported, allow the user to
    /// create and select Custom DS Data snapshots of the driver, and give
    /// them a way to change the driver settings through the setup form of the
    /// driver's user interface...
    /// </summary>
    public partial class FormSetup : Form
    {
        ///////////////////////////////////////////////////////////////////////////////
        // Public Methods...
        ///////////////////////////////////////////////////////////////////////////////
        #region Public Methods...

        /// <summary>
        /// Init stuff...
        /// </summary>
        /// <param name="a_dnssddeviceinfo">the device we're talking to</param>
        /// <param name="a_twainlocalscannerclient">our interface to the scanner</param>
        /// <param name="a_szWriteFolder">where we get/put stuff</param>
        /// <param name="a_resourcemanager">for localization</param>
        /// <param name="a_twainlocalscannerclient">for encryptionReport</param>
        public FormSetup
        (
            Dnssd.DnssdDeviceInfo a_dnssddeviceinfo,
            TwainLocalScannerClient a_twainlocalscannerclient,
            string a_szWriteFolder,
            ResourceManager a_resourcemanager
        )
        {
            float fScale;

            // Init stuff...
            InitializeComponent();
            m_dnssddeviceinfo = a_dnssddeviceinfo;
            m_resourcemanager = a_resourcemanager;

            // Handle scaling...
            fScale = (float)Config.Get("scale", 1.0);
            if (fScale <= 1)
            {
                fScale = 1;
            }
            else if (fScale > 2)
            {
                fScale = 2;
            }
            if (fScale != 1)
            {
                this.Font = new Font(this.Font.FontFamily, this.Font.Size * fScale, this.Font.Style);
            }

            // Localize...
            m_labelSelectDestinationFolder.Text = Config.GetResource(m_resourcemanager, "strLabelSelectImageDestination");
            this.Text = Config.GetResource(m_resourcemanager, "strFormSetupTitle");

            // More init stuff...
            m_twainlocalscannerclient = a_twainlocalscannerclient;
            this.FormClosing += new FormClosingEventHandler(FormSetup_FormClosing);

            // Location of current task...
            m_szCurrentTaskFile = Path.Combine(a_szWriteFolder, "currenttask");

            // We're putting the tasks into the write folder...
            m_szTasksFolder = Path.Combine(a_szWriteFolder, "tasks");
            if (!Directory.Exists(m_szTasksFolder))
            {
                try
                {
                    Directory.CreateDirectory(m_szTasksFolder);
                }
                catch (Exception exception)
                {
                    Log.Error("Can't create folder <" + m_szTasksFolder + ">, so using current folder - " + exception.Message);
                    m_szTasksFolder = Directory.GetCurrentDirectory();
                }
            }

            // Restore values...
            m_textboxFolder.Text = RestoreFolder();
            m_textboxUseUiSettings.Text = "";
            if (File.Exists(m_szCurrentTaskFile))
            {
                m_textboxUseUiSettings.Text = File.ReadAllText(m_szCurrentTaskFile);
            }
        }

        /// <summary>
        /// Include the JSON metadata when transferring an image, an
        /// application can do this to skip a step for better performance
        /// when they know they always want to get the image.
        /// </summary>
        /// <returns>true if the user would like to get the JSON metadata with an image</returns>
        public bool GetMetadataWithImage()
        {
            return (m_checkboxMetadataWithImage.Checked);
        }

        /// <summary>
        /// Get the task...
        /// </summary>
        /// <returns></returns>
        public string GetTask()
        {
            string szTaskFile;
            string szTask;

            // Build the full path...
            szTaskFile = Path.Combine(m_szTasksFolder, m_textboxUseUiSettings.Text);

            // Fix the path...
            if (!szTaskFile.StartsWith("/") && !szTaskFile.StartsWith("\\") && (szTaskFile[1] != ':'))
            {
                szTaskFile = Path.DirectorySeparatorChar + szTaskFile;
            }

            // If we find the file, return its contents...
            szTask = "";
            if (File.Exists(szTaskFile))
            {
                try
                {
                    szTask = File.ReadAllText(szTaskFile);
                }
                catch (Exception exception)
                {
                    Log.Error("Error reading: " + szTaskFile + " - " + exception.Message);
                    szTask = "";
                }
            }

            // No joy...
            if (string.IsNullOrEmpty(szTask))
            {
                Log.Error("No task found, so returning simple task...");
                return
                (
                    "{" +
                    "\"actions\":[" +
                    "{" +
                    "\"action\":\"configure\"" +
                    "}" +
                    "]" +
                    "}"
                );
            }

            // A little bit of weirdness, so we can run the cert tests...
            if (    szTask.Contains("***DATADATADATA***")
                &&  szTask.Contains("category")
                &&  szTask.Contains("summary")
                &&  szTask.Contains("expectedCode"))
            {
                string[] asz = szTask.Split(new string[] { "***DATADATADATA***\r\n", "***DATADATADATA***\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (asz.Length > 1)
                {
                    szTask = asz[1];
                }
            }

            // All done...
            return (szTask);
        }

        /// <summary>
        /// Get the name of the task...
        /// </summary>
        /// <returns>name of the task</returns>
        public string GetTaskName()
        {
            return (Path.GetFileNameWithoutExtension(m_textboxUseUiSettings.Text));
        }

        /// <summary>
        /// We're not actually getting a thumbnail, instead we're asking
        /// if there's a request for thumbnails in the metadata...
        /// </summary>
        /// <returns>true if the user would like thumbnails</returns>
        public bool GetThumbnails()
        {
            return (m_checkboxThumbnails.Checked);
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Private Methods...
        ///////////////////////////////////////////////////////////////////////////////
        #region Private Methods...

        /// <summary>
        /// Validate...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FormSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        /// <summary>
        /// Get the folder path...
        /// </summary>
        /// <returns></returns>
        private string RestoreFolder()
        {
            try
            {
                string szSaveSpot = m_szTasksFolder;
                if (!Directory.Exists(szSaveSpot))
                {
                    return (m_twainlocalscannerclient.GetImagesFolder());
                }
                szSaveSpot = File.ReadAllText(Path.Combine(szSaveSpot, "folder"));
                if (!Directory.Exists(szSaveSpot))
                {
                    return (m_twainlocalscannerclient.GetImagesFolder());
                }
                return (szSaveSpot);
            }
            catch
            {
                return (m_twainlocalscannerclient.GetImagesFolder());
            }
        }

        /// <summary>
        /// Get the setting...
        /// </summary>
        /// <returns></returns>
        private string RestoreSetting()
        {
            try
            {
                if (!Directory.Exists(m_szTasksFolder))
                {
                    return ("");
                }
                return (File.ReadAllText(Path.Combine(m_szTasksFolder, "current")));
            }
            catch
            {
                return ("");
            }
        }

        /// <summary>
        /// Remember the folder path for the next time the app runs...
        /// </summary>
        /// <param name="a_szFolder"></param>
        private void SaveFolder(string a_szFolder)
        {
            try
            {
                string szSaveSpot = m_szTasksFolder;
                if (!Directory.Exists(szSaveSpot))
                {
                    Directory.CreateDirectory(szSaveSpot);
                }
                File.WriteAllText(Path.Combine(szSaveSpot, "folder"), a_szFolder);
            }
            catch
            {
                // Just let it slide...
            }
        }

        /// <summary>
        /// Remember the setting for the next time the app runs...
        /// </summary>
        /// <param name="a_szCurrentTaskName">filename of current task</param>
        private void SaveSetting(string a_szCurrentTaskName)
        {
            try
            {
                File.WriteAllText(m_szCurrentTaskFile, a_szCurrentTaskName);
            }
            catch
            {
                // Just let it slide...
            }
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Private Form Controls...
        ///////////////////////////////////////////////////////////////////////////////
        #region Private Form Controls...

        /// <summary>
        /// Browse for a place to save image files...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_buttonBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog = new FolderBrowserDialog();
            try
            {
                folderbrowserdialog.SelectedPath = m_textboxFolder.Text;
                folderbrowserdialog.ShowNewFolderButton = true;
                if (folderbrowserdialog.ShowDialog() == DialogResult.OK)
                {
                    m_textboxFolder.Text = folderbrowserdialog.SelectedPath;
                    SaveFolder(m_textboxFolder.Text);
                }
            }
            catch (Exception exception)
            {
                Log.Error("Something bad happened..." + exception.Message);
            }
            finally
            {
                folderbrowserdialog.Dispose();
                folderbrowserdialog = null;
            }
        }

        /// <summary>
        /// Bring up Dynamsoft's advanced task generation page...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_buttonGenerateTaskAdvanced_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Path.Combine(Config.Get("writeFolder", ""), "tasks"));
            System.Diagnostics.Process.Start("https://www.dynamsoft.com/Demo/TwainDirectTaskGeneratorOnline/advanced.html");
        }

        /// <summary>
        /// Bring up Dynamsoft's basic task generation page...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_buttonGenerateTaskBasic_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Path.Combine(Config.Get("writeFolder", ""), "tasks"));
            System.Diagnostics.Process.Start("https://www.dynamsoft.com/Demo/TwainDirectTaskGeneratorOnline/Basic.html");
        }

        /// <summary>
        /// Bring up Dynamsoft's PDF/raster validator page...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_buttonValidatePdfRaster_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Path.Combine(Config.Get("writeFolder", ""), "images"));
            System.Diagnostics.Process.Start("https://www.dynamsoft.com/Demo/TwainDirectPdfRasterValidatorOnline/index.html");
        }

        /// <summary>
        /// Bring up Dynamsoft's task validator page...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void m_buttonValidateTask_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Path.Combine(Config.Get("writeFolder", ""), "tasks"));
            System.Diagnostics.Process.Start("https://www.dynamsoft.com/Demo/TwainDirectTaskValidatorOnline/index.html");
        }

        /// <summary>
        /// Send an encryption report to the scanner and show the results...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_buttonGetEncryptionReport_Click(object sender, EventArgs e)
        {
            bool blSuccess;
            long lJsonErrorIndex = 0;
            ApiCmd apicmd;
            JsonLookup jsonlookup;

            // Issue the command...
            apicmd = new ApiCmd(m_dnssddeviceinfo);
            m_twainlocalscannerclient.ClientScannerSendTask("{\"actions\":[{\"action\":\"encryptionReport\"}]}", ref apicmd);
            blSuccess = m_twainlocalscannerclient.ClientCheckForApiErrors("ClientScannerSendTask", ref apicmd);
            if (!blSuccess)
            {
                MessageBox.Show("Command failed...", Config.GetResource(m_resourcemanager, "strFormScanTitle"));
                return;
            }

            // Parse the JSON...
            jsonlookup = new JsonLookup();
            blSuccess = jsonlookup.Load(apicmd.GetHttpResponseData(), out lJsonErrorIndex);
            if (!blSuccess)
            {
                MessageBox.Show("JSON error at: " + lJsonErrorIndex, Config.GetResource(m_resourcemanager, "strFormScanTitle"));
                return;
            }

            // Show the result...
            string szTask = jsonlookup.Get("results.session.task");
            if (!string.IsNullOrEmpty(szTask))
            {
                MessageBox.Show(szTask, Config.GetResource(m_resourcemanager, "strFormScanTitle"));
            }
        }

        /// <summary>
        /// Pick the settings for a scan session...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_buttonUseUiSettings_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            try
            {
                openfiledialog.InitialDirectory = m_szTasksFolder;
                openfiledialog.CheckFileExists = true;
                openfiledialog.CheckPathExists = true;
                openfiledialog.Multiselect = false;
                openfiledialog.Filter = "Tasks|*.tdt";
                openfiledialog.FilterIndex = 0;
                if (!Directory.Exists(openfiledialog.InitialDirectory))
                {
                    try
                    {
                        Directory.CreateDirectory(openfiledialog.InitialDirectory);
                    }
                    catch (Exception exception)
                    {
                        // Unable to create settings folder.
                        MessageBox.Show
                        (
                            Config.GetResource(m_resourcemanager, "errCantCreateSettingsFolder") + "\n" +
                            "\n" +
                            m_szTasksFolder + "\n" +
                            "\n" +
                            exception.Message,
                            Config.GetResource(m_resourcemanager, "strFormScanTitle")
                        );
                        return;
                    }
                }
                if (openfiledialog.ShowDialog() == DialogResult.OK)
                {
                    m_textboxUseUiSettings.Text = Path.GetFileName(openfiledialog.FileName);
                    SaveSetting(m_textboxUseUiSettings.Text);
                }
            }
            catch (Exception exception)
            {
                Log.Error("Something bad happened..." + exception.Message);
            }
            finally
            {
                openfiledialog.Dispose();
                openfiledialog = null;
            }
        }

        /// <summary>
        /// Keep us updated with changes to the save image path...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_textboxFolder_TextChanged(object sender, EventArgs e)
        {
            if (m_twainlocalscannerclient.SetImagesFolder(m_textboxFolder.Text))
            {
                SaveFolder(m_textboxFolder.Text);
            }
            else
            {
                m_textboxFolder.Text = m_twainlocalscannerclient.GetImagesFolder();
            }
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Private Attributes...
        ///////////////////////////////////////////////////////////////////////////////
        #region Private Attributes...

        /// <summary>
        /// So we can pick a new images folder...
        /// </summary>
        private TwainLocalScannerClient m_twainlocalscannerclient;

        /// <summary>
        /// The device we're talking to...
        /// </summary>
        private Dnssd.DnssdDeviceInfo m_dnssddeviceinfo;

        /// <summary>
        /// For localization...
        /// </summary>
        private ResourceManager m_resourcemanager;

        /// <summary>
        /// The settings folder...
        /// </summary>
        private string m_szTasksFolder;

        /// <summary>
        /// Where we keep the name of the current task, which we
        /// set ourselves too on startup...
        /// </summary>
        private string m_szCurrentTaskFile;

        #endregion
    }
}
