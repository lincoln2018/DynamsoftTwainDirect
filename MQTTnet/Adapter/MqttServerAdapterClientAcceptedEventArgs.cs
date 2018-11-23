using System;
using System.Threading.Tasks;

namespace MQTTnet.Adapter
{
    public class MqttServerAdapterClientAcceptedEventArgs : EventArgs
    {
        public MqttServerAdapterClientAcceptedEventArgs(IMqttChannelAdapter client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            Client = client;
        }

        public IMqttChannelAdapter Client { get; }

        public Task SessionTask { get; set; }
    }
}
