using System;
using _Project.Scripts.GameServices;
using _Project.Scripts.Utilities.Math;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterJumpController : MonoBehaviour
{
    [SerializeField] 
    private Transform shadowTransform;
    
    public event Action JumpFinished;
    
    private GameSettings _settings;
    private Vector2 positionBeforeJump;
    private Vector2 initialShadowScale;
    
    private void Awake()
    {
        Services.WaitFor<IGameSettingsProvider>(SetGameSettings);
    }

    private void SetGameSettings(IGameSettingsProvider provider)
    {
        _settings = provider.GameSettings;
    }

    [Button("Jump")]
    public CharacterJumpController Jump()
    {
        positionBeforeJump = transform.localPosition.XY();
        var jumpMovement =  new Vector3(positionBeforeJump.x, positionBeforeJump.y, 0f);
        
        transform.DOLocalJump(
                jumpMovement,
                _settings.CharacterJumpForce,
                1,
                _settings.CharacterJumpDuration,
                false
            )
            .SetEase(_settings.CharacterJumpEaseType)
            .OnComplete(() => JumpFinished?.Invoke());

        initialShadowScale = shadowTransform.localScale;
        shadowTransform
            .DOScale(Vector3.one * _settings.CharacterShadowMinSize, _settings.CharacterJumpDuration / 2)
            .SetEase(_settings.CharacterShadowOutEaseType)
            .OnComplete(ReturnToNormalScale);
        
        void ReturnToNormalScale()
        {
            shadowTransform
                .DOScale(initialShadowScale, _settings.CharacterJumpDuration / 2)
                .SetEase(_settings.CharacterShadowInEaseType);
        }
        
        return this;
    }
}
