using Photon.Pun;
using Unity.FPS.Enums;
using Unity.FPS.Game;
using Unity.FPS.UI;
using UnityEngine;
using Zenject;

namespace Unity.FPS.Multiplayer
{
    public class PlayerSpawner : IInitializable
    {
        private GameConfig _gameConfig;
        private IUIBuilder _uiBuilder;
        private SpawnPointsHolder _spawnPointsHolder;
        private IPhotonManager _photonManager;
        private EventBus _eventBus;


        [Inject]
        public void Construct(SpawnPointsHolder spawnPointsHolder, GameConfig gameConfig, IUIBuilder uIBuilder, EventBus eventBus, IPhotonManager photonManager)
        {
            _spawnPointsHolder = spawnPointsHolder;
            _gameConfig = gameConfig;
            _uiBuilder = uIBuilder;
            _eventBus = eventBus;
            _photonManager = photonManager;
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
            if (teamType == TeamType.Red)
            {
                _player = PhotonNetwork.Instantiate(_gameConfig.redPlayerPrefab.name, position, Quaternion.identity).GetComponent<Player>();
            }
            else
            {
                _player = PhotonNetwork.Instantiate(_gameConfig.bluePlayerPrefab.name, position, Quaternion.identity).GetComponent<Player>();
            }
            _player.Initialize(_eventBus);
            _player.SetAsLocalMultiplayer();
            _player.SetTeam(teamType);
            _photonManager.AddPlayerToTeam(teamType, _player);

            switch (teamType)
            {
                case TeamType.Blue:
                    position = _spawnPointsHolder.BlueTeamSpawnPoints[_photonManager.BlueTeamPlayerCount].transform.position;
                    _player.SetSpawnPoint(_spawnPointsHolder.BlueTeamSpawnPoints[_photonManager.BlueTeamPlayerCount]);
                    break;
                case TeamType.Red:
                    position = _spawnPointsHolder.RedTeamSpawnPoints[_photonManager.RedTeamPlayerCount].transform.position;
                    _player.SetSpawnPoint(_spawnPointsHolder.RedTeamSpawnPoints[_photonManager.BlueTeamPlayerCount]);
                    break;
                default:
                    break;
            }

            _player.transform.position = position;

            _eventBus.InvokePlayerSpawned();
        }
    }
}

