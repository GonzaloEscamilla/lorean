using System;
using UnityEngine;
using UnityEngine.Pool;

public class LevelBackgroundController : MonoBehaviour
{
    [SerializeField] 
    private Transform spawnPoint;

    [SerializeField] 
    private Background firstBackgroundSprite;
    
    [SerializeField] 
    private Background secondBackgroundSprite;
    
    [SerializeField] 
    private Background backgroundPrefab;

    private ObjectPool<Background> _backgroundsPool;

    private void Start()
    {
        firstBackgroundSprite.OutOfScreen += OnFirstBackgroundOutOfScreen;
        secondBackgroundSprite.OutOfScreen += OnSecondBackgroundOutOfScreen;
        
        _backgroundsPool = new ObjectPool<Background>(
            () =>
            {
                return Instantiate(backgroundPrefab);
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
    }

    private void OnFirstBackgroundOutOfScreen(Background firstBackground)
    {
        firstBackgroundSprite.OutOfScreen -= OnFirstBackgroundOutOfScreen;
        SpawnBackground();
        
        Destroy(firstBackgroundSprite.gameObject);
    }

    private void OnSecondBackgroundOutOfScreen(Background secondBackground)
    {
        secondBackgroundSprite.OutOfScreen -= OnSecondBackgroundOutOfScreen;
        SpawnBackground();
        
        Destroy(secondBackgroundSprite.gameObject);
    }
    
    private void SpawnBackground()
    {
        var newBackground = _backgroundsPool.Get();
        newBackground.OutOfScreen += OnBackgroundOutOfScreen;
        newBackground.transform.position = spawnPoint.transform.position + new Vector3(-0.01f,0,0);
    }

    private void OnBackgroundOutOfScreen(Background background)
    {
        _backgroundsPool.Release(background);
        SpawnBackground();
    }
}