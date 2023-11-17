using Photon.Realtime;
using TMPro;
using Unity.FPS.Gameplay;
using Unity.FPS.Multiplayer;
using UnityEngine;

namespace Unity.FPS.UI
{
    public class StoreUI : UIComponent
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
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
        public void UpdateMoney(int money)
        {
            _moneyText.text = money.ToString() + "@";
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