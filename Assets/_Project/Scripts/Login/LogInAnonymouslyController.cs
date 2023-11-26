using System.Threading.Tasks;
using _Project.Scripts.GameServices;
using _Project.Scripts.Telemetry;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace _Project.Scripts.Login
{
    public class LogInAnonymouslyController : ILoginService
    {
        private ITelemetrySender _telemetrySender;

        public LogInAnonymouslyController()
        {
            _telemetrySender = Services.Get<ITelemetrySender>();
        }

        public void LogIn()
        {
            SignInAnonymouslyAsync();
        }

        private async Task SignInAnonymouslyAsync()
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Sign in anonymously succeeded!");

                // Shows how to get the playerID
                Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

                UnityServices.ExternalUserId = AuthenticationService.Instance.PlayerId;
            }
            catch (AuthenticationException ex)
            {
                // Compare error code to AuthenticationErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
            catch (RequestFailedException ex)
            {
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
        }
    }
}


