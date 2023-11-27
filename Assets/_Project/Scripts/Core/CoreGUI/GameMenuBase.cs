using System;
using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.Core.CoreGUI
{
    public abstract class GameMenuBase: MonoBehaviour
    {
        public Action ActionFinished;
        protected IScreenTransitionService _screenTransitionService;
        public GameMenuBase()
        {
            _screenTransitionService = Services.Get<IScreenTransitionService>();
        }
        
        /// <summary>
        /// Initialize is always executed when retrieved from menu provider.
        /// </summary>
        public abstract void Initialize();
        public abstract void Show();
        public abstract void Hide();
    }
}