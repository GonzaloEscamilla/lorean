using System;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float speed;
    
    public event Action<Background> OutOfScreen;
    
    private Camera mainCamera;
    private Renderer spriteRenderer;

    private bool _isOutSide;
    
    private bool _alreadyOutOfScreen;
    private bool _hasAlreadyEnteredOnce;
    
    private void Awake()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        OutOfScreen = null;
        _alreadyOutOfScreen = false;
        _hasAlreadyEnteredOnce = false;
    }

    void Update()
    {
        Move();
        
        CheckIfEnteredOnce();

        if (!_hasAlreadyEnteredOnce || _alreadyOutOfScreen )
        {
            return;
        }

        if (!IsSpriteOutOfScreen()) 
            return;
        
        _alreadyOutOfScreen = true;
        OutOfScreen?.Invoke(this);
    }

    private void Move()
    {
        if (_alreadyOutOfScreen)
        {
            return;
        }
        
        var moveAmount = Vector2.left * (speed * Time.deltaTime);
        transform.Translate(moveAmount);
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
        if (spriteRenderer.isVisible)
        {
            // If the sprite is visible, check if it's outside the camera's view
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
            return !GeometryUtility.TestPlanesAABB(planes, spriteRenderer.bounds);
        }
        else
        {
            // If the sprite is not visible, consider it out of the screen
            return true;
        }
    }
}