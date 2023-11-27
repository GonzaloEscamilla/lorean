using System;
using _Project.Scripts.GameServices;
using _Project.Scripts.Utilities;
using UnityEngine;

namespace _Project.Scripts.Core.CoreGUI
{
    public abstract class GameMenuBase: MonoBehaviour
    {
        public Action ActionFinished;
        protected IScreenTransitionService _screenTransitionService;
        protected IDebug _debug;

        /// <summary>
        /// Initialize is always executed when retrieved from menu provider.
        /// </summary>
        public virtual void Initialize()
        {
            _screenTransitionService = Services.Get<IScreenTransitionService>();
            _debug = Services.Get<IDebug>();
        }
        public abstract void Show();
        public abstract void Hide();
    }
}