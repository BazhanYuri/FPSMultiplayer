﻿using System;
using Unity.FPS.Enums;
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


        public TeamType TeamType { get; private set; }


        private void Awake()
        {
            DisableAll();
        }
        private void DisableAll()
        {
            foreach (var camera in _cameras)
            {
                camera.enabled = false;
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

