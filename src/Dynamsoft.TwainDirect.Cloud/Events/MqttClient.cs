using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using Dynamsoft.TwainDirect.Cloud.Telemetry;

namespace Dynamsoft.TwainDirect.Cloud.Events
{
    /// <summary>
    /// TWAIN Cloud MQTT client.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    internal class MqttEventClient : IDisposable
    {
        #region Private Fields
        private static Logger Logger = Logger.GetLogger<MqttEventClient>();
        private static readonly Encoding DefaultMessageEncoding = Encoding.UTF8;

        private int _reconnectMaxTime = 1;
        protected bool _connected = false;
        private readonly IMqttClient _client;
        private readonly IMqttClientOptions _options;

        #endregion

        #region Ctors

        /// <summary>
        /// Initializes the <see cref="MqttEventClient"/> class.
        /// </summary>
        static MqttEventClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MqttEventClient"/> class.
        /// </summary>
        /// <param name="url">The MQTT broker URL.</param>
        public MqttEventClient(string mqttUrl, bool bClient)
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
                .WithWebSocketServer(mqttUrl)
                .WithClientId("twain-direct-proxy-" + Guid.NewGuid()) // TODO: define this constant somewhere
                .WithTls();

            if (bClient)
            {
                opt.WithCommunicationTimeout(TimeSpan.FromSeconds(30));
            }

            _options = opt.Build();
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
            this.RemoveEvents(this.MessageReceived);
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
                Logger.LogDebug("Disconnected from server");

                // TODO: implement exponential backoff instead.
                await Task.Delay(TimeSpan.FromSeconds(2));

                try
                {
                    _reconnectMaxTime--;
                    if (_reconnectMaxTime >= 0)
                        await ConnectMqttBroker();
                    else
                    {
                        this._connected = false;
                        Debug.WriteLine("### RECONNECTING MORE THAN 1 TIMES, EXIT ###");
                        Logger.LogDebug("Reconnection more than 1 times, exit.");
                    }
                }
                catch
                {
                    this._connected = false;
                    Debug.WriteLine("### RECONNECTING FAILED ###");
                    Logger.LogDebug("Reconnection failed");
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
            using (Logger.StartActivity($"Subscribing to topic: {topic}"))
            {

                // '#' is the wildcard to subscribe to anything under the 'root' topic
                // the QOS level here - I only partially understand why it has to be this level - it didn't seem to work at anything else.
                await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic(topic).Build());
            }

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
            try
            {
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

            using (Logger.StartActivity($"Publishing a message to topic: {topic}"))
            {
                Logger.LogDebug(message);
                await _client.PublishAsync(new MqttApplicationMessage
                {
                    Topic = topic,
                    Payload = DefaultMessageEncoding.GetBytes(message)
                });
            }
        }

        #endregion

        #region Protected Methods
        private void RemoveEvents<T>(EventHandler<T> evts)
        {

            Debug.WriteLine("RemoveEvents in MqttEventClient [Mqtt]");

            if (evts == null)
                return;

            var list = evts.GetInvocationList();
            foreach (var d in list)
            {
                object delObj = d.GetType().GetProperty("Method").GetValue(d, null);
                string funcName = (string)delObj.GetType().GetProperty("Name").GetValue(delObj, null);
                evts -= d as EventHandler<T>;
            }
        }

        /// <summary>
        /// Raises the <see cref="MessageReceived" /> event.
        /// </summary>
        /// <param name="message">Message payload.</param>
        protected virtual void OnMessageReceived(MqttMessage message)
        {
            using (Logger.StartActivity($"Receiving message from topic: {message.Topic}"))
            {
                Logger.LogDebug(message.Message);
                MessageReceived?.Invoke(this, message);
            }
        }

        #endregion

        #region Private Methods

        private async Task ConnectMqttBroker()
        {
            // A wild hack to ensure that HTTP connection is not closed.
            // See https://github.com/chkr1011/MQTTnet/issues/158 for details

            using (Logger.StartActivity("Connecting to broker"))
            {
                // A wild hack to ensure that HTTP connection is not closed.
                // See https://github.com/chkr1011/MQTTnet/issues/158 for details

                var defaultIdleTime = ServicePointManager.MaxServicePointIdleTime;
                ServicePointManager.MaxServicePointIdleTime = Timeout.Infinite;
                await _client.ConnectAsync(_options);

                this._connected = true; 
                ServicePointManager.MaxServicePointIdleTime = defaultIdleTime;
            }
        }

        private void MqttMessagePublishReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            OnMessageReceived(new MqttMessage { Topic = e.ApplicationMessage.Topic, Message = DefaultMessageEncoding.GetString(e.ApplicationMessage.Payload) });
        }

        #endregion        
    }
}
