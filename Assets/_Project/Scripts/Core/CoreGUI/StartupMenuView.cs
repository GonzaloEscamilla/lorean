using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.Core.CoreGUI
{
    public class StartupMenuView : GameMenuBase
    {
        private IScreenTransitionService _screenTransitionService;
        
        public override void Initialize()
        {
            _screenTransitionService = Services.Get<IScreenTransitionService>();
            
            Debug.LogWarning("Startup Menu Initialize");
        }

        public override void Show()
        {
            _screenTransitionService.TransitionFinished += Hide;
            _screenTransitionService.Transition(ScreenTransitionType.In);
            Debug.LogWarning("Startup Menu Show");
        }

        public override void Hide()
        {
            _screenTransitionService.TransitionFinished -= Hide;
            
            _screenTransitionService.TransitionFinished += OnFadeOutFinished;
            _screenTransitionService.Transition(ScreenTransitionType.Out);

            Debug.LogWarning("Startup Menu Hide");
        }

        private void OnFadeOutFinished()
        {
            _screenTransitionService.TransitionFinished -= OnFadeOutFinished;
            ActionFinished?.Invoke();
        }
    }
}