using System.Collections.Generic;
using _Project.Scripts.GameServices;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Core.Gameplay.EnvironmentElements
{
    public abstract class BackgroundLayer : MonoBehaviour
    {
        [SerializeField] protected GameSortingLayer _gameSortingLayer;
        [SerializeField] protected GameObject _backgroundObjectPrefab;
        
        public abstract float CurrentSpeed { get; set; }
        protected abstract ObjectPool<BackgroundObject> _objectPool { get; set; }
        protected List<BackgroundObject> _activeBackgrounds = new ();
        protected GameSettings _gameSettings;
        protected IPlaygroundProvider _playgroundProvider;
        
        private void Awake()
        {
            Services.WaitFor<IGameSettingsProvider>(SetGameSettings);
            Services.WaitFor<IPlaygroundProvider>(SetPlaygroundProvider);
        }

        private void SetPlaygroundProvider(IPlaygroundProvider playgrounProvider)
        {
            _playgroundProvider = playgrounProvider;
        }

        private void SetGameSettings(IGameSettingsProvider settingsProvider)
        {
            _gameSettings = settingsProvider.GameSettings;
            
            Init();
        }

        protected abstract void Init();
    }
}