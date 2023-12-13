using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Core.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class BackgroundObject : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] private float offset;
        [SerializeField] private Vector3 xMiddlePosition;
        [SerializeField] protected Vector3 xOutOfScreenPosition;
        [FormerlySerializedAs("xSpawnPosition")] [SerializeField] protected Vector3 spawnPosition;
        
        public Action<BackgroundObject> MiddleScreenPositionReached;
        public Action<BackgroundObject> OutOfScreenPositionReached; 

        public float CurrentSpeed { get; set; }

        private bool _middleScreenAlreadyReached;
        private bool _outOfScreenPositionAlreadyReached;

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
            transform.position = lastSpawnedBackground.transform.position + new Vector3(offset,0,0);
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