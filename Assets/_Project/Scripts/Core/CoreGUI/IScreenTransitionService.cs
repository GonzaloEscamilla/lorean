using System;
using UnityEngine.UI;

namespace _Project.Scripts.Core.CoreGUI
{
    public enum ScreenTransitionType
    {
        In,
        Out
    }
    
    public interface IScreenTransitionService
    {
        event Action TransitionFinished;
        void Transition(ScreenTransitionType type);
    }
}