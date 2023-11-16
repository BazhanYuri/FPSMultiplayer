using Photon.Realtime;
using Unity.FPS.Gameplay;
using Unity.FPS.Multiplayer;
using UnityEngine;

namespace Unity.FPS.UI
{
    public class StoreUI : UIComponent
    {
        private PlayerInputHandler _playerInputHandler;
        private bool _isStoreOpened = false;



        private void Awake()
        {
            Hide();
        }
        public void SetPlayerInput(PlayerInputHandler playerInputHandler)
        {
            _playerInputHandler = playerInputHandler;
            _playerInputHandler.OnStoreInputDown += SwitchStore;
        }
        private void SwitchStore()
        {
            _isStoreOpened = !_isStoreOpened;

            if (_isStoreOpened)
            {
                ShowStore();
            }
            else
            {
                HideShow();
            }

        }
        private void ShowStore()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Show();
        }
        private void HideShow()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Hide();
        }
    }
}