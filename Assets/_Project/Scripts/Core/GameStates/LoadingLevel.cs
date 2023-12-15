using _Project.Scripts.Core.CoreGUI;
using _Project.Scripts.GameServices;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Core.GameStates
{
    public class LoadingLevel : GameState
    {
        private ISceneLoader _sceneLoader;
        private IScreenTransitionService _screenTransition;
        private GameSettings _gameSettings;
        private IGameAudio _gameAudio;
        
        public LoadingLevel(GameStateController controller) : base(controller)
        {
            _sceneLoader = Services.Get<ISceneLoader>();
            _gameSettings = Services.Get<IGameSettingsProvider>().GameSettings;
            _screenTransition = Services.Get<IScreenTransitionService>();
            _gameAudio = Services.Get<IGameAudio>();
        }

        public override void Enter()
        {
            LoadLevel().Forget();
        }

        private async UniTaskVoid LoadLevel()
        {
            await _sceneLoader.LoadSceneAsync(_gameSettings.LevelSceneIndex);
            _gameAudio.PlayMainSong();
            _screenTransition.Transition(ScreenTransitionType.In, Exit);
        }
        
        public override void Update()
        {
        }

        protected override void Exit()
        {
            _controller.SwitchState<Gameplay>();
        }
    }
}