using System;
using Unity.FPS.Enums;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;


namespace Unity.FPS.Multiplayer
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Camera[] _cameras;
        [SerializeField] private PlayerInputHandler _inputHandler;
        [SerializeField] private PlayerCharacterController _characterController;
        [SerializeField] private Jetpack _jetpack;
        [SerializeField] private Health _health;


        public TeamType TeamType { get; private set; }
        public Health Health { get => _health;}

        private void Awake()
        {
            DisableAll();
        }
        private void DisableAll()
        {
            foreach (var camera in _cameras)
            {
                camera.enabled = false;

                if (camera.TryGetComponent(out AudioListener component))
                {
                    component.enabled = false;
                }
            }
            _inputHandler.enabled = false;
            _characterController.enabled = false;
            _jetpack.enabled = false;
        }
        public void SetAsLocalMultiplayer()
        {
            foreach (var camera in _cameras)
            {
                camera.enabled = true;

                if (camera.TryGetComponent(out AudioListener component))
                {
                    component.enabled = true;
                }
            }
            _inputHandler.enabled = true;
            _characterController.enabled = true;
            _jetpack.enabled = true;
            gameObject.SetActive(true);
        }
        public void SetTeam(TeamType teamType)
        {
            TeamType = teamType;
        }
    }
}

