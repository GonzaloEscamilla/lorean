using UnityEngine;

namespace _Project.Scripts.Core.CoreGUI
{
    public class HomeMenuView : GameMenuBase
    {
        public override void Initialize()
        {
            Debug.LogWarning("Home Menu Initialize");
        }

        public override void Show()
        {
            Debug.LogWarning("Home Menu Show");
            throw new System.NotImplementedException();
        }

        public override void Hide()
        {
            Debug.LogWarning("Home Menu Hide");
        }
    }
}