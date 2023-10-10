using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Unity.FPS.UI
{
    public class StanceHUD : MonoBehaviour
    {
        [Tooltip("Image component for the stance sprites")]
        public Image StanceImage;

        [Tooltip("Sprite to display when standing")]
        public Sprite StandingSprite;

        [Tooltip("Sprite to display when crouching")]
        public Sprite CrouchingSprite;

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
            PlayerCharacterController character = FindObjectOfType<PlayerCharacterController>();
            DebugUtility.HandleErrorIfNullFindObject<PlayerCharacterController, StanceHUD>(character, this);
            character.OnStanceChanged += OnStanceChanged;

            OnStanceChanged(character.IsCrouching);
        }

        void OnStanceChanged(bool crouched)
        {
            StanceImage.sprite = crouched ? CrouchingSprite : StandingSprite;
        }
    }
}