using _Project.Scripts.Core.CoreGUI;
using _Project.Scripts.GameServices;

namespace _Project.Scripts.Core.GameStates
{
    public class Gameplay : GameState
    {
        private GamePlayMenuView _gamePlayMenuView;
        
        public Gameplay(GameStateController controller) : base(controller)
        {
            _gamePlayMenuView = _menuInstanceProvider.GetMenuInstance<GamePlayMenuView>();
        }

        public override void Enter()
        {
            _gamePlayMenuView.Show();
        }

        public override void Update()
        {
        }

        protected override void Exit()
        {
        }
    }
}