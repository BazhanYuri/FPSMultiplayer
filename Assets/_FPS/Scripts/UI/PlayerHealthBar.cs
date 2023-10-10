using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Unity.FPS.UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [Tooltip("Image component dispplaying current health")]
        public Image HealthFillImage;

        private Health _PlayerHealth;

        private EventBus _eventBus;



        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        private void OnEnable()
        {
            _eventBus.PlayerSpawned += OnPlayerSpawned;
        }
        private void OnDisable()
        {
            _eventBus.PlayerSpawned -= OnPlayerSpawned;
        }
        private void OnPlayerSpawned()
        {
            GetPlayer();
        }
        private void GetPlayer()
        {
            PlayerCharacterController playerCharacterController =
                GameObject.FindObjectOfType<PlayerCharacterController>();
            DebugUtility.HandleErrorIfNullFindObject<PlayerCharacterController, PlayerHealthBar>(
                playerCharacterController, this);

            _PlayerHealth = playerCharacterController.GetComponent<Health>();
            DebugUtility.HandleErrorIfNullGetComponent<Health, PlayerHealthBar>(_PlayerHealth, this,
                playerCharacterController.gameObject);
        }

        void Update()
        {
            if (_PlayerHealth == null)
            {
                return;
            }
            // update health bar value
            HealthFillImage.fillAmount = _PlayerHealth.CurrentHealth / _PlayerHealth.MaxHealth;
        }
    }
}