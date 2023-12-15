using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Core.Gameplay.EnvironmentElements
{
    public class BuildingsLayerController : BackgroundLayer
    {
        [SerializeField] private BackgroundObject firstBackground;
        [SerializeField] private BackgroundObject secondBackground;
        
        protected float _currentSpeed;
        public override float CurrentSpeed
        {
            get => _currentSpeed;
            set
            {
                _currentSpeed = value;
                foreach (var background in _activeBackgrounds)
                {
                    background.CurrentSpeed = _currentSpeed;
                }
            }
        }
        protected override ObjectPool<BackgroundObject> _objectPool { get; set; }
      
        private BackgroundObject _lastSpawnedBackground;
        
        // TODO: This should support background variations.
        // It's worth noticing that variations might be just an image/sprite swap.
        
        protected override void Init()
        {
            _objectPool =  new ObjectPool<BackgroundObject>(
                () => Instantiate(_backgroundObjectPrefab).GetComponent<BackgroundObject>(),
                background => background.Init(GameSortingLayers.GetSortingLayer(_gameSortingLayer)),
                background => background.ShutDown(),
                background => Destroy(background.gameObject),
                false,
                10,
                20);
            
            _activeBackgrounds.Add(firstBackground);
            _activeBackgrounds.Add(secondBackground);
        
            firstBackground.MiddleScreenPositionReached += OnFirstBackgroundMiddleScreenPositionReached;
            firstBackground.OutOfScreenPositionReached += OnFirstBackgroundOutOfScreenPositionReached;
            secondBackground.MiddleScreenPositionReached += OnSecondBackgroundMiddleScreenPositionReached;
            secondBackground.OutOfScreenPositionReached += OnSecondBackgroundOutOfScreenPositionReached;

            _lastSpawnedBackground = secondBackground;
        }

        private void OnFirstBackgroundMiddleScreenPositionReached(BackgroundObject obj)
        {
            firstBackground.MiddleScreenPositionReached -= OnFirstBackgroundMiddleScreenPositionReached;
        }

        private void OnSecondBackgroundMiddleScreenPositionReached(BackgroundObject obj)
        {
            secondBackground.MiddleScreenPositionReached -= OnSecondBackgroundMiddleScreenPositionReached;
        
            var newBackground = _objectPool.Get();
            newBackground.SnapTo(_lastSpawnedBackground);
        
            newBackground.MiddleScreenPositionReached += OnBackgroundReachesMiddleScreenPosition;
            newBackground.OutOfScreenPositionReached += OnBackgroundOutOfScreenPositionReached;
        
            _activeBackgrounds.Add(newBackground);
        
            _lastSpawnedBackground = newBackground;
        }
    
        private void OnFirstBackgroundOutOfScreenPositionReached(BackgroundObject obj)
        {
            firstBackground.ShutDown();
            _activeBackgrounds.Remove(firstBackground);
        }
    
        private void OnSecondBackgroundOutOfScreenPositionReached(BackgroundObject obj)
        {
            secondBackground.ShutDown();
            _activeBackgrounds.Remove(secondBackground);
        }
    
        private void OnBackgroundReachesMiddleScreenPosition(BackgroundObject backgroundObject)
        {
            backgroundObject.MiddleScreenPositionReached -= OnBackgroundReachesMiddleScreenPosition;
        
            var newBackground = _objectPool.Get();
            newBackground.SnapTo(_lastSpawnedBackground);

            newBackground.MiddleScreenPositionReached += OnBackgroundReachesMiddleScreenPosition;
            newBackground.OutOfScreenPositionReached += OnBackgroundOutOfScreenPositionReached;
        
            _activeBackgrounds.Add(newBackground);

            _lastSpawnedBackground = newBackground;
        }

        private void OnBackgroundOutOfScreenPositionReached(BackgroundObject backgroundObject)
        {
            backgroundObject.OutOfScreenPositionReached -= OnBackgroundOutOfScreenPositionReached;

            _objectPool.Release(backgroundObject as BackgroundObject);
            _activeBackgrounds.Remove(backgroundObject as BackgroundObject);
        }
    }
}