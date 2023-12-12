using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Core.Gameplay
{
    public class MainBackgroundLayerController : BackgroundLayer
    {
        [SerializeField] private BackgroundObject _firstBackground;
        [SerializeField] private BackgroundObject _secondBackground;
        
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
                () => Instantiate(backgroundObjectPrefab).GetComponent<BackgroundObject>(),
                background => background.Init(),
                background => background.ShutDown(),
                background => Destroy(background.gameObject),
                false,
                10,
                20);
            
            _activeBackgrounds.Add(_firstBackground);
            _activeBackgrounds.Add(_secondBackground);
        
            _firstBackground.MiddleScreenPositionReached += OnFirstBackgroundMiddleScreenPositionReached;
            _firstBackground.OutOfScreenPositionReached += OnFirstBackgroundOutOfScreenPositionReached;
            _secondBackground.MiddleScreenPositionReached += OnSecondBackgroundMiddleScreenPositionReached;
            _secondBackground.OutOfScreenPositionReached += OnSecondBackgroundOutOfScreenPositionReached;

            _lastSpawnedBackground = _secondBackground;
        }
        
        private void OnFirstBackgroundMiddleScreenPositionReached(BackgroundObject obj)
        {
            _firstBackground.MiddleScreenPositionReached -= OnFirstBackgroundMiddleScreenPositionReached;
        }

        private void OnSecondBackgroundMiddleScreenPositionReached(BackgroundObject obj)
        {
            _secondBackground.MiddleScreenPositionReached -= OnSecondBackgroundMiddleScreenPositionReached;
        
            var newBackground = _objectPool.Get();
            newBackground.SnapTo(_lastSpawnedBackground);
        
            newBackground.MiddleScreenPositionReached += OnBackgroundReachesMiddleScreenPosition;
            newBackground.OutOfScreenPositionReached += OnBackgroundOutOfScreenPositionReached;
        
            _activeBackgrounds.Add(newBackground);
        
            _lastSpawnedBackground = newBackground;
        }
    
        private void OnFirstBackgroundOutOfScreenPositionReached(BackgroundObject obj)
        {
            _firstBackground.ShutDown();
            _activeBackgrounds.Remove(_firstBackground);
        }
    
        private void OnSecondBackgroundOutOfScreenPositionReached(BackgroundObject obj)
        {
            _secondBackground.ShutDown();
            _activeBackgrounds.Remove(_secondBackground);
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