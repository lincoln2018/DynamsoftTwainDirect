using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using System.Web;

namespace Dynamsoft.TwainDirect.Cloud.Events
{
    /// <summary>
    /// TWAIN Cloud MQTT client.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    internal class MqttClient : IDisposable
    {
        #region Private Fields

        private static readonly Encoding DefaultMessageEncoding = Encoding.UTF8;

        private int _reconnectMaxTime = 3;
        protected bool _connected = false;
        private readonly IMqttClient _client;
        private readonly IMqttClientOptions _options;

        #endregion

        #region Ctors

        /// <summary>
        /// Initializes the <see cref="MqttClient"/> class.
        /// </summary>
        static MqttClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MqttClient"/> class.
        /// </summary>
        /// <param name="url">The MQTT broker URL.</param>
        public MqttClient(string mqttUrl)
        {
            // Create a new MQTT client.
            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();


            //Uri uri = new Uri(url);
            //string server = uri.Host;
            //int? port = uri.Port;

            // url => server, port


            // Use WebSocket connection.
            var opt = new MqttClientOptionsBuilder()
                //.WithTcpServer(server, port)
                .WithWebSocketServer(mqttUrl)
                .WithTls()
                .WithClientId("twain-direct-proxy-" + Guid.NewGuid()) // TODO: define this constant somewhere
                .WithCommunicationTimeout(TimeSpan.FromSeconds(5))
                .Build();

            _options = opt;
            _client.ApplicationMessageReceived += MqttMessagePublishReceived;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when new message from MQTT broker is received.
        /// </summary>
        public event EventHandler<MqttMessage> MessageReceived;

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public async void Dispose()
        {
            await _client.DisconnectAsync();
        }

        public bool IsConnected { get { return this._connected; } }

        /// <summary>
        /// Connects to MQTT broker.
        /// </summary>
        /// <returns></returns>
        public async Task Connect()
        {
            _client.Disconnected += async (s, e) =>
            {
                Debug.WriteLine("### DISCONNECTED FROM SERVER ###");

                // TODO: implement exponential backoff instead.
                await Task.Delay(TimeSpan.FromSeconds(2)); 

                try
                {
                    _reconnectMaxTime--;
                    if (_reconnectMaxTime >= 0)
                        await ConnectMqttBroker();
                    else {
                        this._connected = false;
                        Debug.WriteLine("### RECONNECTING MORE THAN 3 TIMES, EXIT ###");
                    }
                }
                catch
                {
                    this._connected = false;
                    Debug.WriteLine("### RECONNECTING FAILED ###");
                }
            };

            await ConnectMqttBroker();
        }

        /// <summary>
        /// Subscribes to the specified topic.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <returns></returns>
        public async Task Subscribe(string topic)
        {
            // '#' is the wildcard to subscribe to anything under the 'root' topic
            // the QOS level here - I only partially understand why it has to be this level - it didn't seem to work at anything else.
            await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic(topic).Build());
        }

        /// <summary>
        /// Subscribes to the specified topic.
        /// </summary>
        /// <param name="topicFilters">The topic.</param>
        /// <returns></returns>
        public async Task Unsubscribe(string[] topicFilters)
        {
            // '#' is the wildcard to subscribe to anything under the 'root' topic
            // the QOS level here - I only partially understand why it has to be this level - it didn't seem to work at anything else.
            try {
                await _client.UnsubscribeAsync(topicFilters);
            }
            catch { }
        }

        /// <summary>
        /// Publishes a message to the specified topic.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task Publish(string topic, string message)
        {
            await _client.PublishAsync(new MqttApplicationMessage
            {
                Topic = topic,
                Payload = DefaultMessageEncoding.GetBytes(message),
                QualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce
            });
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Raises the <see cref="MessageReceived" /> event.
        /// </summary>
        /// <param name="message">Message payload.</param>
        protected virtual void OnMessageReceived(MqttMessage message)
        {
            MessageReceived?.Invoke(this, message);
        }

        #endregion

        #region Private Methods

        private async Task ConnectMqttBroker()
        {
            // A wild hack to ensure that HTTP connection is not closed.
            // See https://github.com/chkr1011/MQTTnet/issues/158 for details

            var defaultIdleTime = ServicePointManager.MaxServicePointIdleTime;
            ServicePointManager.MaxServicePointIdleTime = Timeout.Infinite;
            await _client.ConnectAsync(_options);

            this._connected = true;
            ServicePointManager.MaxServicePointIdleTime = defaultIdleTime;
        }
        
        private void MqttMessagePublishReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            OnMessageReceived(new MqttMessage { Topic = e.ApplicationMessage.Topic, Message = DefaultMessageEncoding.GetString(e.ApplicationMessage.Payload) });
        }

        #endregion        
    }
}
