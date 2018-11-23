using System;

namespace MQTTnet
{
    public class MqttApplicationMessageReceivedEventArgs : EventArgs
    {
        public MqttApplicationMessageReceivedEventArgs(string clientId, MqttApplicationMessage applicationMessage)
        {
            if (clientId == null) throw new ArgumentNullException(nameof(clientId));
            if (applicationMessage == null) throw new ArgumentNullException(nameof(applicationMessage));

            ClientId = clientId;
            ApplicationMessage = applicationMessage;
        }

        public string ClientId { get; }

        public MqttApplicationMessage ApplicationMessage { get; }
    }
}
