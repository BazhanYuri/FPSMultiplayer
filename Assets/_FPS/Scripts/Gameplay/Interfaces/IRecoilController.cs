namespace Unity.FPS.Gameplay
{
    public interface IRecoilController
    {
        public void SetWeaponConfig(WeaponConfig weaponConfig);
        public void OnShoot();
    }
}