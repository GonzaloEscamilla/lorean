using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Core.Gameplay.EnvironmentElements
{
    public class InteractableObjectsLayerController : BackgroundLayer
    {
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
            
            StartCoroutine(SpawningInteractables());
        }

        private IEnumerator SpawningInteractables()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(0.8f, 3.5f));

                var newInteractable = _objectPool.Get() as BasicBackgroundObject;
                
                newInteractable.transform.SetParent(this.transform);
                newInteractable.transform.position = new Vector3(
                    newInteractable.transform.position.x,
                    Random.Range(_playgroundProvider.LowerGameplayLimit, _playgroundProvider.UpperGameplayLimit), 
                    0f);

                newInteractable.UpdateSortingOrder();
                
                _activeBackgrounds.Add(newInteractable);

                newInteractable.OutOfScreenPositionReached += InteractableOutOfScreenPositionReached;
            }
        }
        
        private void InteractableOutOfScreenPositionReached(BackgroundObject outOfScreenInteractable)
        {
            outOfScreenInteractable.MiddleScreenPositionReached -= InteractableOutOfScreenPositionReached;

            _objectPool.Release(outOfScreenInteractable);
            _activeBackgrounds.Remove(outOfScreenInteractable);
        }
    }
}