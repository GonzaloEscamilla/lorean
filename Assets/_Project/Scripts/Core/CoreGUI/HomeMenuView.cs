using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Core.CoreGUI
{
    public class HomeMenuView : GameMenuBase
    {
        [SerializeField] 
        private Button playButton;
        
        [SerializeField] 
        private Button optionsButton;

        public event Action PlayButtonPressed;
        public event Action OptionsButtonsPressed;

        public override void Initialize()
        {
            base.Initialize();
            playButton.onClick.AddListener(OnPlayButtonClicked);
            optionsButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            playButton.onClick.RemoveListener(OnOptionsButtonClicked);

            Hide();
        }

        private void OnOptionsButtonClicked()
        {
            playButton.onClick.RemoveListener(OnOptionsButtonClicked);
            OptionsButtonsPressed?.Invoke();
        }
        
        public override void Show()
        {
            gameObject.SetActive(true);
            _screenTransitionService.Transition(ScreenTransitionType.In);
            _debug.LogWarning("Startup Menu Show");
        }
        
        public override void Hide()
        {
            _screenTransitionService.Transition(ScreenTransitionType.Out, FinishHide);

            void FinishHide()
            {
                gameObject.SetActive(false);
                PlayButtonPressed?.Invoke();
            }
        }
    }
}