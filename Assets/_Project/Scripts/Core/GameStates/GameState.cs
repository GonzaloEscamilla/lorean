using _Project.Scripts.Core.CoreGUI;
using _Project.Scripts.GameServices;
using _Project.Scripts.Utilities;

namespace _Project.Scripts.Core.GameStates
{
    public abstract class GameState
    {
        protected GameStateController _controller;
        protected IMenuInstanceProvider _menuInstanceProvider;
        protected IDebug _debug;
        
        protected GameState(GameStateController controller)
        {
            _controller = controller;
            _menuInstanceProvider = Services.Get<IMenuInstanceProvider>();
            _debug = Services.Get<IDebug>();
        }

        public abstract void Enter();
        public abstract void Update();
        
        /// <summary>
        /// This state should dispose and shutdown all resoruces
        /// </summary>
        protected abstract void Exit();

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}