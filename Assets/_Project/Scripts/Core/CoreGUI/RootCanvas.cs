using UnityEngine;

namespace _Project.Scripts.Core.CoreGUI
{
    public class RootCanvas : MonoBehaviour, IRootCanvasProvider
    {
        [SerializeField] private Canvas canvas;
        public Canvas Canvas => canvas;
    }
}
