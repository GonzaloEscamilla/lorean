using System;
using _Project.Scripts.Core.Gameplay;
using _Project.Scripts.GameServices;
using UnityEngine;

public class OrderInLayerSetter : MonoBehaviour
{
    [SerializeField] 
    private Transform objectToSet;

    private IObjectsOrderInLayerProvider _orderInLayerProvider;
    
    private void Awake()
    {
        Services.WaitFor<IObjectsOrderInLayerProvider>(SetOrderProvider);
    }

    private void SetOrderProvider(IObjectsOrderInLayerProvider orderInLayerProvider)
    {
        _orderInLayerProvider = orderInLayerProvider;
    }
}
