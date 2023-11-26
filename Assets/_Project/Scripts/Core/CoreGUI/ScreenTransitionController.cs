using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Core.CoreGUI
{
    public class ScreenTransitionController : MonoBehaviour, IScreenTransitionService
    {
        [SerializeField] 
        private CanvasGroup fadeCanvas;

        public event Action TransitionFinished;

        public void Transition(ScreenTransitionType type)
        {
            switch (type)
            {
                case ScreenTransitionType.In:
                    fadeCanvas.DOFade(1, 1f).OnComplete(OnFadeFinished);
                    break;
                case ScreenTransitionType.Out:
                    fadeCanvas.DOFade(0, 1f).OnComplete(OnFadeFinished);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void OnFadeFinished()
        {
            Debug.Log("Transition Finished");
            TransitionFinished?.Invoke();
        }
    }
}