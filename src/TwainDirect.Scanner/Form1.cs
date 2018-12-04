﻿///////////////////////////////////////////////////////////////////////////////////////
//
// TwainDirect.Scanner.Form1
//
// This is our main form.  Our goal is to keep it pretty thin, it's sole purpose
// is to act as a presentation layer for when a windowing system is being used,
// so there's no business logic at this level...
//
///////////////////////////////////////////////////////////////////////////////////////
//  Author          Date            Comment
//  M.McLaughlin    29-Nov-2014     Initial Release
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) 2014-2018 Kodak Alaris Inc.
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
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using Dynamsoft.TwainDirect.Cloud.Support;

namespace TwainDirect.Scanner
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Form1 : Form, IDisposable
    {
        ///////////////////////////////////////////////////////////////////////////////
        // Public Methods...
        ///////////////////////////////////////////////////////////////////////////////
        #region Public Methods...

        /// <summary>
        /// Initialize stuff for our form...
        /// </summary>
        public Form1()
        {
            // Localize, the user can override the system default...
            string szCurrentUiCulture = Config.Get("language", "");
            if (string.IsNullOrEmpty(szCurrentUiCulture))
            {
                szCurrentUiCulture = Thread.CurrentThread.CurrentUICulture.ToString();
            }

            Log.Info("UiCulture: " + szCurrentUiCulture + " (not supported, so using en-US)");
            m_resourcemanager = lang_en_US.ResourceManager;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            // Init our form...
            InitializeComponent();

            // Localize...
            this.Text = Config.GetResource(m_resourcemanager, "strFormMainTitle"); // TWAIN Direct: TWAIN Bridge
            m_buttonRegister.Text = Config.GetResource(m_resourcemanager, "strButtonRegisterEllipsis"); // Register...
            m_buttonStart.Text = Config.GetResource(m_resourcemanager, "strButtonStart"); // Start
            m_buttonStop.Text = Config.GetResource(m_resourcemanager, "strButtonStop"); // Stop


            // Handle scaling...
            float fScale = (float)Config.Get("scale", 1.0);
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
            Log.Info("Scale: " + fScale);

            // Events...
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);

            // Instantiate our scanner object...
            m_scanner = new Scanner
            (
                m_resourcemanager,
                Display,
                fScale,
                out m_blNoDevices
            );
            if (m_scanner == null)
            {
                Log.Error("Scanner failed...");
                throw new Exception("Scanner failed...");
            }

            // If we don't have any devices, then don't let the user select
            // the start button...
            if (m_blNoDevices)
            {
                SetButtons(ButtonState.NoDevices);
            }
            else
            {
                SetButtons(ButtonState.WaitingForStart);
            }

            // Have we been asked to run in the background?
            if (Config.Get("background", "false") == "true")
            {
                WindowState = FormWindowState.Minimized;
                this.Hide();
            }
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Private Methods...
        ///////////////////////////////////////////////////////////////////////////////
        #region Private Methods...
        /// <summary>
        /// Tell the user about this program...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuitemAbout_Click(object sender, EventArgs e)
        {
            AboutBox aboutbox = new AboutBox(m_resourcemanager);
            aboutbox.ShowDialog();
        }

        /// <summary>
        /// Set the buttons based on the current state...
        /// </summary>
        private void SetButtons(ButtonState a_ebuttonstate)
        {
            switch (a_ebuttonstate)
            {
                // When we first start up, use this...
                default:
                case ButtonState.Undefined:
                    m_CloudRegisterButton.Enabled = false;
                    m_buttonRegister.Enabled = false;
                    m_buttonStart.Enabled = false;
                    m_buttonStop.Enabled = false;
                    break;

                // We have no devices, they need to register...
                case ButtonState.NoDevices:
                    m_CloudRegisterButton.Enabled = false;
                    m_buttonRegister.Enabled = true;
                    m_buttonStart.Enabled = false;
                    m_buttonStop.Enabled = false;
                    break;

                // We have devices, they can register or start...
                case ButtonState.WaitingForStart:
                    m_CloudRegisterButton.Enabled = true;
                    m_buttonRegister.Enabled = true;
                    m_buttonStart.Enabled = true;
                    m_buttonStop.Enabled = false;
                    break;

                // We're waiting for a command, they can stop...
                case ButtonState.Started:
                    m_CloudRegisterButton.Enabled = false;
                    m_buttonRegister.Enabled = false;
                    m_buttonStart.Enabled = false;
                    m_buttonStop.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// Input text...
        /// </summary>
        /// <param name="a_szTitle">title of the box</param>
        /// <param name="a_szPrompt">prompt to the user</param>
        /// <param name="a_szValue">text typed by the user</param>
        /// <returns>button pressed</returns>
        private static DialogResult InputBox(string a_szTitle, string a_szPrompt, ref string a_szValue)
        {
            DialogResult dialogResult = DialogResult.Cancel;
            Form form = null;
            Label label = null;
            TextBox textBox = null;
            Button buttonOk = null;
            Button buttonCancel = null;

            try
            {
                form = new Form();
                label = new Label();
                textBox = new TextBox();
                buttonOk = new Button();
                buttonCancel = new Button();

                form.Text = a_szTitle;
                label.Text = a_szPrompt;
                textBox.Text = a_szValue;

                buttonOk.Text = "OK";
                buttonCancel.Text = "Cancel";
                buttonOk.DialogResult = DialogResult.OK;
                buttonCancel.DialogResult = DialogResult.Cancel;

                label.SetBounds(9, 20, 472, 13);
                textBox.SetBounds(12, 56, 472, 20);
                buttonOk.SetBounds(328, 92, 75, 23);
                buttonCancel.SetBounds(409, 92, 75, 23);

                label.AutoSize = true;
                textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
                buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                form.ClientSize = new Size(496, 127);
                form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
                form.ClientSize = new Size(Math.Max(400, label.Right + 10), form.ClientSize.Height);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.AcceptButton = buttonOk;
                form.CancelButton = buttonCancel;

                dialogResult = form.ShowDialog();
                a_szValue = textBox.Text;
            }
            catch (Exception exception)
            {
                Log.Error("Something bad happened..." + exception.Message);
            }
            finally
            {
                // On the advice of analyze...
                if (form != null)
                {
                    form.Dispose();
                    form = null;
                }
                if (label != null)
                {
                    label.Dispose();
                    label = null;
                }
                if (textBox != null)
                {
                    textBox.Dispose();
                    textBox = null;
                }
                if (buttonOk != null)
                {
                    buttonOk.Dispose();
                    buttonOk = null;
                }
                if (buttonCancel != null)
                {
                    buttonCancel.Dispose();
                    buttonCancel = null;
                }
            }

            // All done...
            return (dialogResult);
        }

        /// <summary>
        /// Display a message...
        /// </summary>
        /// <param name="a_szMsg">the thing to display</param>
        private void Display(string a_szMsg)
        {
            Log.Info("Display: ==> " + a_szMsg);

            // Let us be called from any thread...
            if (this.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate() { Display(a_szMsg); }));
                return;
            }

            // Okay, do the real work...
            m_richtextboxTask.Text += a_szMsg + Environment.NewLine;
            m_richtextboxTask.Select(m_richtextboxTask.Text.Length - 1, 0);
            m_richtextboxTask.ScrollToCaret();
            m_richtextboxTask.Update();
            this.Refresh();

            // This is bad...
            Application.DoEvents();
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Private Form Controls...
        ///////////////////////////////////////////////////////////////////////////////
        #region Private Form Controls...

        /// <summary>
        /// Cleanup...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.Info("Scanner Closing... ");

            if (m_scanner != null)
            {

                Log.Info("MonitorTasksStop... ");
                m_scanner.MonitorTasksStop(e.CloseReason == CloseReason.UserClosing);
            }

            Log.Info("Scanner Closed. ");
        }

        /// <summary>
        /// Register a device for use...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_buttonRegister_Click(object sender, EventArgs e)
        {
            int iScanner;
            long lResponseCharacterOffset;
            string szNumber;
            string szScanners;
            string szText;
            JsonLookup jsonlookup;
            ApiCmd apicmd;
            DialogResult dialogresult;

            Log.Info("Button Register Clicked");

            // Are you sure?
            // Do you want to register a TWAIN driver?  If so, please make sure your scanner is powered on and connected, and press YES.
            dialogresult = MessageBox.Show
            (
                Config.GetResource(m_resourcemanager, "errRegisterTwainDriver"),
                Config.GetResource(m_resourcemanager, "strFormMainTitle"),
                MessageBoxButtons.YesNo
            );
            if (dialogresult == DialogResult.No)
            {
                return;
            }

            // Turn the buttons off...
            SetButtons(ButtonState.Undefined);
            Display("");
            Display(Config.GetResource(m_resourcemanager, "errLookingForScanners"));

            // Get the list of scanners...
            szScanners = m_scanner.GetAvailableScanners("getproductnames", "");
            if (szScanners == null)
            {
                Display(Config.GetResource(m_resourcemanager, "errNoScannersFound"));
                SetButtons(ButtonState.NoDevices);
                return;
            }
            try
            {
                jsonlookup = new JsonLookup();
                jsonlookup.Load(szScanners, out lResponseCharacterOffset);
            }
            catch
            {
                Display(Config.GetResource(m_resourcemanager, "errNoScannersFound"));
                SetButtons(ButtonState.NoDevices);
                return;
            }

            // Show all the scanners, and then ask for the number of
            // the one to use as the new default...
            szText = "";
            for (iScanner = 0 ;; iScanner++)
            {
                // Get the next scanner...
                string szScanner = jsonlookup.Get("scanners[" + iScanner + "].twidentityProductName");
                if (string.IsNullOrEmpty(szScanner))
                {
                    szScanner = jsonlookup.Get("scanners[" + iScanner + "].sane");
                }

                // We're out of stuff...
                if (string.IsNullOrEmpty(szScanner))
                {
                    break;
                }

                // If this is the current default, make a note of it...
                if (m_scanner.GetTwainLocalTy() == szScanner)
                {
                    szText = (iScanner + 1) + ": " + szScanner + " ***DEFAULT***";
                    Display(szText);
                }
                // Otherwise, just list it...
                else
                {
                    Display((iScanner + 1) + ": " + szScanner);
                }
            }

            // Finish the text for the prompt...
            if (string.IsNullOrEmpty(szText))
            {
                szText =
                    "Enter a number from 1 to " + iScanner + Environment.NewLine +
                    "(there is no current default)" + iScanner;
            }
            else
            {
                szText =
                    "Enter a number from 1 to " + iScanner + Environment.NewLine +
                    szText;
            }

            // Select the default...
            int iNumber = 0;
            for (;;)
            {
                // Prompt the user...
                szNumber = "";
                dialogresult = InputBox
                (
                    "Select Default Scanner",
                    szText,
                    ref szNumber
                );

                // The user wants out...
                if (dialogresult != DialogResult.OK)
                {
                    Display("Canceled...");
                    SetButtons(ButtonState.NoDevices);
                    break;
                }

                // Check the result...
                if (!int.TryParse(szNumber, out iNumber))
                {
                    Display("Please enter a number in the range 1 to " + iScanner);
                    continue;
                }

                // Check the range...
                if ((iNumber < 1) || (iNumber > iScanner))
                {
                    Display("Please enter a number in the range 1 to " + iScanner);
                    continue;
                }

                // We have what we want...
                break;
            }

            // Tell the user what we're up to...
            Display("");
            Display("We're going to ask the scanner some questions.");
            Display("This may take a minute...");

            // Do a deep inquiry on the selected scanner...
            szScanners = m_scanner.GetAvailableScanners("getinquiry", jsonlookup.Get("scanners[" + (iNumber - 1) + "].twidentityProductName"));
            if (string.IsNullOrEmpty(szScanners))
            {
                // We are unable to use the selected scanner.  Please make sure your scanner is turned on and connected before trying again.
                Display("");
                Display(Config.GetResource(m_resourcemanager, "errUnableToUseScanner"));
                MessageBox.Show(Config.GetResource(m_resourcemanager, "errUnableToUseScanner"), Config.GetResource(m_resourcemanager, "strFormMainTitle"));
                SetButtons(ButtonState.NoDevices);
                return;
            }
            try
            {
                jsonlookup = new JsonLookup();
                jsonlookup.Load(szScanners, out lResponseCharacterOffset);
            }
            catch
            {
                Display("");
                Display(Config.GetResource(m_resourcemanager, "errUnableToUseScanner"));
                MessageBox.Show(Config.GetResource(m_resourcemanager, "errUnableToUseScanner"), Config.GetResource(m_resourcemanager, "strFormMainTitle"));
                SetButtons(ButtonState.NoDevices);
                return;
            }

            // See if the user wants to update their note...
            string szNote = m_scanner.GetTwainLocalNote();
            if ((iNumber >= 1) && (iNumber <= iScanner))
            {
                // Tell the user what we're up to...
                Display("");
                Display("We're asking what you'd like to call your scanner.");
                Display("Please look for a dialog box...");

                // Prompt the user...
                dialogresult = InputBox
                (
                    "Enter Note",
                    "Your current note is: " + m_scanner.GetTwainLocalNote() + Environment.NewLine +
                    "Type a new note, or just press the Enter key to keep what you have.",
                    ref szNote
                );

                // The user wants out...
                if ((dialogresult != DialogResult.OK) || string.IsNullOrEmpty(szNote))
                {
                    szNote = m_scanner.GetTwainLocalNote();
                }
            }

            // Register it, make a note if it works by clearing the
            // no devices flag.  Note that the way things work now
            // there is only ever one scanner in the list...
            apicmd = new ApiCmd();
            if (m_scanner.RegisterScanner(jsonlookup, 0, szNote, ref apicmd))
            {
                m_blNoDevices = false;
                Display("Done...");
            }
            else
            {
                Display("Registration failed for: " + iNumber);
                MessageBox.Show("Registration failed for: " + iNumber, "Error");
            }

            // Fix the buttons...
            Display("Registration done...");
            if (m_blNoDevices)
            {
                SetButtons(ButtonState.NoDevices);
            }
            else
            {
                SetButtons(ButtonState.WaitingForStart);
            }
        }

        /// <summary>
        /// Register a device with cloud infrastructure.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void m_CloudRegisterButton_Click(object sender, EventArgs e)
        {
            bool isOK = await m_scanner.RegisterCloudScanner();

            if(isOK)
                m_buttonStart_Click(sender, e);
        }

        /// <summary>
        /// Start polling for work...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void m_buttonStart_Click(object sender, EventArgs e)
        {
            bool blSuccess;

            // Turn all the buttons off...
            SetButtons(ButtonState.Undefined);

            // Is PDF/raster happy and healthy?
            try
            {
                PdfRaster pdfraster = new PdfRaster();
                if (!pdfraster.HealthCheck())
                {
                    DialogResult dialogresult = MessageBox.Show
                    (
                        "We need to install the Visual Studio 2015 Redistributables for PDF/raster.  May we continue?",
                        "Warning",
                        MessageBoxButtons.YesNo
                    );
                    if (dialogresult == DialogResult.Yes)
                    {
                        pdfraster.InstallVisualStudioRedistributables();
                    }
                }
            } catch (Exception e1) {
                Log.Error(e1.Message);
            }
            

            // Start polling...
            Display("");
            Display("Starting, please wait...");
            string szNote = m_scanner.GetTwainLocalNote();
            if (!string.IsNullOrEmpty(szNote))
            {
                Display(m_scanner.GetTwainLocalTy() + " (" + szNote + ")");
            }
            else
            {
                Display(m_scanner.GetTwainLocalTy());
            }

            // Start monitoring the cloud...
            try
            {
                blSuccess = await m_scanner.MonitorTasksStart(new EventHandler((object sender1, EventArgs e1) => {

                    this.Invoke(new Action(()=> {

                        // Turn all the buttons off...
                        SetButtons(ButtonState.Undefined);

                        // Set buttons...
                        SetButtons(ButtonState.WaitingForStart);
                    }));
                    
                }));

                if (!blSuccess)
                {
                    Log.Error("MonitorTasksStart failed...");
                    MessageBox.Show("Failed to start cloud monitoring, check the logs for more information.", Config.GetResource(m_resourcemanager, "strFormMainTitle"));
                    SetButtons(ButtonState.WaitingForStart);
                    return;
                }
            }
            catch (Exception exception)
            {
                Log.Error("MonitorTasksStart failed...");
                MessageBox.Show("Failed to start the cloud monitoring, check the logs for more information." + Environment.NewLine + "Error: " + exception.Message, Config.GetResource(m_resourcemanager, "strFormMainTitle"));
            }

            if (m_scanner.IsTwainCloudStarted())
            {
                
                Display($"TWAIN Cloud is ready for use...{m_scanner.GetTwainCloudId()}");
            }

            // Set buttons...
            SetButtons(ButtonState.Started);
        }

        /// <summary>
        /// Stop polling for work...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_buttonStop_Click(object sender, EventArgs e)
        {
            // Turn all the buttons off...
            SetButtons(ButtonState.Undefined);

            // Staaaaaaahp...
            m_scanner.MonitorTasksStop(true);
            Display("Stop...");

            // Set buttons...
            SetButtons(ButtonState.WaitingForStart);
        }

        /// <summary>
        /// Control autorun...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_checkboxRunOnLogin_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;

            // If we're checked, add us...
            if (checkbox.Checked)
            {
                string szValueName = null;
                RegistryKey registrykeyRun;

                // Check the local machine (needed for legacy)...
                registrykeyRun = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                foreach (string sz in registrykeyRun.GetValueNames())
                {
                    string szValue = Convert.ToString(registrykeyRun.GetValue(sz));
                    if (szValue.ToLowerInvariant().Contains("twaindirect.scanner.exe"))
                    {
                        szValueName = sz;
                        break;
                    }
                }

                // Check the local user...
                if (string.IsNullOrEmpty(szValueName))
                {
                    registrykeyRun = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                    foreach (string sz in registrykeyRun.GetValueNames())
                    {
                        string szValue = Convert.ToString(registrykeyRun.GetValue(sz));
                        if (szValue.ToLowerInvariant().Contains("twaindirect.scanner.exe"))
                        {
                            szValueName = sz;
                            break;
                        }
                    }
                }

                // We don't exist, so add us...
                if (string.IsNullOrEmpty(szValueName))
                {
                    Registry.SetValue
                    (
                        "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                        "TWAIN Direct: TWAIN Bridge",
                        "\"" + System.Reflection.Assembly.GetEntryAssembly().Location + "\" background=true" 
                    );
                }
            }

            // Otherwise, remove us...
            else
            {
                bool blAdmin = false;
                string szValueName = null;
                RegistryKey registrykeyRun;

                // Are we running as admin?
                using (WindowsIdentity windowsidentity = WindowsIdentity.GetCurrent())
                {
                    WindowsPrincipal windowsprincipal = new WindowsPrincipal(windowsidentity);
                    if (windowsprincipal.IsInRole(WindowsBuiltInRole.Administrator))
                    {
                        blAdmin = true;
                    }
                }

                // Check the local machine (needed for legacy)...
                registrykeyRun = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", blAdmin);
                foreach (string sz in registrykeyRun.GetValueNames())
                {
                    string szValue = Convert.ToString(registrykeyRun.GetValue(sz));
                    if (szValue.ToLowerInvariant().Contains("twaindirect.scanner.exe"))
                    {
                        szValueName = sz;
                        break;
                    }
                }

                // Check the local user...
                if (string.IsNullOrEmpty(szValueName))
                {
                    registrykeyRun = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    foreach (string sz in registrykeyRun.GetValueNames())
                    {
                        string szValue = Convert.ToString(registrykeyRun.GetValue(sz));
                        if (szValue.ToLowerInvariant().Contains("twaindirect.scanner.exe"))
                        {
                            szValueName = sz;
                            break;
                        }
                    }
                }

                // Remove it...
                if (!string.IsNullOrEmpty(szValueName))
                {
                    try
                    {
                        registrykeyRun.DeleteValue(szValueName);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Sorry, we couldn't turn off autostart.  Run this program as admin, and try again.", "Error");
                        Log.Error("Failed to delete registry value - " + exception.Message);
                        checkbox.Checked = true;
                    }
                }
            }
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Private Definitions...
        ///////////////////////////////////////////////////////////////////////////////
        #region Private Definitions...

        /// <summary>
        /// The button states (enable/disable)...
        /// </summary>
        private enum ButtonState
        {
            Undefined,
            NoDevices,
            WaitingForStart,
            Started
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Private Attributes...
        ///////////////////////////////////////////////////////////////////////////////
        #region Private Attributes...

        /// <summary>
        /// Our scanner interface...
        /// </summary>
        private Scanner m_scanner;

        /// <summary>
        /// Localized text...
        /// </summary>
        private ResourceManager m_resourcemanager;

        /// <summary>
        /// True if we have no devices...
        /// </summary>
        private bool m_blNoDevices;


        #endregion
    }
}
