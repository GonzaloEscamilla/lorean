using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Core.Gameplay.EnvironmentElements
{
    public class TreesLayerController : BackgroundLayer
    {
        [SerializeField] private List<BackgroundObject> previousTrees;
        
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
            
            StartCoroutine(SpawningTrees());

            foreach (var tree in previousTrees)
            {
                _activeBackgrounds.Add(tree);
                tree.OutOfScreenPositionReached += InitialTreeOutOfScreenPositionReached;
            }
        }

        private IEnumerator SpawningTrees()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1.85f, 4f));

                var newTree = _objectPool.Get() as Tree;
                newTree.transform.SetParent(this.transform);
                
                _activeBackgrounds.Add(newTree);

                newTree.OutOfScreenPositionReached += TreeOutOfScreenPositionReached;
            }
        }
        
        private void TreeOutOfScreenPositionReached(BackgroundObject outOfScreenTree)
        {
            outOfScreenTree.MiddleScreenPositionReached -= TreeOutOfScreenPositionReached;

            _objectPool.Release(outOfScreenTree);
            _activeBackgrounds.Remove(outOfScreenTree);
        }
        
        private void InitialTreeOutOfScreenPositionReached(BackgroundObject outOfScreenTree)
        {
            outOfScreenTree.MiddleScreenPositionReached -= InitialTreeOutOfScreenPositionReached;
            _activeBackgrounds.Remove(outOfScreenTree);
            Destroy(outOfScreenTree.gameObject);
        }
        
    }
}