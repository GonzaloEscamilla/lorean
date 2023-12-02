using System;
using UnityEngine;

public class BackgroundObject : MonoBehaviour
{
    [SerializeField] private OutOfScreenDetector detector;

    public event Action<BackgroundObject> OutOfScreen; 

    private void OnEnable()
    {
        detector.OutOfScreen += OnOutOfScreen;
    }

    private void OnDisable()
    {
        detector.OutOfScreen -= OnOutOfScreen;
    }

    public void Move(float speed)
    {
        var movement = Vector2.left * speed * Time.deltaTime;
        transform.Translate(movement);
    }
    
    private void OnOutOfScreen()
    {
        OutOfScreen?.Invoke(this);
    }
}