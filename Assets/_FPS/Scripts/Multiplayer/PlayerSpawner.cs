using Photon.Pun;
using Unity.FPS.Enums;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using Unity.FPS.UI;
using UnityEngine;
using Zenject;

namespace Unity.FPS.Multiplayer
{
    public struct PlayerSpawnInfo
    {
        public SpawnPointsHolder spawnPointsHolder;
        public int index;
    }
    public class PlayerSpawner : IInitializable
    {
        private GameConfig _gameConfig;
        private IUIBuilder _uiBuilder;
        private SpawnPointsHolder _spawnPointsHolder;
        private IPhotonManager _photonManager;
        private IRecoilController _recoilController;
        private EventBus _eventBus;


        [Inject]
        public void Construct(SpawnPointsHolder spawnPointsHolder, GameConfig gameConfig, IUIBuilder uIBuilder, EventBus eventBus, IPhotonManager photonManager, IRecoilController recoilController)
        {
            _spawnPointsHolder = spawnPointsHolder;
            _gameConfig = gameConfig;
            _uiBuilder = uIBuilder;
            _eventBus = eventBus;
            _photonManager = photonManager;
            _recoilController = recoilController;
        }

        public void Initialize()
        {
            _uiBuilder.TeamChooserUI.TeamButtonClicked += OnPlayerSelectedRoom;
        }
        private void OnPlayerSelectedRoom(TeamType teamType)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Player _player;
            Vector3 position = Vector3.zero;
            _player = PhotonNetwork.Instantiate(_gameConfig.playerPrefab.name, position, Quaternion.identity).GetComponent<Player>();
            _player.Initialize(_eventBus, _recoilController);
            _player.SetAsLocalMultiplayer();
            _player.SetTeam(teamType);
            _photonManager.AddPlayerToTeam(teamType, _player);


            PlayerSpawnInfo playerSpawnInfo = new PlayerSpawnInfo();
            playerSpawnInfo.spawnPointsHolder = _spawnPointsHolder;

            switch (teamType)
            {
                case TeamType.Blue:
                    playerSpawnInfo.index = _photonManager.BlueTeamPlayerCount;
                    position = _spawnPointsHolder.BlueTeamSpawnPoints[_photonManager.BlueTeamPlayerCount].transform.position;
                    _player.SetSpawnPoint(_spawnPointsHolder.BlueTeamSpawnPoints[_photonManager.BlueTeamPlayerCount], playerSpawnInfo);
                    break;
                case TeamType.Red:
                    playerSpawnInfo.index = _photonManager.RedTeamPlayerCount;
                    position = _spawnPointsHolder.RedTeamSpawnPoints[_photonManager.RedTeamPlayerCount].transform.position;
                    _player.SetSpawnPoint(_spawnPointsHolder.RedTeamSpawnPoints[_photonManager.BlueTeamPlayerCount], playerSpawnInfo);
                    break;
                default:
                    break;
            }

            _player.transform.position = position;

            _eventBus.InvokePlayerSpawned();
        }
    }
}

