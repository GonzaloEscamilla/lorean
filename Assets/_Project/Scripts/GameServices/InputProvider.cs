using System;
using UnityEngine;

namespace _Project.Scripts.GameServices
{
    public interface IInputProvider
    {
        public event Action<Vector2> InputDirectionChanged;
        public event Action StartInputPressed;
        public event Action JumpInputPressed;
    }
    
    public class InputProvider : MonoBehaviour, IInputProvider
    {
        public event Action<Vector2> InputDirectionChanged;
        public event Action StartInputPressed;
        public event Action JumpInputPressed;

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
            
            InputDirectionChanged?.Invoke(direction.normalized);
        }
    }
}