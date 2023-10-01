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
        private EventBus _eventBus;


        [Inject]
        public void Construct(SpawnPointsHolder spawnPointsHolder, GameConfig gameConfig, IUIBuilder uIBuilder, EventBus eventBus)
        {
            _spawnPointsHolder = spawnPointsHolder;
            _gameConfig = gameConfig;
            _uiBuilder = uIBuilder;
            _eventBus = eventBus;
        }

        public void Initialize()
        {
            _uiBuilder.TeamChooserUI.TeamButtonClicked += OnPlayerSelectedRoom;
        }
        private void OnPlayerSelectedRoom(TeamType teamType)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Vector3 position;
            if (teamType == TeamType.Red)
            {
                position = _spawnPointsHolder.RedTeamSpawnPoints[PhotonNetwork.LocalPlayer.ActorNumber].transform.position;
                Player _player = PhotonNetwork.Instantiate(_gameConfig.redPlayerPrefab.name, position, Quaternion.identity).GetComponent<Player>();
                _player.SetAsLocalMultiplayer();
            }
            else
            {
                position = _spawnPointsHolder.BlueTeamSpawnPoints[PhotonNetwork.LocalPlayer.ActorNumber].transform.position;
                Player _player = PhotonNetwork.Instantiate(_gameConfig.bluePlayerPrefab.name, position, Quaternion.identity).GetComponent<Player>();
                _player.SetAsLocalMultiplayer();
            }

            _eventBus.InvokePlayerSpawned();
        }
    }
}

