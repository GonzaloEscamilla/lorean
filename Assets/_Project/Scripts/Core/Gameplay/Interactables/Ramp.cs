using _Project.Scripts.Core.Gameplay.Character;

namespace _Project.Scripts.Core.Gameplay.Interactables
{
    public class Ramp : Interactable
    {
        protected override void OnCharacterCollision(CharacterController character)
        {
            character.Jump(JumpType.Medium);
        }
    }
}