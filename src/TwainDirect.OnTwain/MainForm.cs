///////////////////////////////////////////////////////////////////////////////////////
//
//  TwainDirect.OnTwain.MainForm
//
//  This is our interactive dialog that allows users to select the TWAIN driver
//  they want to use and experiment with SWORD tasks.
//
///////////////////////////////////////////////////////////////////////////////////////
//  Author          Date            Comment
//  M.McLaughlin    16-Jun-2014     Initial Release
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
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Dynamsoft.TwainDirect.Cloud.Support;

namespace TwainDirect.OnTwain
{
    /// <summary>
    /// Our main form...
    /// </summary>
    public partial class MainForm : Form
    {
        // Our constructor...
        /// <summary>
        /// 
        /// </summary>
        public MainForm()
        {
            string szScanner;

            // Init the form...
            InitializeComponent();
            this.Load += new EventHandler(MainForm_Load);
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);

            // Init stuff...
            m_processswordtask = null;

            // Remember this stuff...
            m_szExecutablePath = Config.Get("executablePath", "");
            m_szReadFolder = Config.Get("readFolder", "");
            m_szWriteFolder = Config.Get("writeFolder", "");
            szScanner = Config.Get("scanner", null);

            // Check for a TWAIN driver, yelp if we can't find one...
            m_szTwainDefaultDriver = ProcessSwordTask.GetCurrentDriver(m_szWriteFolder, szScanner);
            if (m_szTwainDefaultDriver == null)
            {
                MessageBox.Show("There are no TWAIN drivers installed on this system.", "Error");
                SetButtonMode(ButtonMode.Disabled);
            }
        }

        /// <summary>
        /// Load the main dialog...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Populate the listview with tasks...
            try
            {
                m_listviewTasks.Items.Clear();
                string[] files = Directory.GetFiles(Path.Combine(m_szWriteFolder,"tasks"));
                foreach (string file in files)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    ListViewItem item = new ListViewItem(fileName);
                    item.Tag = file;
                    m_listviewTasks.Items.Add(item);
                }
                m_listviewTasks.View = View.Details;
                m_listviewTasks.Refresh();
                if (m_listviewTasks.Items.Count > 0)
                {
                    m_listviewTasks.Items[0].Selected = true;
                    m_listviewTasks.Select();
                }
            }
            catch
            {
                // Sorry, couldn't do the list.  We're probably missing the task folder...
                MessageBox.Show("Couldn't access the task folder");
            }
        }

        /// <summary>
        /// The user is trying to exit...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.Close();
            if (m_timer != null)
            {
                m_timer.Stop();
                m_timer = null;
            }
            if (m_processswordtask != null)
            {
                m_processswordtask.Close();
                m_processswordtask = null;
            }
        }
 
        /// <summary>
        /// Cancel a scanning session...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_buttonCancel_Click(object sender, EventArgs e)
        {
            // Removed...
        }

        /// <summary>
        /// Wait for a scanning session to end...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_timer_Tick(object sender, EventArgs e)
        {
            // We still have a manager...
            if (m_processswordtask != null)
            {
                // Processing this action is done...
                if (!m_processswordtask.IsProcessing())
                {
                    // Cleanup...
                    if (m_timer != null)
                    {
                        m_timer.Enabled = false;
                        m_timer = null;
                    }
                    m_processswordtask.Close();
                    m_processswordtask = null;

                    // Turn the buttons on...
                    SetButtonMode(ButtonMode.Ready);
                }
            }
        }
        
        

        /// <summary>
        /// Manage our buttons in one place to reduce insanity...
        /// </summary>
        /// <param name="a_buttonmode">The mode we'd like to show</param>
        private void SetButtonMode(ButtonMode a_buttonmode)
        {
            switch (a_buttonmode)
            {
                default:
                case ButtonMode.Disabled:
                    AcceptButton = null;
                    break;

                case ButtonMode.Ready:
                    break;

                case ButtonMode.Scanning:
                    break;

                case ButtonMode.Canceled:
                    break;
            }

            // Why the hell do I have this?
            Application.DoEvents();
        }

        /// <summary>
        /// Private attributes...
        /// </summary>
        private enum ButtonMode
        {
            Disabled,
            Ready,
            Scanning,
            Canceled
        }
        private string m_szExecutablePath;
        private string m_szReadFolder;
        private string m_szWriteFolder;
        private Timer m_timer;
        private string m_szTwainDefaultDriver;
        private ProcessSwordTask m_processswordtask;
    }
}
