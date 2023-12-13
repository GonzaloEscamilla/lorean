using System;
using _Project.Scripts.GameServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Core.Gameplay
{
    public class BasicBackgroundObject : BackgroundObject
    {
        [SerializeField] 
        private bool usesOwnSpeed;

        [SerializeField] 
        private Transform screenPositionReference;
        
        [SerializeField] 
        [ShowIf(nameof(usesOwnSpeed))]
        private float speed;
        
        private void Awake()
        {
            Services.WaitFor<IObjectsOrderInLayerProvider>(SetOrder);

            if (usesOwnSpeed)
            {
                CurrentSpeed = speed;
            }
        }

        private void SetOrder(IObjectsOrderInLayerProvider orderInLayerProvider)
        {
            spriteRenderer.sortingOrder = orderInLayerProvider.GetOrderInLayer(this.screenPositionReference.position);
        }
    }
}