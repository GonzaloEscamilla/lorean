using System.Collections.Generic;

namespace _Project.Scripts.Telemetry
{
    public interface ITelemetrySender
    {
        void Send(string eventID);
        public void Send(string eventID, Dictionary<string, object> eventData);
    }
}