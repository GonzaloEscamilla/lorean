using UnityEngine;

namespace _Project.Scripts.Core.Gameplay.Interactables
{
    public class Wall : Interactable
    {
        protected override void OnCharacterCollision()
        {
            Debug.Log("Character Detected");
        }
    }
}