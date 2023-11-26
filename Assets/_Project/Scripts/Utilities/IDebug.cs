using UnityEngine;

namespace _Project.Scripts.Utilities
{
    public interface IDebug
    {
        public void Log(object message, Object sender = null);
        public void LogWarning(object message, Object sender = null);
        public string Prefix { get; set; }
        public Color PrefixColor { get; set; }
    }
}