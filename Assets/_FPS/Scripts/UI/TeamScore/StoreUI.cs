using Unity.FPS.Gameplay;

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
                Show();
            }
            else
            {
                Hide();
            }

        }
    }
}