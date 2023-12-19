using _Project.Scripts.GameServices;
using _Project.Scripts.GameServices.Events;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Core.CoreGUI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Image lifeContainerOne;
        [SerializeField] private Image lifeContainerTow;
        [SerializeField] private Image lifeContainerThree;
        
        private GameSettings _gameSettings;
        
        public void Initialize()
        {
            Services.WaitFor<IGameSettingsProvider>(SetGameSettings);
        }

        private void SetGameSettings(IGameSettingsProvider gameSettingsProvider)
        {
            _gameSettings = gameSettingsProvider.GameSettings;
            EventsManager.Listen<HUD, CharacterLostHealth>(this, OnCharacterLostHealth);
            
            SetupCharacterLifeBar();
        }

        private void OnCharacterLostHealth(CharacterLostHealth healthData)
        {
            if (healthData.CurrentHealth == 0)
            {
                lifeContainerOne.gameObject.SetActive(false);
                lifeContainerTow.gameObject.SetActive(false);
                lifeContainerThree.gameObject.SetActive(false);
            }
            else if(healthData.CurrentHealth == 1)
            {
                lifeContainerOne.gameObject.SetActive(true);
                lifeContainerTow.gameObject.SetActive(false);
                lifeContainerThree.gameObject.SetActive(false);
            }
            else if(healthData.CurrentHealth == 2)
            {
                lifeContainerOne.gameObject.SetActive(true);
                lifeContainerTow.gameObject.SetActive(true);
                lifeContainerThree.gameObject.SetActive(false);
            }
            
            Debug.LogWarning("Character Lost Health Event Callback");
        }

        private void SetupCharacterLifeBar()
        {
            lifeContainerOne.gameObject.SetActive(true);
            lifeContainerTow.gameObject.SetActive(true);
            lifeContainerThree.gameObject.SetActive(true);
        }
    }
}