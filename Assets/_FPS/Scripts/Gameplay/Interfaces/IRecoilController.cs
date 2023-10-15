using System;

namespace Unity.FPS.Gameplay
{

    public interface IRecoilController
    {
        public event Action CooldDownEnded;
        public int CurrentFireCount { get; }
        public void SetWeaponConfig(WeaponConfig weaponConfig);
        public void OnShoot();
    }
}