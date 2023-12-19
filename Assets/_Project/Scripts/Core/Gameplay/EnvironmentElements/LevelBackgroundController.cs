using System.Collections.Generic;
using UnityEngine;
using _Project.Scripts.GameServices;

namespace _Project.Scripts.Core.Gameplay.EnvironmentElements
{
    public class LevelBackgroundController : MonoBehaviour
    {
        [SerializeField] private TreesLayerController treesLayerController;
        [SerializeField] private MainBackgroundLayerController mainBackgroundLayerController;
        [SerializeField] private InteractableObjectsLayerController interactableObjectsLayerController;
        [SerializeField] private BuildingsLayerController buildingsLayerController;
        [SerializeField] private float currentBaseBackgroundSpeed;

        public float CurrentBaseBackgroundSpeed
        {
            get => currentBaseBackgroundSpeed;
            set
            {
                currentBaseBackgroundSpeed = value;

                if (currentBaseBackgroundSpeed <= 0)
                {
                    currentBaseBackgroundSpeed = 0;
                }
            }
        }
        
        private List<BackgroundObject> _backgroundObjects = new();
        private BackgroundObject _lastSpawnedBackground;

        private GameSettings _gameSettings;
        private bool _isInitialized;
    
        private void Awake()
        {
            Services.WaitFor<IGameSettingsProvider>(SetGameSettings);
        }

        private void SetGameSettings(IGameSettingsProvider settingsProvider)
        {
            _gameSettings = settingsProvider.GameSettings;

            Initialize();
        }

        private void Initialize()
        {
            _isInitialized = true;

            CurrentBaseBackgroundSpeed = _gameSettings.InitialBaseBackgroundSpeed;
        }
        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            buildingsLayerController.CurrentSpeed = CurrentBaseBackgroundSpeed + _gameSettings.BuildingsLayerSpeed;
            treesLayerController.CurrentSpeed = CurrentBaseBackgroundSpeed + _gameSettings.TreeLayerSpeed;
            mainBackgroundLayerController.CurrentSpeed = CurrentBaseBackgroundSpeed + _gameSettings.BackgroundSpeed;
            interactableObjectsLayerController.CurrentSpeed = CurrentBaseBackgroundSpeed +_gameSettings.BackgroundSpeed;
            
            foreach (var background in _backgroundObjects)
            {
                background.CurrentSpeed = _gameSettings.BackgroundSpeed;
            }
        }
    }
}