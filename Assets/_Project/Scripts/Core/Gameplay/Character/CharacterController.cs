using System;
using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.Core.Gameplay.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] 
        private SpriteRenderer renderer;

        private IInputProvider _inputProvider;
        private GameSettings _gameSettings;
        private Rigidbody2D _rigidbody2D;

        private Vector2 _currentInputDirection;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Services.WaitFor<IInputProvider>(SetInputProvider);
            Services.WaitFor<IGameSettingsProvider>(SetGameSettings);
        }

        private void SetInputProvider(IInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
            _inputProvider.InputDirectionChanged += OnInputDirectionChanged;
        }

        private void OnInputDirectionChanged(Vector2 inputDirection)
        {
            _currentInputDirection = inputDirection;
        }

        private void SetGameSettings(IGameSettingsProvider provider)
        {
            _gameSettings = provider.GameSettings;
        }

        private void FixedUpdate()
        {
            Move();            
        }

        public void Move()
        {
            var movement = _currentInputDirection * _gameSettings.CharacterBaseSpeed;
            //_rigidbody2D.velocity = movement;
            _rigidbody2D.AddForce(movement, ForceMode2D.Impulse);
        }

        public void Jump()
        {
            // Makes the character jump.
        }
    }
}
