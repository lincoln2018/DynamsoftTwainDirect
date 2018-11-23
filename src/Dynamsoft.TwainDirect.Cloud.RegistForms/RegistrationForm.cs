using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dynamsoft.TwainDirect.Cloud.Registration;
using Timer = System.Threading.Timer;

namespace Dynamsoft.TwainDirect.Cloud.RegistForms
{
    public partial class RegistrationForm : Form
    {
        private bool m_bShowTokens = false;
        private RegistrationManager m_manager = null;
        private string m_pollUrl = null;
        private Timer m_pollingTimer = null;

        public RegistrationForm(RegistrationManager manager, RegistrationResponse registrationResponse) : this(manager, registrationResponse, false)
        {
            
        }

        public RegistrationForm(RegistrationManager manager, RegistrationResponse registrationResponse, bool bShowTokens)
        {
            InitializeComponent();

            m_manager = manager;
            m_pollUrl = registrationResponse.PollingUrl;

            m_bShowTokens = bShowTokens;
            registrationUrlLabel.Text = registrationResponse.InviteUrl;
            registrationTokenTextBox.Text = registrationResponse.RegistrationToken;

            if (!m_bShowTokens) {
                registrationTokenTextBox.Visible = false;
                label3.Visible = false;
                this.Height = 250;
            }

            RunTimer();
        }

        public PollResponse PollResponse { get; set; }


        private void ShowFailureResult()
        {
            if (this.InvokeRequired)
            {
                Action action = ShowFailureResult;
                this.Invoke(action);
            }
            else
            {
                progressPictureBox.Image = Properties.Resources.Error;
                statusLabel.Text = "Registration failed. Please try again.";
            }
        }

        private void ShowSuccessResult()
        {
            if (this.InvokeRequired)
            {
                Action action = ShowSuccessResult;
                this.Invoke(action);
            }
            else
            {
                progressPictureBox.Image = Properties.Resources.Information;
                statusLabel.Text = "Registration Success. Closing ...";

                m_pollingTimer = new Timer(state =>
                {
                    m_pollingTimer.Dispose();
                    m_pollingTimer = null;
                    CloseFormNow();
                }, null, 0, 4000);

            }
        }


        private void CloseFormNow()
        {
            if (this.InvokeRequired)
            {
                Action action = CloseFormNow;
                this.Invoke(action);
            }
            else
            {
                this.Close();
            }
        }

        private void linkLabel2_LinkClickedAsync(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(((LinkLabel)sender).Text);
        }

        private void RunTimer() {

            int pollingCounter = 0;
            m_pollingTimer = new Timer(state =>
            {
                pollingCounter++;
                var pollResult = m_manager.Poll(m_pollUrl).Result;

                if (pollResult.Success)
                {
                    m_pollingTimer.Dispose();
                    m_pollingTimer = null;
                    PollResponse = pollResult;
                    ShowSuccessResult();

                }
                else
                {
                    if (pollingCounter > 120) // 10 minutes
                    {
                        m_pollingTimer.Dispose();
                        m_pollingTimer = null;
                        ShowFailureResult();
                    }

                }
            }, null, 0, 5000);
        }

        protected override void OnClosed(EventArgs e)
        {
            if (this.m_pollingTimer != null)
            {
                this.m_pollingTimer.Dispose();
                this.m_pollingTimer = null;
            }
            base.OnClosed(e);
        }
    }
}
