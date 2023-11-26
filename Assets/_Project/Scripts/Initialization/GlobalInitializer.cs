using System;
using _Project.Scripts.Core;
using _Project.Scripts.Core.CoreGUI;
using _Project.Scripts.GameServices;
using _Project.Scripts.Login;
using _Project.Scripts.Telemetry;
using Cysharp.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

namespace _Project.Scripts.Initialization
{
    public class GlobalInitializer : MonoBehaviour
    {
        [SerializeField] 
        private GameStateController gameStateController;

        [SerializeField] 
        private RootCanvas rootCanvas;
        
        private const string UGS_ENVIRONMENT_NAME = "production"; 
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            Initialize();
        }
    
        private async UniTaskVoid Initialize()
        {
            await InitializeUGS();
            InitializeServices();
            
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            
            gameStateController.Initialize();
        }
    
        private async UniTask<string> InitializeUGS()
        {
            try
            {
                var options = new InitializationOptions().SetEnvironmentName(UGS_ENVIRONMENT_NAME);
                await UnityServices.InitializeAsync(options);
                Debug.Log("Unity Game Services Initialized");
            }
            catch (Exception exception)
            {
                // Note: An error occurred during initialization.
                throw new Exception(exception.Message);
            }
    
            return null;
        }
        
        private void InitializeServices()
        {
            Debug.Log("Initialize other services");
            
            ITelemetrySender telemetrySender = new UnityAnalyticsManager();
            Services.Add<ITelemetrySender>(telemetrySender);
    
            ILoginService loginService = new LogInAnonymouslyController();
            Services.Add<ILoginService>(loginService);

            Services.Add<IRootCanvasProvider>(rootCanvas);

            IMenuInstanceProvider menuInstanceProvider = new MenuInstanceFactory();
            Services.Add<IMenuInstanceProvider>(menuInstanceProvider);
        }
    }
}

