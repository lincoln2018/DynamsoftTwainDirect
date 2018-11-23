using System;
using MQTTnet.Adapter;
using MQTTnet.Client;
using MQTTnet.Diagnostics;
using MQTTnet.Serializer;

namespace MQTTnet.Implementations
{
    public class MqttClientAdapterFactory : IMqttClientAdapterFactory
    {
        public IMqttChannelAdapter CreateClientAdapter(IMqttClientOptions options, IMqttNetChildLogger logger)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            var serializer = new MqttPacketSerializer { ProtocolVersion = options.ProtocolVersion };

            IMqttClientChannelOptions opts = options.ChannelOptions;

            if (opts is MqttClientTcpOptions)
            {
                return new MqttChannelAdapter(new MqttTcpChannel(options), serializer, logger);
            }
            else if (opts is MqttClientWebSocketOptions)
            {
                var webSocketOptions = opts as MqttClientWebSocketOptions;
                return new MqttChannelAdapter(new MqttWebSocketChannel(webSocketOptions), serializer, logger);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
