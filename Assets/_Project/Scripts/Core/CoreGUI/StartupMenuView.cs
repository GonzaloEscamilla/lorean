using UnityEngine;

namespace _Project.Scripts.Core.CoreGUI
{
    public class StartupMenuView : GameMenuBase
    {
        public override void Initialize()
        {
            Debug.LogWarning("Startup Menu Initialize");
        }

        public override void Show()
        {
            Debug.LogWarning("Startup Menu Show");
        }

        public override void Hide()
        {
            Debug.LogWarning("Startup Menu Hide");
        }
    }
}