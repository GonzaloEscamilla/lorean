using System.Collections;
using _Project.Scripts.GameServices;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Core.Gameplay
{
    public class LevelBackgroundController : MonoBehaviour
    {
        [SerializeField] 
        private Transform spawnPoint;

        [SerializeField] 
        private BackgroundObject backgroundObjectPrefab;
    
        [SerializeField] 
        private BackgroundLayer _treesLayer;

        [SerializeField] 
        private BackgroundLayer midGroundLayer;
        
        private ObjectPool<BackgroundObject> _treesPool;

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
            midGroundLayer.CurrentSpeed = _gameSettings.TreeLayerSpeed; //Note: Define its own setting.
        }
        
        private IEnumerator SpawningTrees()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(2.5f, 5f));
            
                var newBackgroundObject = _treesPool.Get();
                newBackgroundObject.transform.position = spawnPoint.position;
                newBackgroundObject.OutOfScreen += OnOutOfScreen;
            
                newBackgroundObject.transform.SetParent(_treesLayer.transform);
            }
        }

        private void OnOutOfScreen(BackgroundObject objectOutOfScreen)
        {
            objectOutOfScreen.OutOfScreen -= OnOutOfScreen;

            _treesPool.Release(objectOutOfScreen);
        }
    }
}