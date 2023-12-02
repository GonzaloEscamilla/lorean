using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Core;
using _Project.Scripts.GameServices;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class LevelBackgroundController : MonoBehaviour
{
    [SerializeField] 
    private Transform spawnPoint;

    [SerializeField] 
    private BackgroundObject backgroundObjectPrefab;
    
    [SerializeField] 
    private BackgroundLayer _treesLayer;
    
    private ObjectPool<BackgroundObject> _backgroundsPool;

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
        
        _backgroundsPool = new ObjectPool<BackgroundObject>(
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
        
        UpdateTreeLayer();
    }

    /// <summary>
    /// Any object that moves righ behind the wall.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void UpdateTreeLayer()
    {
        _treesLayer.CurrentSpeed = _gameSettings.TreeLayerSpeed;
    }

    private IEnumerator SpawningTrees()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2.5f, 5f));
            
            var newBackgroundObject = _backgroundsPool.Get();
            newBackgroundObject.transform.position = spawnPoint.position;
            newBackgroundObject.OutOfScreen += OnOutOfScreen;
            
            newBackgroundObject.transform.SetParent(_treesLayer.transform);
        }
    }

    private void OnOutOfScreen(BackgroundObject objectOutOfScreen)
    {
        objectOutOfScreen.OutOfScreen -= OnOutOfScreen;

        _backgroundsPool.Release(objectOutOfScreen);
    }
}