using System;
using Core;
using Cysharp.Threading.Tasks;
using Login;
using Telemetry;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

namespace Initialization
{
    public class GlobalInitializer : MonoBehaviour
    {
        [SerializeField] 
        private GameStateController gameStateController;
        
        private const string UGS_ENVIRONMENT_NAME = "production"; 
        
        private void Awake()
        {
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
        }
    }
}

