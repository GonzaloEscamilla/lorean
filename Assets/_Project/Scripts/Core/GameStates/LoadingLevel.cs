﻿using _Project.Scripts.Core.CoreGUI;
using _Project.Scripts.GameServices;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Core.GameStates
{
    public class LoadingLevel : GameState
    {
        private ISceneLoader _sceneLoader;
        private IScreenTransitionService _screenTransition;
        private GameSettings _gameSettings;
        
        public LoadingLevel(GameStateController controller) : base(controller)
        {
            _sceneLoader = Services.Get<ISceneLoader>();
            _gameSettings = Services.Get<IGameSettingsProvider>().GameSettings;
            _screenTransition = Services.Get<IScreenTransitionService>();
        }

        public override void Enter()
        {
            LoadLevel().Forget();
        }

        private async UniTaskVoid LoadLevel()
        {
            _debug.LogWarning("Here");
            await _sceneLoader.LoadSceneAsync(_gameSettings.LevelSceneIndex);
            _debug.LogWarning("Here1");
            _screenTransition.Transition(ScreenTransitionType.In);
        }
        
        public override void Update()
        {
        }

        protected override void Exit()
        {
        }
    }
}