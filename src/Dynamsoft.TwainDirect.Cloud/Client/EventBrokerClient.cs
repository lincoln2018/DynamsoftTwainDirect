using System;
using System.Threading.Tasks;
using Dynamsoft.TwainDirect.Cloud.Events;
using System.Diagnostics;

namespace Dynamsoft.TwainDirect.Cloud.Client
{
    public abstract class EventBrokerClient: IDisposable
    {
        private MqttEventClient _mqttClient;

        #region Events

        /// <summary>
        /// Occurs when asynchronious message from TWAIN Cloud is received.
        /// </summary>
        public event EventHandler<string> Received;

        #endregion

        public async Task Connect(string url, bool bClient)
        {
            _mqttClient = new MqttEventClient(url, bClient);
            _mqttClient.MessageReceived += (_, message) => {
                if (_mqttClient.IsConnected)
                {
                    OnReceived(message.Message);
                }
            };

            await _mqttClient.Connect();
        }

        public void Dispose()
        {
            this.RemoveEvents(this.Received);

            _mqttClient?.Dispose();
        }

        private void RemoveEvents<T>(EventHandler<T> evts)
        {
            Debug.WriteLine("RemoveEvents in EventBrokerClient [Mqtt]");
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
        /// Sends the specified message to the TWAIN Cloud.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">If the session is in disconnected state.</exception>
        public async Task Send(string topic, string message)
        {
            if(ValidateState())
                await _mqttClient.Publish(topic, message);
        }

        public async Task Subscribe(string topic)
        {
            if (ValidateState()) {
                await _mqttClient.Subscribe(topic);
            }
        }

        public async Task Unsubscribe(string [] topicFilters)
        {
            if (ValidateState())
            {
                await _mqttClient.Unsubscribe(topicFilters);
            }
        }



        #region Protected Methods

        /// <summary>
        /// Raises the <see cref="Received" /> event.
        /// </summary>
        /// <param name="message">The received message.</param>
        protected virtual void OnReceived(string message)
        {
            Received?.Invoke(this, message);
        }

        #endregion

        private bool ValidateState()
        {
            if (_mqttClient == null || !_mqttClient.IsConnected) {
                // throw new InvalidOperationException("Broker is in disconnected state. Call Connect first.");

                return false;
            }

            return true;
        }
    }
}
