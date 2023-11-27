using _Project.Scripts.Utilities;
using UnityEngine;

namespace _Project.Scripts.Core.CoreGUI
{
    public class HomeMenuView : GameMenuBase
    {
        public override void Show()
        {
            gameObject.SetActive(true);
            _screenTransitionService.Transition(ScreenTransitionType.In);
            _debug.LogWarning("Startup Menu Show");
        }
        
        public override void Hide()
        {
        }
    }
}