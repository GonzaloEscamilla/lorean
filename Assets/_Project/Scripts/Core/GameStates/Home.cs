﻿using _Project.Scripts.Core.CoreGUI;
using UnityEngine;

namespace _Project.Scripts.Core.GameStates
{
    /// <summary>
    /// Main Menu or Home. Entry point to Gameplay..
    /// </summary>
    public class Home : GameState
    {
        private HomeMenuView _homeMenuView;
        
        public Home(GameStateController controller) : base(controller)
        {
            Debug.LogWarning("Home Menu Constructuros");
            _homeMenuView = _menuInstanceProvider.GetMenuInstance<HomeMenuView>();
        }

        public override void Enter()
        {
            Debug.LogWarning("Home Menu State Enter");

            _homeMenuView.Show();
        }

        public override void Update()
        {
        }

        protected override void Exit()
        {
        }
    }
}