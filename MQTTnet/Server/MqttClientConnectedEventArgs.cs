using System;

namespace MQTTnet.Server
{
    public class MqttClientConnectedEventArgs : EventArgs
    {
        public MqttClientConnectedEventArgs(string clientId)
        {
            if (clientId == null) throw new ArgumentNullException(nameof(clientId));
            ClientId = clientId;
        }

        public string ClientId { get; }
    }
}
