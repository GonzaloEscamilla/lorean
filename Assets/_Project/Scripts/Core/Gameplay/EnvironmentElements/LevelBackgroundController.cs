using System.Collections.Generic;
using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.Core.Gameplay.EnvironmentElements
{
    public class LevelBackgroundController : MonoBehaviour
    {
        [SerializeField] private TreesLayerController treesLayerController;
        [SerializeField] private MainBackgroundLayerController mainBackgroundLayerController;
        [SerializeField] private InteractableObjectsLayerController interactableObjectsLayerController;
        [SerializeField] private BuildingsLayerController buildingsLayerController;
        
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
        }
        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            buildingsLayerController.CurrentSpeed = _gameSettings.BuildingsLayerSpeed;
            treesLayerController.CurrentSpeed = _gameSettings.TreeLayerSpeed;
            mainBackgroundLayerController.CurrentSpeed = _gameSettings.BackgroundSpeed;
            interactableObjectsLayerController.CurrentSpeed = _gameSettings.BackgroundSpeed;
            
            foreach (var background in _backgroundObjects)
            {
                background.CurrentSpeed = _gameSettings.BackgroundSpeed;
            }
        }
    }
}