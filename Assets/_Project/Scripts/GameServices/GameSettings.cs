using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Core
{
    [CreateAssetMenu(menuName = "ND/GameSettings", fileName = "GameSettings", order = 0)]
    public class GameSettings: ScriptableObject
    {
        [Title("General")]
        [SerializeField] 
        private int levelSceneIndex;

        [Title("Environment & Background")] 
        [SerializeField]
        private float backgroundSpeed;

        [SerializeField]
        private float treeLayerSpeed;
        
        public int LevelSceneIndex => levelSceneIndex;
        public float BackgroundSpeed => backgroundSpeed;
        public float TreeLayerSpeed => treeLayerSpeed;
    }
}