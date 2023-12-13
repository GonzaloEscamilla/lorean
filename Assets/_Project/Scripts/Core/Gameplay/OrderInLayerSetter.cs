using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.Core.Gameplay
{
    public class OrderInLayerSetter : MonoBehaviour
    {
        [SerializeField] 
        private Transform objectToSet;

        private IPlaygroundProvider _orderInLayerProvider;
    
        private void Awake()
        {
            Services.WaitFor<IPlaygroundProvider>(SetOrderProvider);
        }

        private void SetOrderProvider(IPlaygroundProvider orderInLayerProvider)
        {
            _orderInLayerProvider = orderInLayerProvider;
        }
    }
}
