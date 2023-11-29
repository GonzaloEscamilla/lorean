using UnityEngine;

namespace _Project.Scripts.Core
{
    [CreateAssetMenu(menuName = "ND/GameSettings", fileName = "GameSettings", order = 0)]
    public class GameSettings: ScriptableObject
    {
        [SerializeField] private int levelSceneIndex;

        public int LevelSceneIndex => levelSceneIndex;
    }
}