using _Project.Scripts.Core.CoreGUI;
using _Project.Scripts.Utilities.WebGL;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Menues
{
    public class RootCanvasView : MonoBehaviour, IRootCanvasProvider
    {
        [SerializeField] private Canvas rootCanvas;
        [SerializeField] private CanvasScaler canvasScaler;

        public Canvas Canvas => rootCanvas;
        private void Awake()
        {
            if (WebGLMobileHelper.IsRunningOnMobile())
            {
                canvasScaler.referenceResolution = new Vector2(1080, 1920);
            }
        }
    }
}