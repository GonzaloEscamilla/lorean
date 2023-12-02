using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Core.CoreGUI
{
    public class ScreenTransitionController : MonoBehaviour, IScreenTransitionService
    {
        [SerializeField] 
        private CanvasGroup fadeCanvas;

        private Tweener tweener;

        private void Awake()
        {
            fadeCanvas.alpha = 1;
        }

        public void Transition(ScreenTransitionType type, Action callback = null)
        {
            switch (type)
            {
                case ScreenTransitionType.In:
                    fadeCanvas.DOFade(0, 2f).OnComplete(() => callback?.Invoke());
                    break;
                case ScreenTransitionType.Out:
                    fadeCanvas.DOFade(1, 2f).OnComplete(() => callback?.Invoke());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}