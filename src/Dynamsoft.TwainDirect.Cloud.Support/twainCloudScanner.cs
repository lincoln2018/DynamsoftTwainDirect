
using System.Diagnostics;
using System.Threading.Tasks;
using Dynamsoft.TwainDirect.Cloud.Application;
using Dynamsoft.TwainDirect.Cloud.Client;
using System.IO;
using System;
using Dynamsoft.TwainDirect.Cloud.Support.Dnssd;

namespace Dynamsoft.TwainDirect.Cloud.Support
{
    public class TwainCloudScannerClient : TwainLocalScannerClient
    {
        private TwainCloudClient m_client;

        public TwainCloudScannerClient(EventCallback a_eventcallback,
            object a_objectEventCallback,
            bool a_blCreateTwainLocalSession) : base(
            a_eventcallback,
            a_objectEventCallback,
            a_blCreateTwainLocalSession)
        {
        }

        public bool isCloudValid()
        {
            if (m_client == null || m_applicationmanager == null)
                return false;

            return true;
        }
        public void SetToken(string authorizationToken)
        {
            this.m_dictionaryExtraHeaders.Remove("Authorization");
            this.m_dictionaryExtraHeaders.Add("Authorization", authorizationToken);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task ConnectToCloud(TwainCloudClient client)
        {
            m_client = client;
            m_applicationmanager = new ApplicationManager(client);
            m_applicationmanager.Received += (sender, e) =>
            {
                Debug.WriteLine(e);
                ApiCmd.CompleteCloudResponse(e);
            };

            await m_applicationmanager.Connect();
        }

        public TwainCloudClient GetCloudClient()
        {
            return m_client;
        }

        public void SetCloudClient(TwainCloudClient client)
        {
            m_client = client;
        }


        /// <summary>
        /// Get info about the device...
        /// </summary>
        /// <param name="a_apicmd">info about the command</param>
        /// <param name="a_szOverride">used for certification testing</param>
        /// <returns>true on success</returns>
        public CloudResponse ClientInfo
        (
            DnssdDeviceInfo a_dnssddeviceinfo,
            out ApiCmd a_apicmd,
            string a_szOverride = null
        )
        {
            a_apicmd = new ApiCmd(a_dnssddeviceinfo);

            CloudResponse ret = new CloudResponse();
            string szCommand;
            string szFunction = "ClientInfo";

            // This command can be issued at any time, so we don't check state, we also
            // don't have to worry about locking anything...

            // Figure out what command we're sending...
            if (a_szOverride != null)
            {
                szCommand = "/privet/" + a_szOverride;
            }
            else
            {
                szCommand = (Config.Get("useInfoex", "yes") == "yes") ? "/privet/infoex" : "/privet/info";
            }

            // Send the RESTful API command, we'll support using either
            // privet/info or /privet/infoex, but we'll default to infoex...
            ret.blSuccess = ClientHttpRequest
            (
                szFunction,
                ref a_apicmd,
                szCommand,
                "GET",
                ClientHttpBuildHeader(true),
                null,
                null,
                null,
                m_iHttpTimeoutCommand,
                ApiCmd.HttpReplyStyle.SimpleReplyWithSessionInfo
            );
            if (!ret.blSuccess)
            {
                ClientReturnError(a_apicmd, false, "", 0, "");
                return ret;
            }

            // All done...
            return ret;
        }

        /// <summary>
        /// Close a session...
        /// </summary>
        /// <param name="a_apicmd">info about the command</param>
        /// <returns>true on success</returns>
        public CloudResponse ClientScannerCloseSession(DnssdDeviceInfo a_dnssddeviceinfo, out ApiCmd a_apicmd)
        {
            a_apicmd = new ApiCmd(a_dnssddeviceinfo);

            CloudResponse ret = new CloudResponse();
            string szFunction = "ClientScannerCloseSession";

            // Lock this command to protect the session object...
            lock (m_objectLockClientApi)
            {
                string szSessionId = "";

                // Collect session data, if we have any...
                if (m_twainlocalsession != null)
                {
                    a_apicmd.SetClientCommandId(m_twainlocalsession.ClientCreateCommandId());
                    szSessionId = m_twainlocalsession.GetSessionId();
                }

                // Send the RESTful API command...
                ret.blSuccess = ClientHttpRequest
                (
                    szFunction,
                    ref a_apicmd,
                    "/privet/twaindirect/session",
                    "POST",
                    ClientHttpBuildHeader(),
                    "{" +
                    "\"kind\":\"twainlocalscanner\"," +
                    "\"commandId\":\"" + a_apicmd.GetCommandId() + "\"," +
                    "\"method\":\"closeSession\"," +
                    "\"params\":{" +
                    "\"sessionId\":\"" + szSessionId + "\"" +
                    "}" +
                    "}",
                    null,
                    null,
                    m_iHttpTimeoutCommand,
                    ApiCmd.HttpReplyStyle.SimpleReplyWithSessionInfo
                );
                if (!ret.blSuccess)
                {
                    ClientReturnError(a_apicmd, false, "", 0, "");
                    return ret;
                }
            }

            // All done...
            return ret;
        }

        /// <summary>
        /// Create a session, basically seeing if the device is available for use.
        /// If it works out the session state will go to "ready".  Anything else
        /// is going to be an issue...
        /// </summary>
        /// <param name="a_apicmd">info about the command</param>
        /// <returns>true on success</returns>
        public CloudResponse ClientScannerCreateSession(DnssdDeviceInfo a_dnssddeviceinfo, out ApiCmd a_apicmd)
        {
            a_apicmd = new ApiCmd(a_dnssddeviceinfo);

            CloudResponse ret = new CloudResponse();
            bool blCreatedTwainLocalSession = false;
            string szFunction = "ClientScannerCreateSession";

            // Lock this command to protect the session object...
            lock (m_objectLockClientApi)
            {
                // Create it if we need it...
                if (m_twainlocalsession == null)
                {
                    // We got this X-Privet-Token from info or infoex, if we didn't
                    // get one yet, there will be sadness on the scanner side...
                    m_twainlocalsession = new TwainLocalSession(m_szXPrivetToken);
                    blCreatedTwainLocalSession = true;
                }

                // Send the RESTful API command...
                a_apicmd.SetClientCommandId(m_twainlocalsession.ClientCreateCommandId());
                ret.blSuccess = ClientHttpRequest
                (
                    szFunction,
                    ref a_apicmd,
                    "/privet/twaindirect/session",
                    "POST",
                    ClientHttpBuildHeader(),
                    "{" +
                    "\"kind\":\"twainlocalscanner\"," +
                    "\"commandId\":\"" + a_apicmd.GetCommandId() + "\"," +
                    "\"method\":\"createSession\"" +
                    "}",
                    null,
                    null,
                    m_iHttpTimeoutCommand,
                    ApiCmd.HttpReplyStyle.SimpleReplyWithSessionInfo
                );
                if (!ret.blSuccess)
                {
                    ClientReturnError(a_apicmd, false, "", 0, "");
                    if (blCreatedTwainLocalSession)
                    {
                        if (m_twainlocalsession != null)
                        {
                            m_twainlocalsession.SetUserShutdown(false);
                            m_twainlocalsession.Dispose();
                            m_twainlocalsession = null;
                        }
                    }
                    return ret;
                }
            }

            // All done...
            return ret;
        }

        /// <summary>
        /// Get the session information...
        /// </summary>
        /// <param name="a_apicmd">info about the command</param>
        /// <returns>true on success</returns>
        public CloudResponse ClientScannerGetSession(DnssdDeviceInfo a_dnssddeviceinfo, out ApiCmd a_apicmd)
        {
            a_apicmd = new ApiCmd(a_dnssddeviceinfo);

            CloudResponse ret = new CloudResponse();
            string szFunction = "ClientScannerGetSession";

            // Lock this command to protect the session object...
            lock (m_objectLockClientApi)
            {
                string szSessionId = "";

                // Collection session data, if we have any...
                if (m_twainlocalsession != null)
                {
                    a_apicmd.SetClientCommandId(m_twainlocalsession.ClientCreateCommandId());
                    szSessionId = m_twainlocalsession.GetSessionId();
                }

                // Send the RESTful API command...
                ret.blSuccess = ClientHttpRequest
                (
                    szFunction,
                    ref a_apicmd,
                    "/privet/twaindirect/session",
                    "POST",
                    ClientHttpBuildHeader(),
                    "{" +
                    "\"kind\":\"twainlocalscanner\"," +
                    "\"commandId\":\"" + a_apicmd.GetCommandId() + "\"," +
                    "\"method\":\"getSession\"," +
                    "\"params\":{" +
                    "\"sessionId\":\"" + szSessionId + "\"" +
                    "}" +
                    "}",
                    null,
                    null,
                    m_iHttpTimeoutCommand,
                    ApiCmd.HttpReplyStyle.SimpleReplyWithSessionInfo
                );
                if (!ret.blSuccess)
                {
                    ClientReturnError(a_apicmd, false, "", 0, "");
                    return ret;
                }
            }

            // All done...
            return ret;
        }

        /// <summary>
        /// This is an invalid command, it's only used to test certification, please
        /// don't go around adding this to your applications... 
        /// </summary>
        /// <param name="a_apicmd">info about the command</param>
        /// <returns>true on success</returns>
        public CloudResponse ClientScannerInvalidCommand(DnssdDeviceInfo a_dnssddeviceinfo, out ApiCmd a_apicmd)
        {
            a_apicmd = new ApiCmd(a_dnssddeviceinfo);

            CloudResponse ret = new CloudResponse();
            string szFunction = "ClientScannerInvalidCommand";

            // Lock this command to protect the session object...
            lock (m_objectLockClientApi)
            {
                // Send the RESTful API command...
                a_apicmd.SetClientCommandId(System.Guid.NewGuid().ToString());
                ret.blSuccess = ClientHttpRequest
                (
                    szFunction,
                    ref a_apicmd,
                    "/privet/twaindirect/session",
                    "POST",
                    ClientHttpBuildHeader(),
                    "{" +
                    "\"kind\":\"twainlocalscanner\"," +
                    "\"commandId\":\"" + a_apicmd.GetCommandId() + "\"," +
                    "\"method\":\"invalidCommand\"" +
                    "}",
                    null,
                    null,
                    m_iHttpTimeoutCommand,
                    ApiCmd.HttpReplyStyle.SimpleReply
                );
                if (!ret.blSuccess)
                {
                    ClientReturnError(a_apicmd, false, "", 0, "");
                    return ret;
                }
            }

            // All done...
            return ret;
        }

        /// <summary>
        /// This is an invalid uri, it's only used to test certification, please
        /// don't go around adding this to your applications... 
        /// </summary>
        /// <param name="a_apicmd">info about the command</param>
        /// <returns>true on success</returns>
        public CloudResponse ClientScannerInvalidUri(DnssdDeviceInfo a_dnssddeviceinfo, out ApiCmd a_apicmd)
        {
            a_apicmd = new ApiCmd(a_dnssddeviceinfo);

            CloudResponse ret = new CloudResponse();
            string szFunction = "ClientScannerInvalidUri";

            // Lock this command to protect the session object...
            lock (m_objectLockClientApi)
            {
                // Send the RESTful API command...
                ret.blSuccess = ClientHttpRequest
                (
                    szFunction,
                    ref a_apicmd,
                    "/privet/twaindirect/invaliduri",
                    "GET",
                    ClientHttpBuildHeader(true),
                    null,
                    null,
                    null,
                    m_iHttpTimeoutCommand,
                    ApiCmd.HttpReplyStyle.SimpleReply
                );
                if (!ret.blSuccess)
                {
                    ClientReturnError(a_apicmd, false, "", 0, "");
                    return ret;
                }
            }

            // All done...
            return ret;
        }

        /// <summary>
        /// Read an image block's TWAIN Direct metadata from the scanner...
        /// </summary>
        /// <param name="a_lImageBlockNum">image block to read</param>
        /// <param name="a_blGetThumbnail">the caller would like a thumbnail</param>
        /// <param name="a_apicmd">info about the command</param>
        /// <returns>true on success</returns>
        public CloudResponse ClientScannerReadImageBlockMetadata(long a_lImageBlockNum, bool a_blGetThumbnail, 
            DnssdDeviceInfo a_dnssddeviceinfo, ref ApiCmd a_apicmd)
        {
            if(a_apicmd == null)
                a_apicmd = new ApiCmd(a_dnssddeviceinfo);

            CloudResponse ret = new CloudResponse();
            string szThumbnail;
            string szMetaFile = "(no session)";
            string szFunction = "ClientScannerReadImageBlockMetadata";

            // Lock this command to protect the session object...
            lock (m_objectLockClientApi)
            {
                string szSessionId = "";

                // Collection session data, if we have any...
                if (m_twainlocalsession != null)
                {
                    a_apicmd.SetClientCommandId(m_twainlocalsession.ClientCreateCommandId());
                    szSessionId = m_twainlocalsession.GetSessionId();
                }

                // We're asking for a thumbnail...
                szThumbnail = null;
                if (a_blGetThumbnail)
                {
                    // Build the full image thumbnail path...
                    szThumbnail = Path.Combine(m_szImagesFolder, "img" + a_lImageBlockNum.ToString("D6") + "_thumbnail.tdpdf"); // twain direct temporary pdf

                    // Make sure it's clean...
                    if (File.Exists(szThumbnail))
                    {
                        try
                        {
                            File.Delete(szThumbnail);
                        }
                        catch (Exception exception)
                        {
                            ClientReturnError(a_apicmd, false, "critical", -1, szFunction + ": access denied: " + szThumbnail + " (" + exception.Message + ")");
                            return ret;
                        }
                    }
                }

                // Send the RESTful API command...
                ret.blSuccess = ClientHttpRequest
                (
                    szFunction,
                    ref a_apicmd,
                    "/privet/twaindirect/session",
                    "POST",
                    ClientHttpBuildHeader(),
                    "{" +
                    "\"kind\":\"twainlocalscanner\"," +
                    "\"commandId\":\"" + a_apicmd.GetCommandId() + "\"," +
                    "\"method\":\"readImageBlockMetadata\"," +
                    "\"params\":{" +
                    "\"sessionId\":\"" + szSessionId + "\"," +
                    "\"imageBlockNum\":" + a_lImageBlockNum +
                    (a_blGetThumbnail ? ",\"withThumbnail\":true" : "") +
                    "}" +
                    "}",
                    null,
                    a_blGetThumbnail ? szThumbnail : null,
                    m_iHttpTimeoutCommand,
                    ApiCmd.HttpReplyStyle.SimpleReplyWithSessionInfo
                );
                if (!ret.blSuccess)
                {
                    ClientReturnError(a_apicmd, false, "", 0, "");
                    return ret;
                }

                // Make sure we have a session for this...
                if (m_twainlocalsession != null)
                {
                    // Try to get the meta data...
                    if (string.IsNullOrEmpty(m_twainlocalsession.GetMetadata()))
                    {
                        m_twainlocalsession.SetMetadata(null);
                        ClientReturnError(a_apicmd, false, "critical", -1, szFunction + " 'results.metadata' missing for imageBlock=" + a_lImageBlockNum);
                        if (!string.IsNullOrEmpty(a_apicmd.GetHttpResponseData()))
                        {
                            Log.Error(a_apicmd.GetHttpResponseData());
                        }
                        return ret;
                    }

                    // Save the metadata to a file...
                    szMetaFile = Path.Combine(m_szImagesFolder, "img" + a_lImageBlockNum.ToString("D6") + ".tdmeta");
                    string szMetadata = "{\"metadata\":" + m_twainlocalsession.GetMetadata() + "}";
                    try
                    {
                        File.WriteAllText(szMetaFile, szMetadata);
                    }
                    catch (Exception exception)
                    {
                        m_twainlocalsession.SetMetadata(null);
                        ClientReturnError(a_apicmd, false, "critical", -1, szFunction + " access denied: " + szMetaFile + " (" + exception.Message + ")");
                        return ret;
                    }
                }
            }

            // All done...
            Log.Info("metadata:  " + szMetaFile);
            if (a_blGetThumbnail)
            {
                Log.Info("thumbnail: " + szThumbnail);
            }
            return ret;
        }

        /// <summary>
        /// Release one or more image blocks
        /// </summary>
        /// <param name="a_lImageBlockNum">first block to release</param>
        /// <param name="a_lLastImageBlockNum">last block in range (inclusive)</param>
        /// <param name="a_apicmd">info about the command</param>
        /// <returns></returns>
        public CloudResponse ClientScannerReleaseImageBlocks(long a_lImageBlockNum, long a_lLastImageBlockNum, 
            DnssdDeviceInfo a_dnssddeviceinfo, ref ApiCmd a_apicmd)
        {
            if(a_apicmd == null)
                a_apicmd = new ApiCmd(a_dnssddeviceinfo);

            CloudResponse ret = new CloudResponse();
            string szFunction = "ClientScannerReleaseImageBlocks";

            // Lock this command to protect the session object...
            lock (m_objectLockClientApi)
            {
                string szSessionId = "";

                // Collection session data, if we have any...
                if (m_twainlocalsession != null)
                {
                    a_apicmd.SetClientCommandId(m_twainlocalsession.ClientCreateCommandId());
                    szSessionId = m_twainlocalsession.GetSessionId();
                }

                // Send the RESTful API command...
                ret.blSuccess = ClientHttpRequest
                (
                    szFunction,
                    ref a_apicmd,
                    "/privet/twaindirect/session",
                    "POST",
                    ClientHttpBuildHeader(),
                    "{" +
                    "\"kind\":\"twainlocalscanner\"," +
                    "\"commandId\":\"" + a_apicmd.GetCommandId() + "\"," +
                    "\"method\":\"releaseImageBlocks\"," +
                    "\"params\":{" +
                    "\"sessionId\":\"" + szSessionId + "\"," +
                    "\"imageBlockNum\":" + a_lImageBlockNum + "," +
                    "\"lastImageBlockNum\":" + a_lLastImageBlockNum +
                    "}" +
                    "}",
                    null,
                    null,
                    m_iHttpTimeoutCommand,
                    ApiCmd.HttpReplyStyle.SimpleReplyWithSessionInfo
                );
                if (!ret.blSuccess)
                {
                    ClientReturnError(a_apicmd, false, "", 0, "");
                    return ret;
                }
            }

            // All done...
            return ret;
        }

        /// <summary>
        /// Send a task to the scanner...
        /// </summary>
        /// <param name="a_szTask">the task to use</param>
        /// <param name="a_apicmd">info about the command</param>
        /// <returns>true on success</returns>
        public CloudResponse ClientScannerSendTask(string a_szTask, DnssdDeviceInfo a_dnssddeviceinfo, out ApiCmd a_apicmd)
        {
            a_apicmd = new ApiCmd(a_dnssddeviceinfo);

            CloudResponse ret = new CloudResponse();
            string szFunction = "ClientScannerSendTask";

            // Lock this command to protect the session object...
            lock (m_objectLockClientApi)
            {
                string szSessionId = "";

                // Collect session data, if we have any...
                if (m_twainlocalsession != null)
                {
                    a_apicmd.SetClientCommandId(m_twainlocalsession.ClientCreateCommandId());
                    szSessionId = m_twainlocalsession.GetSessionId();
                }

                // Send the RESTful API command...
                ret.blSuccess = ClientHttpRequest
                (
                    szFunction,
                    ref a_apicmd,
                    "/privet/twaindirect/session",
                    "POST",
                    ClientHttpBuildHeader(),
                    "{" +
                    "\"kind\":\"twainlocalscanner\"," +
                    "\"commandId\":\"" + a_apicmd.GetCommandId() + "\"," +
                    "\"method\":\"sendTask\"," +
                    "\"params\":{" +
                    "\"sessionId\":\"" + szSessionId + "\"," +
                    "\"task\":" + a_szTask +
                    "}" +
                    "}",
                    null,
                    null,
                    m_iHttpTimeoutCommand,
                    ApiCmd.HttpReplyStyle.SimpleReplyWithSessionInfo
                );
                if (!ret.blSuccess)
                {
                    ClientReturnError(a_apicmd, false, "", 0, "");
                    return ret;
                }
            }

            // All done...
            return ret;
        }

        /// <summary>
        /// Start capturing...
        /// </summary>
        /// <param name="a_apicmd">info about the command</param>
        /// <returns>true on success</returns>
        public CloudResponse ClientScannerStartCapturing(DnssdDeviceInfo a_dnssddeviceinfo, out ApiCmd a_apicmd)
        {
            a_apicmd = new ApiCmd(a_dnssddeviceinfo);

            CloudResponse ret = new CloudResponse();
            string szFunction = "ClientScannerStartCapturing";

            // Lock this command to protect the session object...
            lock (m_objectLockClientApi)
            {
                string szSessionId = "";

                // Init stuff...
                m_iLastImageNumberFromPreviousStartCapturing += m_iLastImageNumberSinceCurrentStartCapturing;
                m_iLastImageNumberSinceCurrentStartCapturing = 0;

                // Collect session data, if we have any...
                if (m_twainlocalsession != null)
                {
                    a_apicmd.SetClientCommandId(m_twainlocalsession.ClientCreateCommandId());
                    szSessionId = m_twainlocalsession.GetSessionId();
                    m_twainlocalsession.SetSessionStatusSuccess(true);
                    m_twainlocalsession.SetSessionStatusDetected("nominal");
                    m_twainlocalsession.SetSessionDoneCapturing(false);
                    m_twainlocalsession.SetSessionImageBlocksDrained(false);
                }

                // Send the RESTful API command...
                ret.blSuccess = ClientHttpRequest
                (
                    szFunction,
                    ref a_apicmd,
                    "/privet/twaindirect/session",
                    "POST",
                    ClientHttpBuildHeader(),
                    "{" +
                    "\"kind\":\"twainlocalscanner\"," +
                    "\"commandId\":\"" + a_apicmd.GetCommandId() + "\"," +
                    "\"method\":\"startCapturing\"," +
                    "\"params\":{" +
                    "\"sessionId\":\"" + szSessionId + "\"" +
                    "}" +
                    "}",
                    null,
                    null,
                    m_iHttpTimeoutCommand,
                    ApiCmd.HttpReplyStyle.SimpleReplyWithSessionInfo
                );
                if (!ret.blSuccess)
                {
                    ClientReturnError(a_apicmd, false, "", 0, "");
                    if (m_twainlocalsession != null)
                    {
                        m_twainlocalsession.SetSessionDoneCapturing(true);
                        m_twainlocalsession.SetSessionImageBlocksDrained(true);
                    }
                    return ret;
                }
            }

            // All done...
            return ret;
        }

        /// <summary>
        /// Stop capturing...
        /// </summary>
        /// <param name="a_apicmd">info about the command</param>
        /// <returns>true on success</returns>
        public CloudResponse ClientScannerStopCapturing(DnssdDeviceInfo a_dnssddeviceinfo, out ApiCmd a_apicmd)
        {
            a_apicmd = new ApiCmd(a_dnssddeviceinfo);

            CloudResponse ret = new CloudResponse();
            string szFunction = "ClientScannerStopCapturing";

            // Lock this command to protect the session object...
            lock (m_objectLockClientApi)
            {
                string szSessionId = "";

                // Collection session data, if we have any...
                if (m_twainlocalsession != null)
                {
                    a_apicmd.SetClientCommandId(m_twainlocalsession.ClientCreateCommandId());
                    szSessionId = m_twainlocalsession.GetSessionId();
                }

                // Send the RESTful API command...
                ret.blSuccess = ClientHttpRequest
                (
                    szFunction,
                    ref a_apicmd,
                    "/privet/twaindirect/session",
                    "POST",
                    ClientHttpBuildHeader(),
                    "{" +
                    "\"kind\":\"twainlocalscanner\"," +
                    "\"commandId\":\"" + a_apicmd.GetCommandId() + "\"," +
                    "\"method\":\"stopCapturing\"," +
                    "\"params\":{" +
                    "\"sessionId\":\"" + szSessionId + "\"" +
                    "}" +
                    "}",
                    null,
                    null,
                    m_iHttpTimeoutCommand,
                    ApiCmd.HttpReplyStyle.SimpleReplyWithSessionInfo
                );
                if (!ret.blSuccess)
                {
                    ClientReturnError(a_apicmd, false, "", 0, "");
                    return ret;
                }
            }

            // All done...
            return ret;
        }

        /// <summary>
        /// Wait for one or more events...
        /// </summary>
        /// <param name="a_apicmd">info about the command</param>
        /// <returns>true on success</returns>
        public CloudResponse ClientScannerWaitForEvents(DnssdDeviceInfo a_dnssddeviceinfo, out ApiCmd a_apicmd)
        {
            a_apicmd = new ApiCmd(a_dnssddeviceinfo);

            CloudResponse ret = new CloudResponse();
            string szFunction = "ClientScannerWaitForEvents";

            // Lock this command to protect the session object...
            lock (m_objectLockClientApi)
            {
                string szSessionId = "";

                // Collection session data, if we have any...
                if (m_twainlocalsession != null)
                {
                    szSessionId = m_twainlocalsession.GetSessionId();
                }

                // Init our event timeout for HTTPS communication, this value
                // needs to be more than whatever is being used by the scanner.
                int iDefault = 60000; // 60 seconds
                int iHttpTimeoutEvent = (int)Config.Get("httpTimeoutEvent", iDefault);
                if (iHttpTimeoutEvent < 10000)
                {
                    iHttpTimeoutEvent = iDefault;
                }

                // Send the RESTful API command...
                // Both @@@COMMANDID@@@ and @@@SESSIONREVISION@@@ are resolved
                // inside of the ClientScannerWaitForEventsCommunicationHelper thread...
                ret.blSuccess = ClientHttpRequest
                (
                    szFunction,
                    ref a_apicmd,
                    "/privet/twaindirect/session",
                    "POST",
                    ClientHttpBuildHeader(),
                    "{" +
                    "\"kind\":\"twainlocalscanner\"," +
                    "\"commandId\":\"@@@COMMANDID@@@\"," +
                    "\"method\":\"waitForEvents\"," +
                    "\"params\":{" +
                    "\"sessionId\":\"" + szSessionId + "\"," +
                    "\"sessionRevision\":@@@SESSIONREVISION@@@" +
                    "}" +
                    "}",
                    null,
                    null,
                    iHttpTimeoutEvent,
                    ApiCmd.HttpReplyStyle.Event
                );
                if (!ret.blSuccess)
                {
                    ClientReturnError(a_apicmd, false, "", 0, "");
                    return ret;
                }
            }

            // All done...
            return ret;
        }

        /// <summary>
        /// Read an image block from the scanner...
        /// </summary>
        /// <param name="a_lImageBlockNum">block number to read</param>
        /// <param name="a_blGetMetadataWithImage">ask for the metadata</param>
        /// <param name="a_scancallback">function to call</param>
        /// <param name="a_apicmd">info about the command</param>
        /// <returns>true on success</returns>
        public CloudResponse ClientScannerReadImageBlock
        (
            long a_lImageBlockNum,
            bool a_blGetMetadataWithImage,
            ScanCallback a_scancallback,
            DnssdDeviceInfo a_dnssddeviceinfo,
            ref ApiCmd a_apicmd
        )
        {
            if(a_apicmd == null)
                a_apicmd = new ApiCmd(a_dnssddeviceinfo);

            CloudResponse ret = new CloudResponse();
            string szImage;
            string szMetaFile;
            string szFunction = "ClientScannerReadImageBlock";

            // Lock this command to protect the session object...
            lock (m_objectLockClientApi)
            {
                string szSessionId = "";

                // Collection session data, if we have any...
                if (m_twainlocalsession != null)
                {
                    a_apicmd.SetClientCommandId(m_twainlocalsession.ClientCreateCommandId());
                    szSessionId = m_twainlocalsession.GetSessionId();
                }

                // Build the full image path...
                szImage = Path.Combine(m_szImagesFolder, "img" + a_lImageBlockNum.ToString("D6") + ".tdpdf"); // TWAIN direct temporary pdf

                // Make sure it's clean...
                if (File.Exists(szImage))
                {
                    try
                    {
                        File.Delete(szImage);
                    }
                    catch
                    {
                        ClientReturnError(a_apicmd, false, "critical", -1, szFunction + ": access denied: " + szImage);
                        return ret;
                    }
                }

                // Send the RESTful API command...
                ret.blSuccess = ClientHttpRequest
                (
                    szFunction,
                    ref a_apicmd,
                    "/privet/twaindirect/session",
                    "POST",
                    ClientHttpBuildHeader(),
                    "{" +
                    "\"kind\":\"twainlocalscanner\"," +
                    "\"commandId\":\"" + a_apicmd.GetCommandId() + "\"," +
                    "\"method\":\"readImageBlock\"," +
                    "\"params\":{" +
                    "\"sessionId\":\"" + szSessionId + "\"," +
                    (a_blGetMetadataWithImage ? "\"withMetadata\":true," : "") +
                    "\"imageBlockNum\":" + a_lImageBlockNum +
                    "}" +
                    "}",
                    null,
                    szImage,
                    m_iHttpTimeoutData,
                    ApiCmd.HttpReplyStyle.SimpleReplyWithSessionInfo
                );
                if (!ret.blSuccess)
                {
                    ClientReturnError(a_apicmd, false, "", 0, "");
                    return ret;
                }

                // We asked for metadata...
                string szMetadata = "";
                if (a_blGetMetadataWithImage && (m_twainlocalsession != null))
                {
                    // Try to get the meta data...
                    if (string.IsNullOrEmpty(m_twainlocalsession.GetMetadata()))
                    {
                        m_twainlocalsession.SetMetadata(null);
                        ClientReturnError(a_apicmd, false, "critical", -1, szFunction + ": 'results.metadata' missing for imageBlock=" + a_lImageBlockNum);
                        if (!string.IsNullOrEmpty(a_apicmd.GetHttpResponseData()))
                        {
                            Log.Error(a_apicmd.GetHttpResponseData());
                        }
                        return ret;
                    }

                    // Save the metadata to a file...
                    szMetadata = "{\"metadata\":" + m_twainlocalsession.GetMetadata() + "}";
                    szMetaFile = Path.Combine(m_szImagesFolder, "img" + a_lImageBlockNum.ToString("D6") + ".tdmeta");
                    try
                    {
                        File.WriteAllText(szMetaFile, szMetadata);
                    }
                    catch (Exception exception)
                    {
                        m_twainlocalsession.SetMetadata(null);
                        ClientReturnError(a_apicmd, false, "critical", -1, szFunction + " access denied: " + szMetaFile + " (" + exception.Message + ")");
                        return ret;
                    }
                    Log.Info("metadata: " + szMetaFile);
                }

                // If we have a scanner callback, hit it now...
                if (a_scancallback != null)
                {
                    a_scancallback(a_lImageBlockNum);
                }
            }

            // All done...
            Log.Info("image: " + szImage);
            return ret;
        }

    }

    public class CloudResponse
    {
        public string HttpResponse;
        public bool blSuccess;
    }
}