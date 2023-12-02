using System;
using Codice.CM.Client.Differences;
using UnityEngine;

public class BackgroundLayer : MonoBehaviour
{
    public float CurrentSpeed { get; set; }
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var movement = Vector2.left * (CurrentSpeed * Time.deltaTime);
        transform.Translate(movement);
    }
}