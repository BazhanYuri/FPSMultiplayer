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
            OnCooldownEnded();
            _weaponConfig = weaponConfig;
        }
        public void OnShoot()
        {
            CalculateSpread();
        }
        private void CalculateSpread()
        {
            Vector3 tempSpread = CurrentSpread;
            int currentFireCount = _recoilController.CurrentFireCount - 1;

            if (_weaponConfig.spreads.Length > currentFireCount)
            {
                ShootData spreadData = _weaponConfig.spreads[currentFireCount];
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
        private int _currentFireCount = 0;

        public event System.Action CooldDownEnded;

        public int CurrentFireCount {get { return _currentFireCount; } }

        public void Initialize()
        {

        }
        public void SetWeaponConfig(WeaponConfig weaponConfig)
        {
            _currentFireCount = 0;
            _currentRecoilForce.y = 0;
            _currentRecoilForce.x = 0;

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
            if (_weaponConfig.recoils.Length > CurrentFireCount)
            {
                ShootData recoilData = _weaponConfig.recoils[CurrentFireCount];
                _currentRecoilForce.x += recoilData.delta.x;

                float randomX = recoilData.randomize * recoilData.delta.x;
                randomX = Random.Range(-randomX, randomX);
                _currentRecoilForce.x += randomX;

                _currentRecoilForce.y += recoilData.delta.y;

                float randomY = recoilData.randomize * recoilData.delta.y;
                randomY = Random.Range(-randomY, randomY);
                _currentRecoilForce.y += randomY;
            }


            _playerCharacterController.StopAllCoroutines();
            _playerCharacterController.StartCoroutine(CoolDown());
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