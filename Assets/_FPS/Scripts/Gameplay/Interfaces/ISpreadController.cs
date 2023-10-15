
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public interface ISpreadController
    {
        Vector2 CurrentSpread { get; }
        public void SetWeaponConfig(WeaponConfig weaponConfig);
        public void OnShoot();
    }   
}