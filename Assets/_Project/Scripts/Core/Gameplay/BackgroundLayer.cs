using System.Collections.Generic;
using _Project.Scripts.GameServices;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Core.Gameplay
{
    public abstract class BackgroundLayer : MonoBehaviour
    {
        [SerializeField] protected GameObject backgroundObjectPrefab;
        
        public abstract float CurrentSpeed { get; set; }
        protected abstract ObjectPool<BackgroundObject> _objectPool { get; set; }
        protected List<BackgroundObject> _activeBackgrounds = new ();
        protected GameSettings _gameSettings;
        
        private void Awake()
        {
            Services.WaitFor<IGameSettingsProvider>(SetGameSettings);
        }

        private void SetGameSettings(IGameSettingsProvider settingsProvider)
        {
            _gameSettings = settingsProvider.GameSettings;
            
            Init();
        }

        protected abstract void Init();
    }
}