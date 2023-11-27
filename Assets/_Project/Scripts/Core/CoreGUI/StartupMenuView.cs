using System;
using _Project.Scripts.GameServices;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Core.CoreGUI
{
    public class StartupMenuView : GameMenuBase
    {
        public override void Show()
        {
            gameObject.SetActive(true);
            _screenTransitionService.Transition(ScreenTransitionType.In, Hide);
            
            _debug.Log("Startup Menu Show");
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

            ActionFinished?.Invoke();

            _debug.Log("Startup Menu Hide");
        }
    }
}