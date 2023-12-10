using System;
using UnityEngine;

namespace _Project.Scripts.Core.Gameplay
{
    public class BackgroundObject : MonoBehaviour
    {
        [SerializeField] private float xFinalPosition;
        [SerializeField] private float xOutOfScreenPosition;
        [SerializeField] private float xSpawnPosition;
        [SerializeField] private float offset;

        public event Action<BackgroundObject> OutOfScreenPositionReached; 
        public event Action<BackgroundObject> EndPositionReached;

        private bool _endPositionAlreadyReached;
        private bool _outOfScreenPositionAlreadyReached;

        public void Init()
        {
            _endPositionAlreadyReached = false;
            _outOfScreenPositionAlreadyReached = false;
            
            gameObject.SetActive(true);
            transform.position = new Vector3(xSpawnPosition, 0, 0);
        }

        public void ShutDown()
        {
            OutOfScreenPositionReached = null;
            EndPositionReached = null;
            _endPositionAlreadyReached = false;
            _outOfScreenPositionAlreadyReached = false;
            
            gameObject.SetActive(false);
        }

        public void SnapTo(BackgroundObject lastSpawnedBackground)
        {
            transform.position = lastSpawnedBackground.transform.position + new Vector3(offset,0,0);
        }
        
        private void Update()
        {
            if (!_endPositionAlreadyReached && transform.position.x <= xFinalPosition)
            {
                OnEndPositionReached();
            }

            if (!_outOfScreenPositionAlreadyReached && transform.position.x <= xOutOfScreenPosition)
            {
                OnOutOfScreenPositionReached();
            }
        }

        public void Move(float speed)
        {
            var movement = Vector2.left * (speed * Time.deltaTime);
            transform.Translate(movement);
        }
    
        private void OnEndPositionReached()
        {
            _endPositionAlreadyReached = true;
            EndPositionReached?.Invoke(this);
        }
        private void OnOutOfScreenPositionReached()
        {
            _outOfScreenPositionAlreadyReached = true;
            OutOfScreenPositionReached?.Invoke(this);
        }
    }
}