using Unity.FPS.Game;
using UnityEngine;
using Zenject;

namespace Unity.FPS.UI
{
    public class Store : MonoBehaviour
    {
        [SerializeField] private WeaponBuySlot[] _weaponBuySlots;
        [SerializeField] private StoreUI _storeUI;



        private IWallet _wallet;

        [Inject]
        public void Construct(IWallet wallet)
        {
            _wallet = wallet;
        }

        private void OnEnable()
        {
            _wallet.OnAmountChanged += MoneyUpdated;
            _wallet.AddAmount(10000);
            foreach (var weaponBuySlot in _weaponBuySlots)
            {
                weaponBuySlot.OnWeaponClicked += OnWeaponClicked;
            }
            UpdatePrices();
        }

        private void OnDisable()
        {
            _wallet.OnAmountChanged -= MoneyUpdated;
            foreach (var weaponBuySlot in _weaponBuySlots)
            {
                weaponBuySlot.OnWeaponClicked -= OnWeaponClicked;
            }
        }

        private void OnWeaponClicked(WeaponBuySlot weaponBuySlot)
        {
            if (IsPriceEnough(weaponBuySlot.Weapon.WeaponConfig.price))
            {
                _wallet.RemoveAmount(weaponBuySlot.Weapon.WeaponConfig.price);
                EquipWeapon(weaponBuySlot.Weapon);
            }
            UpdatePrices();
        }
        private bool IsPriceEnough(int price)
        {
            return _wallet.CanAfford(price);
        }

        private void EquipWeapon(WeaponController weaponController)
        {
            Multiplayer.Player.Instance.WeaponsManager.AddWeapon(weaponController);
        }
        private void UpdatePrices()
        {
            foreach (var weaponBuySlot in _weaponBuySlots)
            {
                weaponBuySlot.UpdateColor(IsPriceEnough(weaponBuySlot.Weapon.WeaponConfig.price));
            }
        }
        private void MoneyUpdated(int amount)
        {
            UpdatePrices();
            _storeUI.UpdateMoney(amount);
        }
    }
}