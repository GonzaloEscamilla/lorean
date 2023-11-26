using _Project.Scripts.Core.CoreGUI;
using _Project.Scripts.GameServices;

namespace _Project.Scripts.Core.GameStates
{
    /// <summary>
    /// Handles, default login, game splash screen and any other feature before main menu.
    /// </summary>
    public class Startup : GameState
    {
        private StartupMenuView _startupMenu;
        
        public Startup(GameStateController controller) : base(controller)
        {
            _startupMenu = _menuInstanceProvider.GetMenuInstance<StartupMenuView>();
        }

        public override void Enter()
        {
            _startupMenu.ActionFinished += Exit;
            _startupMenu.Show();
        }

        public override void Update()
        {
        }

        protected override void Exit()
        {
            _startupMenu.ActionFinished -= Exit;
            _controller.SwitchState<Home>();
        }
    }
}