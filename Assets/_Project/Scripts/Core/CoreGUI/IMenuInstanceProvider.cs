using System;
using System.Collections.Generic;
using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.Core.CoreGUI
{
    public interface IMenuInstanceProvider
    {
        T GetMenuInstance<T>() where T : GameMenuBase;
    }

    public class MenuInstanceFactory : IMenuInstanceProvider
    {
        private string _defaultMenuName = "DefaultMenu";
        private string _startupMenuPrefabName = "StartupMenu";
        private string _homeMenuPrefabName = "HomeMenu";

        private Dictionary<string, GameMenuBase> _catchedMenues = new();

        private readonly Dictionary<Type, string> _menuTypes;

        public MenuInstanceFactory()
        {
            _menuTypes = new ()
            {
                {typeof(StartupMenuView), _startupMenuPrefabName},
                {typeof(HomeMenuView), _homeMenuPrefabName},
            };
        }
        
        public T GetMenuInstance<T>() where T : GameMenuBase
        {
            string prefabName = _defaultMenuName;
            if (_menuTypes.ContainsKey(typeof(T)))
            {
                _menuTypes.TryGetValue(typeof(T), out prefabName);
            }
            
            if (_catchedMenues.ContainsKey(typeof(T).ToString()))
            {
                _catchedMenues.TryGetValue(typeof(T).ToString(), out var menu);
                menu.Initialize();
                return menu as T;
            }
            
            var menuPrefab = Resources.Load<GameObject>(prefabName);
            if (menuPrefab == null)
            {
                Debug.LogError("The SplashScreen prefab failed to load, please make sure it exist in the right path");
                return null;
            }

            var rootCanvasProvider = Services.Get<IRootCanvasProvider>();

            GameMenuBase menuBaseInstance;
            
            var splashScreenInstance = GameObject.Instantiate(menuPrefab, rootCanvasProvider.Canvas.transform);
            menuBaseInstance = splashScreenInstance.GetComponent<GameMenuBase>();
            
            _catchedMenues.Add(menuBaseInstance.GetType().ToString(), menuBaseInstance);
            
            menuBaseInstance.Initialize();
            return menuBaseInstance as T;
        }
    }
}