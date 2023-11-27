using UnityEngine;

namespace _Project.Scripts.Core.CoreGUI
{
    public interface IRootCanvasProvider
    {
        public Canvas Canvas { get; }
        public void AddNewChild(GameObject newChild);
    }
}