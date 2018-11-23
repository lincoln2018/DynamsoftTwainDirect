using System;

namespace MQTTnet.Diagnostics
{
    public class MqttNetLogMessagePublishedEventArgs : EventArgs
    {
        public MqttNetLogMessagePublishedEventArgs(MqttNetLogMessage logMessage)
        {
            if (logMessage == null) throw new ArgumentNullException(nameof(logMessage));
            TraceMessage = logMessage;
        }

        public MqttNetLogMessage TraceMessage { get; }
    }
}
