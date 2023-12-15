using UnityEngine;
using CharacterController = _Project.Scripts.Core.Gameplay.Character.CharacterController;

namespace _Project.Scripts.Core.Gameplay.Interactables
{
    public class Wall : Interactable
    {
        [SerializeField] private float pushBackStrength;
        
        protected override void OnCharacterCollision(CharacterController character)
        {
            character.GetDamaged();
            character.Push(Vector2.left, pushBackStrength);
        }
    }
}