using System;

namespace MQTTnet.Server
{
    public class MqttClientUnsubscribedTopicEventArgs : EventArgs
    {
        public MqttClientUnsubscribedTopicEventArgs(string clientId, string topicFilter)
        {
            if (clientId == null) throw new ArgumentNullException(nameof(clientId));
            if (topicFilter == null) throw new ArgumentNullException(nameof(topicFilter));

            ClientId = clientId;
            TopicFilter = topicFilter;
        }

        public string ClientId { get; }

        public string TopicFilter { get; }
    }
}
