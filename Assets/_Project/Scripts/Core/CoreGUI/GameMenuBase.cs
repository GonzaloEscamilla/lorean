using System;
using UnityEngine;

namespace _Project.Scripts.Core.CoreGUI
{
    public abstract class GameMenuBase: MonoBehaviour
    {
        public Action ActionFinished;

        /// <summary>
        /// Initialize is always executed when retrieved from menu provider.
        /// </summary>
        public abstract void Initialize();
        
        public abstract void Show();
        public abstract void Hide();
    }
}