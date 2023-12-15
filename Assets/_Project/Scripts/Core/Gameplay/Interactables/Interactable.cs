using UnityEngine;

namespace _Project.Scripts.Core.Gameplay.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableMask;

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the entering collider is on the specified layer
            if (((1 << other.gameObject.layer) & interactableMask) != 0)
            {
                OnCharacterCollision();
            }
        }

        protected abstract void OnCharacterCollision();
    }
}
