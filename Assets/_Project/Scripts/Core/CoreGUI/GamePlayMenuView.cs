using System;
using UnityEngine;

namespace _Project.Scripts.Core.CoreGUI
{
    public class GamePlayMenuView : GameMenuBase
    {
        [SerializeField] private HUD hud;

        private void Awake()
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();

            hud.Initialize();
        }
        
        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}