using System;
using System.Diagnostics;
using System.Web;
using System.Windows.Forms;
using Dynamsoft.TwainDirect.Cloud.Client;
using System.Threading;

namespace Dynamsoft.TwainDirect.Cloud.RegistForms
{
    /// <summary>
    /// Simple form that simplifies Facebook Authentication process.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FacebookLoginForm : Form
    {
        private const string AuthorizationTokenName = "authorization_token";
        private const string RefreshTokenName = "refresh_token";

        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookLoginForm"/> class.
        /// </summary>
        /// <param name="loginUrl">The login URL.</param>
        public FacebookLoginForm(string loginUrl)
        {
            InitializeComponent();

            ThreadWebBrowser(loginUrl);
        }

        private void ThreadWebBrowser(string url)
        {
            webBrowser.Navigate(url);
            //Thread tread = new Thread(new ParameterizedThreadStart(BeginCatch));
            //tread.SetApartmentState(ApartmentState.STA);
            //tread.Start(url);
        }

        private void BeginCatch(object obj)
        {
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.Navigate(obj.ToString());
        }

        /// <summary>
        /// Occurs when TWAIN Cloud successfully authorized the user and issued access tokens.
        /// </summary>
        public event EventHandler<TwainCloudAuthorizedEventArgs> Authorized;

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            string url = e.Url.ToString();
            Debug.WriteLine(url);

            if (url.StartsWith("http://52.199.8.228/")) {

                var queryParams = HttpUtility.ParseQueryString(e.Url.Query);
                var authToken = queryParams[AuthorizationTokenName];
                var refreshToken = queryParams[RefreshTokenName];

                // There will be several redirects when new user accesses the app.
                // Make sure we fire Authorized event only when we have both token successfully extracted.
                if (!string.IsNullOrEmpty(authToken) && !string.IsNullOrEmpty(refreshToken))
                {
                    webBrowser.Document.Write("Loading...");
                    OnAuthorized(new TwainCloudAuthorizedEventArgs(new TwainCloudTokens(authToken, refreshToken)));
                }
                else
                {
                    webBrowser.Document.Write("Authorization token is invalid.");

                }
            }
        }

        protected virtual void OnAuthorized(TwainCloudAuthorizedEventArgs e)
        {
            Authorized?.Invoke(this, e);
        }
    }

    public class TwainCloudAuthorizedEventArgs : EventArgs
    {
        public TwainCloudAuthorizedEventArgs(TwainCloudTokens tokens)
        {
            Tokens = tokens;
        }

        public TwainCloudTokens Tokens { get; }
    }
}
