using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.Core.Gameplay.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] 
        private SpriteRenderer carRenderer;

        [SerializeField] 
        private SpriteRenderer shadowRenderer;

        [SerializeField] 
        private BoxCollider2D collider2D;
        
        [SerializeField] 
        private Transform screenPositionReference;
        
        [SerializeField] 
        private CharacterJumpController jumpController;

        private float CurrentLife { get; set; }
        
        private IInputProvider _inputProvider;
        private GameSettings _gameSettings;
        private Rigidbody2D _rigidbody2D;

        private Vector2 _currentInputDirection;
        private bool _canJump = true;

        private IPlaygroundProvider _orderInLayerProvider;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            Services.WaitFor<IGameSettingsProvider>(SetGameSettings);
            Services.WaitFor<IInputProvider>(SetInputProvider);
            Services.WaitFor<IPlaygroundProvider>(SetOrderInLayerProvider);
            
            jumpController.Setup(this);
        }

        private void SetOrderInLayerProvider(IPlaygroundProvider orderInLayerProvider)
        {
            _orderInLayerProvider = orderInLayerProvider;
        }

        private void SetGameSettings(IGameSettingsProvider provider)
        {
            _gameSettings = provider.GameSettings;
        }
        
        private void SetInputProvider(IInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
            _inputProvider.InputDirectionChanged += OnInputDirectionChanged;
            _inputProvider.JumpInputPressed += OnJumpInputPressed;
        }

        private void OnInputDirectionChanged(Vector2 inputDirection)
        {
            _currentInputDirection = inputDirection;
        }

        private void OnJumpInputPressed()
        {
            Jump(JumpType.Low);
        }

        
        public void GetDamaged()
        {
            CurrentLife--;
        }
        
        public void Push(Vector2 direction, float strength)
        {
            _rigidbody2D.AddForce(direction * strength, ForceMode2D.Impulse);
        }

        public void Jump(JumpType jumpType)
        {
            if (!_canJump)
            {
                return;
            }

            _canJump = false;
            
            jumpController.Jump(jumpType).JumpFinished += OnJumpFinished;
            
            void OnJumpFinished()
            {
                _canJump = true;
            }
        }
        
        public void EnableCollisions()
        {
            collider2D.enabled = true;
        }
        
        public void DisableCollisions()
        {
            collider2D.enabled = false;
        }
        
        private void Update()
        {
            if (_orderInLayerProvider == null)
            {
                return;
            }

            var position = screenPositionReference.position;
            carRenderer.sortingOrder = _orderInLayerProvider.GetOrderInLayer(position);
            shadowRenderer.sortingOrder = _orderInLayerProvider.GetOrderInLayer(position) - 1;
        }
        
        private void FixedUpdate()
        {
            Move();            
        }

        private void Move()
        {
            var movement = _currentInputDirection * _gameSettings.CharacterBaseSpeed;
            _rigidbody2D.AddForce(movement, ForceMode2D.Impulse);
        }
    }
}
