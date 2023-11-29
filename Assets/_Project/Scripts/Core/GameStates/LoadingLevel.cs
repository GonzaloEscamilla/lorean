using _Project.Scripts.GameServices;

namespace _Project.Scripts.Core.GameStates
{
    public class LoadingLevel : GameState
    {
        private ISceneLoader _sceneLoader;
    
        public LoadingLevel(GameStateController controller) : base(controller)
        {
            _sceneLoader = Services.Get<ISceneLoader>();
        }

        public override void Enter()
        {
            _sceneLoader.LoadSceneAsync(1).Forget(); // TODO: Remove the magic number and use a shared const
        }

        public override void Update()
        {
        }

        protected override void Exit()
        {
        }
    }
}