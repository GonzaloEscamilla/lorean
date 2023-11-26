using UnityEngine;

namespace _Project.Scripts.Utilities
{
    public class UnityLogAdapter : ILog
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }
    }
}