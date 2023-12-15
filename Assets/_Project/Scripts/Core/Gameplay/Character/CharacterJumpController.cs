using System;
using _Project.Scripts.GameServices;
using _Project.Scripts.Utilities.Math;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Core.Gameplay.Character
{
    public enum JumpType
    {
        Low,
        Medium,
        High
    }
    
    public class CharacterJumpController : MonoBehaviour
    {
        [SerializeField] 
        private Transform shadowTransform;
    
        public event Action JumpFinished;

        private CharacterController _character;
        private GameSettings _settings;
        private Vector2 positionBeforeJump;
        private Vector2 initialShadowScale;

        public void Setup(CharacterController controller)
        {
            _character = controller;
            
            Services.WaitFor<IGameSettingsProvider>(SetGameSettings);
        }

        private void SetGameSettings(IGameSettingsProvider provider)
        {
            _settings = provider.GameSettings;
        }

        [Button("Jump")]
        public CharacterJumpController Jump(JumpType jumpType)
        {
            positionBeforeJump = transform.localPosition.XY();
            var jumpMovement =  new Vector3(positionBeforeJump.x, positionBeforeJump.y, 0f);

            SetJumpForceAndDuration(jumpType, out var jumpForce, out var jumpDuration);

            _character.DisableCollisions();
            
            transform.DOLocalJump(
                    jumpMovement,
                    jumpForce,
                    1,
                    jumpDuration,
                    false
                )
                .SetEase(_settings.CharacterJumpEaseType)
                .OnComplete(OnJumpFinished);

            initialShadowScale = shadowTransform.localScale;
            shadowTransform
                .DOScale(Vector3.one * _settings.CharacterShadowMinSize, jumpDuration / 2)
                .SetEase(_settings.CharacterShadowOutEaseType)
                .OnComplete(ReturnToNormalScale);

            void OnJumpFinished()
            {
                _character.EnableCollisions();
                JumpFinished?.Invoke();
            }
            
            void ReturnToNormalScale()
            {
                shadowTransform
                    .DOScale(initialShadowScale, jumpDuration/ 2)
                    .SetEase(_settings.CharacterShadowInEaseType);
            }
        
            return this;
        }

        private void SetJumpForceAndDuration(JumpType jumpType, out float jumpForce, out float jumpDuration)
        {
            jumpForce = _settings.CharacterJumpForce;
            jumpDuration = _settings.CharacterJumpDuration;
            
            switch (jumpType)
            {
                case JumpType.Low:
                    // Note: Low is the default one.
                    break;
                case JumpType.Medium:
                    jumpForce = _settings.CharacterJumpForceMedium;
                    jumpDuration = _settings.CharacterJumpDurationMedium;
                    break;
                case JumpType.High:
                    jumpForce = _settings.CharacterJumpForceHigh;
                    jumpDuration = _settings.CharacterJumpDurationHigh;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(jumpType), jumpType, null);
            }
        }
    }
}
