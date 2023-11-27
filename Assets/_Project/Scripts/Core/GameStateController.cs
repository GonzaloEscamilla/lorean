using System;
using _Project.Scripts.Core.GameStates;
using _Project.Scripts.GameServices;
using _Project.Scripts.Utilities;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class GameStateController : MonoBehaviour
    {
        private GameState _currentGameState;
        private IDebug _debug;
        
        public void Initialize()
        {
            _debug = Services.Get<IDebug>();
            
            StartFSM();
        }
        
        private void Update()
        {
            if (_currentGameState == null)
            {
                return;
            }
            
            _currentGameState.Update();
        }

        private void StartFSM()
        {
            SwitchState<Startup>();
        }

        public void SwitchState<T>() where T : GameState
        {
            _debug.Log($"GameState: Switching from {_currentGameState} to {typeof(T).Name}");
            
            _currentGameState = (T)Activator.CreateInstance(typeof(T), new object[1]{this});
            _currentGameState.Enter();
        }
    }
}