using System;
using _Project.Scripts.GameServices;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Core.CoreGUI
{
    public class StartupMenuView : GameMenuBase
    {
        public override void Initialize()
        {
            _screenTransitionService = Services.Get<IScreenTransitionService>();
            
            Debug.LogWarning("Startup Menu Initialize");
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            _screenTransitionService.Transition(ScreenTransitionType.In, Hide);
            Debug.LogWarning("Startup Menu Show");
        }
        
        public override void Hide()
        {
            HoldSplashScreenBeforeHiding().Forget();
        }

        private async UniTaskVoid HoldSplashScreenBeforeHiding()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
            
            _screenTransitionService.Transition(ScreenTransitionType.Out, OnFadeOutFinished);
        }

        private void OnFadeOutFinished()
        {
            gameObject.SetActive(false);
            Debug.Log("Fade out finished");
            ActionFinished?.Invoke();
        }
    }
}