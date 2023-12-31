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
        [SerializeField] private TeamWinScreen _teamWinScreenPrefab;
        [SerializeField] private PhotonGameplayManager _photonGameplayManagerPrefab;



        public override void InstallBindings()
        {
            BindConfigs();
            BindPhotonGameplayManager();
            BindEventBus();
            BindRecoilController();
            BindSprayController();
            BindUIFactories();
            BindUIBuilder();
            BindSpawnPointsHolder();
            BindPlayerSpawner();
            BindGameCore();
            BindWallet();
        }
        
        private void BindPhotonGameplayManager()
        {
            PhotonGameplayManager photonGameplayManager = Container.InstantiatePrefabForComponent<PhotonGameplayManager>(_photonGameplayManagerPrefab);
            Container.BindInterfacesTo<PhotonGameplayManager>().FromInstance(photonGameplayManager).AsSingle();
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
        private void BindRecoilController()
        {
            Container
                .BindInterfacesTo<RecoilController>()
                .AsSingle();
        }
        private void BindSprayController()
        {
            Container
                .BindInterfacesTo<SpreadController>()
                .AsSingle();
        }
        private void BindUIFactories()
        {
            Container.BindFactory<TeamChooserUI, TeamChooserUIFactory>()
                .FromComponentInNewPrefab(_teamChooserUIPrefab);
            Container.BindFactory<TeamWinScreen, TeamWinUIFactory>()
                .FromComponentInNewPrefab(_teamWinScreenPrefab);

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
        private void BindGameCore()
        {
            Container
                .BindInterfacesTo<GameCore>()
                .AsSingle();
        }
        private void BindWallet()
        {
            Container
                .BindInterfacesTo<Wallet>()
                .AsSingle();
        }
        
    }
}