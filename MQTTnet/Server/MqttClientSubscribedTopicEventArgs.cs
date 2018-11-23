using System;

namespace MQTTnet.Server
{
    public class MqttClientSubscribedTopicEventArgs : EventArgs
    {
        public MqttClientSubscribedTopicEventArgs(string clientId, TopicFilter topicFilter)
        {
            if (clientId == null) throw new ArgumentNullException(nameof(clientId));
            if (topicFilter == null) throw new ArgumentNullException(nameof(topicFilter));

            ClientId = clientId;
            TopicFilter = topicFilter;
        }

        public string ClientId { get; }

        public TopicFilter TopicFilter { get; }
    }
}
