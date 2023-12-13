using System;
using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.Core.Gameplay.EnvironmentElements
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class BackgroundObject : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected Vector3 xOutOfScreenPosition;
        [SerializeField] protected Vector3 spawnPosition;
        [SerializeField] protected Transform screenPositionReference;
        [SerializeField] private float snapOffset;
        [SerializeField] private Vector3 xMiddlePosition;
        
        public Action<BackgroundObject> MiddleScreenPositionReached;
        public Action<BackgroundObject> OutOfScreenPositionReached; 

        public float CurrentSpeed { get; set; }

        protected IPlaygroundProvider _playgroundProvider;
        
        private bool _middleScreenAlreadyReached;
        private bool _outOfScreenPositionAlreadyReached;

        private void Awake()
        {
            Services.WaitFor<IPlaygroundProvider>(SetPlaygroundProvider);
        }

        private void SetPlaygroundProvider(IPlaygroundProvider playgroundProvider)
        {
            _playgroundProvider = playgroundProvider;
        }

        private void OnEnable()
        {
            _middleScreenAlreadyReached = false;
            _outOfScreenPositionAlreadyReached = false;
        }

        private void OnDisable()
        {
            OutOfScreenPositionReached = null;
            MiddleScreenPositionReached = null;
            
            _middleScreenAlreadyReached = false;
            _outOfScreenPositionAlreadyReached = false;
        }

        public virtual void Init(string sortingLayer = "Default")
        {
            gameObject.SetActive(true);
            transform.position = spawnPosition;
            
            spriteRenderer.sortingLayerName = sortingLayer;
        }
        
        public virtual void ShutDown()
        {
            gameObject.SetActive(false);
        }

        public void SnapTo(BackgroundObject lastSpawnedBackground)
        {
            transform.position = lastSpawnedBackground.transform.position + new Vector3(snapOffset,0,0);
        }
        
        public void UpdateSortingOrder()
        {
            spriteRenderer.sortingOrder = _playgroundProvider.GetOrderInLayer(this.screenPositionReference.position);
        }
        
        private void Update()
        {
            Move();
            
            if (!_middleScreenAlreadyReached && transform.position.x <= xMiddlePosition.x)
            {
                OnEndPositionReached();
            }

            if (!_outOfScreenPositionAlreadyReached && transform.position.x <= xOutOfScreenPosition.x)
            {
                OnOutOfScreenPositionReached();
            }
        }
        
        private void Move()
        {
            var movement = Vector2.left * (CurrentSpeed * Time.deltaTime);
            transform.Translate(movement);
        }
        
        private void OnEndPositionReached()
        {
            _middleScreenAlreadyReached = true;
            MiddleScreenPositionReached?.Invoke(this);
        }
        private void OnOutOfScreenPositionReached()
        {
            _outOfScreenPositionAlreadyReached = true;
            OutOfScreenPositionReached?.Invoke(this);
        }
    }
}