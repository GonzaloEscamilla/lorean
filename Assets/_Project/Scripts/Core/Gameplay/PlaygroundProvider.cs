using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.Core.Gameplay
{
    public interface IPlaygroundProvider
    {
        public int GetOrderInLayer(Vector2 screenPosition);
        public float UpperGameplayLimit { get; }
        public float LowerGameplayLimit { get; }
    }

    public class PlaygroundProvider : MonoBehaviour, IPlaygroundProvider
    {
        [SerializeField] private Transform upperPivot;
        [SerializeField] private Transform lowerPivot;

        public float UpperGameplayLimit => upperPivot.transform.position.y;
        public float LowerGameplayLimit => lowerPivot.transform.position.y;
        
        private const int MIN_ORDER_IN_LAYER = 0;
        private const int MAX_ORDER_IN_LAYER = 100;

        public int GetOrderInLayer(Vector2 screenPosition)
        {
            // Calculate the normalized position between lower and upper pivots
            float normalizedPosition = Mathf.InverseLerp(lowerPivot.position.y, upperPivot.position.y, screenPosition.y);
        
            // Ensure the normalized position is within the [0, 1] range
            normalizedPosition = Mathf.Clamp01(normalizedPosition);

            // Interpolate between the minimum and maximum order in layer
            int interpolatedOrder = Mathf.RoundToInt(Mathf.Lerp(MAX_ORDER_IN_LAYER, MIN_ORDER_IN_LAYER, normalizedPosition));

            return interpolatedOrder;
        }

        private void Awake()
        {
            Services.Add<IPlaygroundProvider>(this);
        }
    }
}