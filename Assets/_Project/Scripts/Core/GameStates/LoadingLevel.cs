using _Project.Scripts.GameServices;

namespace _Project.Scripts.Core.GameStates
{
    public class LoadingLevel : GameState
    {
        private ISceneLoader _sceneLoader;
        private GameSettings _gameSettings;
        
        public LoadingLevel(GameStateController controller) : base(controller)
        {
            _sceneLoader = Services.Get<ISceneLoader>();
            _gameSettings = Services.Get<IGameSettingsProvider>().GameSettings;
        }

        public override void Enter()
        {
            _sceneLoader.LoadSceneAsync(_gameSettings.LevelSceneIndex).Forget(); // TODO: Remove the magic number and use a shared const
        }
        
        public override void Update()
        {
        }

        protected override void Exit()
        {
        }
    }
}