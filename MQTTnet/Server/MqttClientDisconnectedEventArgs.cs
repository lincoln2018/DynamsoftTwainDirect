using System;

namespace MQTTnet.Server
{
    public class MqttClientDisconnectedEventArgs : EventArgs
    {
        public MqttClientDisconnectedEventArgs(string clientId, bool wasCleanDisconnect)
        {
            if (clientId == null) throw new ArgumentNullException(nameof(clientId));
            ClientId = clientId;
            WasCleanDisconnect = wasCleanDisconnect;
        }
        
        public string ClientId { get; }

        public bool WasCleanDisconnect { get; }
    }
}
