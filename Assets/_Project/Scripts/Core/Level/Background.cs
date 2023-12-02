using _Project.Scripts.Core;
using _Project.Scripts.GameServices;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Background : MonoBehaviour
{
    private const string TEXTURE_PROPERTY_NAME = "_MainTex";
    
    private GameSettings _gameSettings;
    private Renderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        Services.WaitFor<IGameSettingsProvider>(SetGameSettings);
    }

    private void SetGameSettings(IGameSettingsProvider provider)
    {
        _gameSettings = provider.GameSettings;
    }

    void Update()
    {
        if (_gameSettings == null)
        {
            return;
        }
        
        Move();
    }

    private void Move()
    {
        _spriteRenderer.material.SetTextureOffset(TEXTURE_PROPERTY_NAME,  Vector2.right * (_gameSettings.BackgroundSpeed * Time.time));
    }
}