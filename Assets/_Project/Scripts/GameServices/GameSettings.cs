using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.GameServices
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

        [Title("Character")] 
        [SerializeField]
        private float characterBaseSpeed;

        [SerializeField]
        private Vector2 mapXBounds;
        
        [SerializeField]
        private Vector2 mapYBounds;
        
        public int LevelSceneIndex => levelSceneIndex;
        public float BackgroundSpeed => backgroundSpeed;
        public float TreeLayerSpeed => treeLayerSpeed;
        public float CharacterBaseSpeed => characterBaseSpeed;
        public Vector2 MapXBounds => mapXBounds;
        public Vector2 MapYBounds => mapYBounds;
    }
}