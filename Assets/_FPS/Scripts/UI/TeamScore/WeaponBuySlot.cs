using System;
using TMPro;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class WeaponBuySlot : UIComponent
    {
        [SerializeField] private WeaponController _weapon;
        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _weaponName;

        private Color _canBuyColor = Color.green;
        private Color _cantBuyColor = Color.red;

        public WeaponController Weapon { get => _weapon;}
        public event Action<WeaponBuySlot> OnWeaponClicked;

        private void OnEnable()
        {
            _buyButton.onClick.AddListener(WeaponClicked);

            UpdatePriceText();
        }
        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(WeaponClicked);
        }
        private void WeaponClicked()
        {
            OnWeaponClicked?.Invoke(this);
        }
        private void UpdatePriceText()
        {
            _priceText.text = _weapon.WeaponConfig.price.ToString() + "@";
            _weaponName.text = _weapon.WeaponConfig.name;
        }
        public void UpdateColor(bool canBuy)
        {
            _priceText.color = canBuy ? _canBuyColor : _cantBuyColor;
        }
    }
}