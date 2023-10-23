using System.Collections;
using UnityEngine;
using Zenject;

namespace Unity.FPS.Gameplay
{
    public class SpreadController : IInitializable, ISpreadController
    {
        private IRecoilController _recoilController;
        private WeaponConfig _weaponConfig;

        public Vector2 CurrentSpread { get; private set; }


        [Inject]
        public void Construct(IRecoilController recoilController)
        {
            _recoilController = recoilController;
        }
        public void Initialize()
        {
            _recoilController.CooldDownEnded += OnCooldownEnded;
        }
        public void SetWeaponConfig(WeaponConfig weaponConfig)
        {
            _weaponConfig = weaponConfig;
        }
        public void OnShoot()
        {
            CalculateSpread();
        }
        private void CalculateSpread()
        {
            Vector3 tempSpread = CurrentSpread;

            if (_weaponConfig.spreads.Length > _recoilController.CurrentFireCount)
            {
                Spread spreadData = _weaponConfig.spreads[_recoilController.CurrentFireCount];
                tempSpread.x += spreadData.delta.x;

                float randomX = spreadData.randomize * spreadData.delta.x;
                randomX = Random.Range(-randomX, randomX);
                tempSpread.x += randomX;

                tempSpread.y += spreadData.delta.y;

                float randomY = spreadData.randomize * spreadData.delta.y;
                randomY = Random.Range(-randomY, randomY);
                tempSpread.y += randomY;
            }

            CurrentSpread = tempSpread;
        }
        private void OnCooldownEnded()
        {
            CurrentSpread = Vector2.zero;
        }
    }

    public class RecoilController : IInitializable, IRecoilController
    {
        private PlayerCharacterController _playerCharacterController;
        private WeaponConfig _weaponConfig;

        private Vector2 _currentRecoilForce;
        private int _currentFireCount;

        public event System.Action CooldDownEnded;

        public int CurrentFireCount {get { return _currentFireCount; } }

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
            _playerCharacterController.transform.Rotate(
                    new Vector3(0f, (_currentRecoilForce.x), 0), Space.Self);

            _currentFireCount++;
        }
        private void CalculateRecoil()
        {
            int randomSign = UnityEngine.Random.Range(0, 2) * 2 - 1; // Randomly generates -1 or 1

            _currentRecoilForce.y += (_weaponConfig.recoilForce.y + _weaponConfig.recoilDeltaOverTime.y) * _currentFireCount;
            _currentRecoilForce.x += (randomSign * (_weaponConfig.recoilForce.x + _weaponConfig.recoilDeltaOverTime.x) * _currentFireCount);

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
            _currentRecoilForce.y = 0;
            _currentRecoilForce.x = 0;
            _currentFireCount = 0;
            CooldDownEnded?.Invoke();
        }
    }
}