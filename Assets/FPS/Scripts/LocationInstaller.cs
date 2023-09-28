using UnityEngine;
using Zenject;

namespace Unity.FPS.Gameplay
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private PlayerCharacterController _playerPrefab;
        [SerializeField] private Transform _startPoint;


        
        public override void InstallBindings()
        {
            BindPlayer();
        }
        private void BindPlayer()
        {
            PlayerCharacterController player = Container.InstantiatePrefabForComponent<PlayerCharacterController>(_playerPrefab, _startPoint.position, Quaternion.identity, null);

            Container
                .Bind<PlayerCharacterController>()
                .FromInstance(player)
                .AsSingle();
        }
    }
}

