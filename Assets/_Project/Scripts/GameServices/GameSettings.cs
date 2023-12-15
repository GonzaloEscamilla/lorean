using DG.Tweening;
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
        
        [SerializeField]
        private float buildingsLayerSpeed;
        
        [Title("Character")] 
        [SerializeField]
        private float characterBaseSpeed;
        
        [SerializeField]
        private float characterJumpForce;
        
        [SerializeField]
        private float characterJumpDuration;
        
        [SerializeField]
        private Ease characterJumpEaseType;
        
        [SerializeField]
        private float characterShadowMinSize;
        
        [SerializeField]
        private Ease characterShadowOutEaseType;
        
        [SerializeField]
        private Ease characterShadowInEaseType;
        
        public int LevelSceneIndex => levelSceneIndex;
        public float BackgroundSpeed => backgroundSpeed;
        public float TreeLayerSpeed => treeLayerSpeed;
        public float BuildingsLayerSpeed => buildingsLayerSpeed;
        public float CharacterBaseSpeed => characterBaseSpeed;
        public float CharacterJumpForce => characterJumpForce;
        public float CharacterJumpDuration => characterJumpDuration;
        public Ease CharacterJumpEaseType => characterJumpEaseType;
        public float CharacterShadowMinSize => characterShadowMinSize;
        public Ease CharacterShadowOutEaseType => characterShadowOutEaseType;
        public Ease CharacterShadowInEaseType => characterShadowInEaseType;
    }
}