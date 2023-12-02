using _Project.Scripts.Utilities.WebGL;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Core.CoreGUI
{
    public class RootCanvasView : MonoBehaviour, IRootCanvasProvider
    {
        [SerializeField] private Canvas rootCanvas;
        [SerializeField] private CanvasScaler canvasScaler;

        public Canvas Canvas => rootCanvas;
        public void AddNewChild(GameObject newChild)
        {
            newChild.transform.SetAsFirstSibling();
        }

        private void Awake()
        {
            if (WebGLMobileHelper.IsRunningOnMobile())
            {
                canvasScaler.referenceResolution = new Vector2(1080, 1920);
            }
        }
    }
}