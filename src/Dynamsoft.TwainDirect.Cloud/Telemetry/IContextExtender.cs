namespace Dynamsoft.TwainDirect.Cloud.Telemetry
{
    public interface IContextExtender
    {
        void Extend(TelemetryContext context);
    }
}
