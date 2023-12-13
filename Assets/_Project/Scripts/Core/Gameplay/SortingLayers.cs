using System;

namespace _Project.Scripts.Core.Gameplay
{
    public enum GameSortingLayer
    {
        Background,
        Middleground,
        Playground,
        Foreground
    }
    
    public static class GameSortingLayers
    {
        private const string BACKGROUND = "Background";
        private const string MIDDLEGROUND = "Middleground";
        private const string PLAYGROUND = "Playground";
        private const string FOREGROUND = "Foreground";

        public static string GetSortingLayer(GameSortingLayer layer)
        {
            switch (layer)
            {
                case GameSortingLayer.Background:
                    return BACKGROUND;
                case GameSortingLayer.Middleground:
                    return MIDDLEGROUND;
                case GameSortingLayer.Playground:
                    return PLAYGROUND;
                case GameSortingLayer.Foreground:
                    return FOREGROUND;
                default:
                    throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
            }
        }
    }
}