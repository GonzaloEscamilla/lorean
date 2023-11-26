using UnityEngine;
using UnityEngine.UI;

namespace Core.Menues
{
    public class MainCanvasView : MonoBehaviour, IRootCanvasProvider
    {
        [SerializeField] private Canvas rootCanvas;
        [SerializeField] private CanvasScaler canvasScaler;
        
        public Canvas RootCanvas => rootCanvas;

        private void Awake()
        {
            if (WebGLMobileHelper.IsRunningOnMobile())
            {
                canvasScaler.referenceResolution = new Vector2(1080, 1920);
                headerText.fontSize = 90;
            }
        }
    }
}