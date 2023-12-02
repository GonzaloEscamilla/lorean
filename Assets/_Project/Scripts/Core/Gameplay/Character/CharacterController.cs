using System;
using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.Core.Gameplay.Character
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] 
        private SpriteRenderer renderer;

        private GameSettings _gameSettings;

        private void Awake()
        {
            Services.WaitFor<IGameSettingsProvider>(SetGameSettings);
        }

        private void SetGameSettings(IGameSettingsProvider provider)
        {
            _gameSettings = provider.GameSettings;
        }

        private void Update()
        {
            Vector2 direction = Vector2.zero;
            
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector2.up;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector2.left;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector2.down;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector2.right;
            }
            
            Move(direction);
        }

        public void Move(Vector2 direction)
        {
            var movement = direction.normalized * (_gameSettings.CharacterBaseSpeed * Time.deltaTime);

            var lastPosition = transform.position;            
            
            transform.Translate(movement);

            if (transform.position.x < _gameSettings.MapXBounds.x
                || transform.position.x > _gameSettings.MapXBounds.y
                || transform.position.y < _gameSettings.MapYBounds.x
                || transform.position.y > _gameSettings.MapYBounds.y)
            {
                transform.position = lastPosition;
            }
        }

        public void Jump()
        {
            // Makes the character jump.
        }
    }
}
