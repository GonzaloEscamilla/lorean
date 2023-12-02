using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class OutOfScreenDetector : MonoBehaviour
{
    public event Action OutOfScreen;
    public UnityEvent OnOutOfScreen;
    
    private bool _alreadyOutOfScreen;
    private bool _hasAlreadyEnteredOnce;
    private bool _isOutSide;
    
    private Renderer _spriteRenderer;
    private Camera _mainCamera;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _alreadyOutOfScreen = false;
        _hasAlreadyEnteredOnce = false;
    }

    private void Update()
    {
        CheckIfEnteredOnce();

        if (!_hasAlreadyEnteredOnce || _alreadyOutOfScreen )
        {
            return;
        }

        if (!IsSpriteOutOfScreen()) 
            return;
        
        _alreadyOutOfScreen = true;    
        OutOfScreen?.Invoke();
        OnOutOfScreen.Invoke();
    }
    
    private void CheckIfEnteredOnce()
    {
        if (!IsSpriteOutOfScreen())
        {
            if (!_hasAlreadyEnteredOnce)
            {
                _hasAlreadyEnteredOnce = true;
            }
        }
    }
    
    private bool IsSpriteOutOfScreen()
    {
        if (_spriteRenderer.isVisible)
        {
            // If the sprite is visible, check if it's outside the camera's view
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_mainCamera);
            return !GeometryUtility.TestPlanesAABB(planes, _spriteRenderer.bounds);
        }
        else
        {
            // If the sprite is not visible, consider it out of the screen
            return true;
        }
    }
}