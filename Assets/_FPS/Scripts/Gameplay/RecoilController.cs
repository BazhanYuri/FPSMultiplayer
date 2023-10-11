using System.Collections;
using UnityEngine;
using Zenject;

namespace Unity.FPS.Gameplay
{
    public class RecoilController : IInitializable, IRecoilController
    {
        private PlayerCharacterController _playerCharacterController;
        private WeaponConfig _weaponConfig;

        private Vector2 _currentRecoilForce;
        private int _currentFireCount;

        public void Initialize()
        {

        }
        public void SetWeaponConfig(WeaponConfig weaponConfig)
        {
            _weaponConfig = weaponConfig;

            _playerCharacterController = Object.FindObjectOfType<PlayerCharacterController>();
        }
        public void OnShoot()
        {
            CalculateRecoil();
            _playerCharacterController.MoveCameraVertical(-_currentRecoilForce.y);

            _currentFireCount++;
        }
        private void CalculateRecoil()
        {

            _currentRecoilForce.y = _weaponConfig.recoilForce.y * _currentFireCount;
            _currentRecoilForce = ClampVector2(_currentRecoilForce, -_weaponConfig.maxLimitForRecoil, _weaponConfig.maxLimitForRecoil);

            _playerCharacterController.StopAllCoroutines();
            _playerCharacterController.StartCoroutine(CoolDown());
        }
        public Vector2 ClampVector2(Vector2 inputVector, Vector2 minRange, Vector2 maxRange)
        {
            float clampedX = Mathf.Clamp(inputVector.x, minRange.x, maxRange.x);
            float clampedY = Mathf.Clamp(inputVector.y, minRange.y, maxRange.y);

            return new Vector2(clampedX, clampedY);
        }
        private IEnumerator CoolDown()
        {
            float timer = _weaponConfig.timeToRecover;

            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
            _currentFireCount = 0;
        }
    }
}