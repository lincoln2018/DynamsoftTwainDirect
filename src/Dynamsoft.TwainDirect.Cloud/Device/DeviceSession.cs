﻿using System;
using System.Threading.Tasks;
using Dynamsoft.TwainDirect.Cloud.Client;
using Dynamsoft.TwainDirect.Cloud.Telemetry;

namespace Dynamsoft.TwainDirect.Cloud.Device
{
    /// <summary>
    /// Class that handles connection to TWAIN Cloud infrastructure from device side of things.
    /// </summary>
    public class DeviceSession: EventBrokerClient
    {
        #region Private Fields
        private static Logger Logger = Logger.GetLogger<DeviceSession>();

        private readonly TwainCloudClient _client;
        private readonly string _scannerId;
        private string _cloudTopicName = null;

        #endregion

        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceSession"/> class.
        /// </summary>
        /// <param name="client">Initialized TWAIN Cloud client.</param>
        /// <param name="scannerId">Initialized TWAIN Cloud client.</param>
        public DeviceSession(TwainCloudClient client, string scannerId)
        {
            _client = client;
            _scannerId = scannerId;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Connects specified scanner to TWAIN Cloud infrastructure.
        /// </summary>
        /// <returns></returns>
        public async Task Connect()
        {
            using (Logger.StartActivity("Connecting to cloud infrastructure"))
            {
                var scannerInfo = await _client.Get<ScannerStatusResponse>($"scanners/{_scannerId}");
                _cloudTopicName = scannerInfo.ResponseTopic;

                await base.Connect(scannerInfo.Url, false);
                await base.Subscribe(scannerInfo.RequestTopic);
            }
        }

        /// <summary>
        /// Sends the specified message to the TWAIN Cloud.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">If the session is in disconnected state.</exception>
        public async Task Send(string message)
        {
            try {
                if(!String.IsNullOrEmpty(_cloudTopicName))
                    await base.Send(_cloudTopicName, message);
            } catch { }
            
        }

        /// <summary>
        /// Uploads the specified data to the TWAIN Cloud.
        /// </summary>
        /// <param name="data">The data to upload.</param>
        /// <returns>Unique ID of the object stored in the cloud.</returns>
        public async Task<string> UploadBlock(byte[] data)
        {
            var base64 = Convert.ToBase64String(data);
            string mediaType = "application/json";

            return await _client.Post<string>($"scanners/{_scannerId}/blocks", base64, mediaType);
        }

        /// <summary>
        /// Connects specified scanner to TWAIN Cloud infrastructure.
        /// </summary>
        /// <returns></returns>
        public async Task Close()
        {
            await base.Unsubscribe(new string[] { _cloudTopicName });
            _cloudTopicName = null;
        }
        #endregion
    }
}
