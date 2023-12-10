using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.GameServices;
using _Project.Scripts.Core.Gameplay;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Core.Gameplay
{
    public class LevelBackgroundController : MonoBehaviour
    {
        [SerializeField] private BackgroundObject _backgroundPrefab;
        
        [SerializeField] private BackgroundObject _firstBackground;
        [SerializeField] private BackgroundObject _secondBackground;

        [SerializeField] 
        private Transform spawnPoint;

        [SerializeField] 
        private BackgroundObject backgroundObjectPrefab;
    
        [SerializeField] 
        private BackgroundLayer _treesLayer;

        [SerializeField] 
        private BackgroundLayer midGroundLayer;
        
        private ObjectPool<BackgroundObject> _treesPool;
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
            _treesPool = new ObjectPool<BackgroundObject>(
                () =>
                {
                    return Instantiate(backgroundObjectPrefab);
                },
                background =>
                {
                    background.gameObject.SetActive(true);
                },
                background =>
                {
                    background.gameObject.SetActive(false);
                },
                background =>
                {
                    Destroy(background);
                },
                false,
                5,
                20
            );
            
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
        
            _firstBackground.EndPositionReached += OnFirstBackgroundEndPositionReached;
            _firstBackground.OutOfScreenPositionReached += OnFirstBackgroundOutOfScreenPositionReached;
            _secondBackground.EndPositionReached += OnSecondBackgroundEndPositionReached;
            _secondBackground.OutOfScreenPositionReached += OnSecondBackgroundOutOfScreenPositionReached;

            _lastSpawnedBackground = _secondBackground;
            
            StartCoroutine(SpawningTrees());

            _isInitialized = true;
        }
        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }
        
            _treesLayer.CurrentSpeed = _gameSettings.TreeLayerSpeed;
            midGroundLayer.CurrentSpeed = _gameSettings.TreeLayerSpeed;

            foreach (var background in _backgroundObjects)
            {
                background.Move(_gameSettings.BackgroundSpeed);
            }
        }
        
        private IEnumerator SpawningTrees()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(2.5f, 5f));
            
                var newBackgroundObject = _treesPool.Get();
                newBackgroundObject.transform.position = spawnPoint.position;
                newBackgroundObject.EndPositionReached += EndPositionReached;
            
                newBackgroundObject.transform.SetParent(_treesLayer.transform);
            }
        }

        private void EndPositionReached(BackgroundObject objectOutOfScreen)
        {
            objectOutOfScreen.EndPositionReached -= EndPositionReached;

            _treesPool.Release(objectOutOfScreen);
        }
        
        private void OnFirstBackgroundEndPositionReached(BackgroundObject obj)
        {
            _firstBackground.EndPositionReached -= OnFirstBackgroundEndPositionReached;
        }

        private void OnSecondBackgroundEndPositionReached(BackgroundObject obj)
        {
            _secondBackground.EndPositionReached -= OnSecondBackgroundEndPositionReached;
        
            var newBackground = _backgroundPool.Get();
            newBackground.SnapTo(_lastSpawnedBackground);
        
            newBackground.EndPositionReached += OnBackgroundReachesEndPosition;
            newBackground.OutOfScreenPositionReached += OnBackgroundOutOfScreenPositionReached;
        
            _backgroundObjects.Add(newBackground);
        
            _lastSpawnedBackground = newBackground;
        }
    
        private void OnFirstBackgroundOutOfScreenPositionReached(BackgroundObject obj)
        {
            _firstBackground.ShutDown();
            _backgroundObjects.Remove(_firstBackground);
        }
    
        private void OnSecondBackgroundOutOfScreenPositionReached(BackgroundObject obj)
        {
            _secondBackground.ShutDown();
            _backgroundObjects.Remove(_secondBackground);
        }
    
        private void OnBackgroundReachesEndPosition(BackgroundObject background)
        {
            background.EndPositionReached -= OnBackgroundReachesEndPosition;
        
            var newBackground = _backgroundPool.Get();
            newBackground.SnapTo(_lastSpawnedBackground);

            newBackground.EndPositionReached += OnBackgroundReachesEndPosition;
            newBackground.OutOfScreenPositionReached += OnBackgroundOutOfScreenPositionReached;
        
            _backgroundObjects.Add(newBackground);

            _lastSpawnedBackground = newBackground;
        }

        private void OnBackgroundOutOfScreenPositionReached(BackgroundObject background)
        {
            background.OutOfScreenPositionReached -= OnBackgroundOutOfScreenPositionReached;

            _backgroundPool.Release(background);
            _backgroundObjects.Remove(background);
        }
    }
}