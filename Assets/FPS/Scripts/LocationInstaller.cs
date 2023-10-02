using UnityEngine;
using Zenject;
using Unity.FPS.Multiplayer;
using Unity.FPS.UI;
using Unity.FPS.Game;

namespace Unity.FPS.Gameplay
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private SpawnPointsHolder _spawnPointsHolder;
        [Header("Configs")]
        [SerializeField] private GameConfig _gameConfig;
        [Header("UI")]
        [SerializeField] private TeamChooserUI _teamChooserUIPrefab;



        public override void InstallBindings()
        {
            BindConfigs();
            BindPhotonGameplayManager();
            BindEventBus();
            BindUIFactories();
            BindUIBuilder();
            BindSpawnPointsHolder();
            BindPlayerSpawner();
        }
        
        private void BindPhotonGameplayManager()
        {
            Container
                .BindInterfacesTo<PhotonGameplayManager>()
                .AsSingle();
        }
        private void BindConfigs()
        {
            Container
                .Bind<GameConfig>()
                .FromInstance(_gameConfig)
                .AsSingle();
        }
        private void BindEventBus()
        {
            Container
                .Bind<EventBus>()
                .AsSingle();
        }
        private void BindUIFactories()
        {
            Container.BindFactory<TeamChooserUI, TeamChooserUIFactory>()
                .FromComponentInNewPrefab(_teamChooserUIPrefab);
        }
        private void BindUIBuilder()
        {
            Container
                .BindInterfacesTo<UIBuilder>()
                .AsSingle();
        }
        private void BindSpawnPointsHolder()
        {
            Container
                .Bind<SpawnPointsHolder>()
                .FromInstance(_spawnPointsHolder)
                .AsSingle();
        }
        private void BindPlayerSpawner()
        {
            Container
                .BindInterfacesTo<PlayerSpawner>()
                .AsSingle();
        }
    }
}