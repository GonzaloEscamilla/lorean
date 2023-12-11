using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.GameServices;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Core.Gameplay
{
    public class LevelBackgroundController : MonoBehaviour
    {
        [SerializeField] private BackgroundObject _backgroundPrefab;
        
        [SerializeField] private BackgroundObject _firstBackground;
        [SerializeField] private BackgroundObject _secondBackground;

        [SerializeField] private TreesLayerController treesLayerController;
        
        [SerializeField] 
        private Transform spawnPoint;

        [SerializeField] 
        private BackgroundObject backgroundObjectPrefab;
    
        [FormerlySerializedAs("midGroundLayer")] [SerializeField] 
        private Background midGround;
        
        private ObjectPool<BackgroundObject> _backgroundPool;

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
            _backgroundPool = new ObjectPool<BackgroundObject>(
                () => Instantiate(_backgroundPrefab),
                background => background.Init(),
                background => background.ShutDown(),
                background => Destroy(background.gameObject),
                false,
                10,
                20);
        
            _backgroundObjects.Add(_firstBackground);
            _backgroundObjects.Add(_secondBackground);
        
            _firstBackground.MiddleScreenPositionReached += OnFirstBackgroundMiddleScreenPositionReached;
            _firstBackground.OutOfScreenPositionReached += OnFirstBackgroundOutOfScreenPositionReached;
            _secondBackground.MiddleScreenPositionReached += OnSecondBackgroundMiddleScreenPositionReached;
            _secondBackground.OutOfScreenPositionReached += OnSecondBackgroundOutOfScreenPositionReached;

            _lastSpawnedBackground = _secondBackground;
            

            _isInitialized = true;
        }
        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }
        
            treesLayerController.CurrentSpeed = _gameSettings.TreeLayerSpeed;
            
            midGround.CurrentSpeed = _gameSettings.TreeLayerSpeed;
            
            foreach (var background in _backgroundObjects)
            {
                background.CurrentSpeed = _gameSettings.BackgroundSpeed;
            }
        }
        

        
        private void OnFirstBackgroundMiddleScreenPositionReached(Background obj)
        {
            _firstBackground.MiddleScreenPositionReached -= OnFirstBackgroundMiddleScreenPositionReached;
        }

        private void OnSecondBackgroundMiddleScreenPositionReached(Background obj)
        {
            _secondBackground.MiddleScreenPositionReached -= OnSecondBackgroundMiddleScreenPositionReached;
        
            var newBackground = _backgroundPool.Get();
            newBackground.SnapTo(_lastSpawnedBackground);
        
            newBackground.MiddleScreenPositionReached += OnBackgroundReachesMiddleScreenPosition;
            newBackground.OutOfScreenPositionReached += OnBackgroundOutOfScreenPositionReached;
        
            _backgroundObjects.Add(newBackground);
        
            _lastSpawnedBackground = newBackground;
        }
    
        private void OnFirstBackgroundOutOfScreenPositionReached(Background obj)
        {
            _firstBackground.ShutDown();
            _backgroundObjects.Remove(_firstBackground);
        }
    
        private void OnSecondBackgroundOutOfScreenPositionReached(Background obj)
        {
            _secondBackground.ShutDown();
            _backgroundObjects.Remove(_secondBackground);
        }
    
        private void OnBackgroundReachesMiddleScreenPosition(Background background)
        {
            background.MiddleScreenPositionReached -= OnBackgroundReachesMiddleScreenPosition;
        
            var newBackground = _backgroundPool.Get();
            newBackground.SnapTo(_lastSpawnedBackground);

            newBackground.MiddleScreenPositionReached += OnBackgroundReachesMiddleScreenPosition;
            newBackground.OutOfScreenPositionReached += OnBackgroundOutOfScreenPositionReached;
        
            _backgroundObjects.Add(newBackground);

            _lastSpawnedBackground = newBackground;
        }

        private void OnBackgroundOutOfScreenPositionReached(Background background)
        {
            background.OutOfScreenPositionReached -= OnBackgroundOutOfScreenPositionReached;

            _backgroundPool.Release(background as BackgroundObject);
            _backgroundObjects.Remove(background as BackgroundObject);
        }
    }
}