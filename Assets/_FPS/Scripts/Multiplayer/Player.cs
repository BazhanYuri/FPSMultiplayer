using Photon.Pun;
using System;
using Unity.FPS.Enums;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;


namespace Unity.FPS.Multiplayer
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private Camera[] _cameras;
        [SerializeField] private PlayerInputHandler _inputHandler;
        [SerializeField] private PlayerCharacterController _characterController;
        [SerializeField] private PlayerWeaponsManager _weaponsManager;
        [SerializeField] private Jetpack _jetpack;
        [SerializeField] private Health _health;
        [SerializeField] private Transform _blueTeamSkin;
        [SerializeField] private Transform _redTeamSkin;

        private TeamType _teamType;
        private Vector3 _skinSizeTemp = new Vector3(1.2033f, 1.2033f, 1.2033f);
        private EventBus _eventBus;
        private GameConfig _gameConfig;
        private PlayerSpawnInfo _playerSpawnInfo;
        private IRecoilController _recoilController;
        private ISpreadController _spreadController;

        public TeamType TeamType
        {
            get => _teamType;
            private set
            {
                _teamType = value;
                RefreshPlayer();
            }
        }
        public SpawnPoint SpawnPoint { get; private set; }
        public Health Health { get => _health; }
        public IRecoilController RecoilController { get => _recoilController;}
        public ISpreadController SpreadController { get => _spreadController; }
        public GameConfig GameConfig { get => _gameConfig; }
        public static Player Instance { get; set; }
        public PlayerWeaponsManager WeaponsManager { get => _weaponsManager;}

        private void OnDisable()
        {
            _eventBus.RoundCompleted -= RestartPlayer;
        }

        private void Awake()
        {
            Instance = this;
            DisableAll();
        }
        private void DisableAll()
        {
            foreach (var camera in _cameras)
            {
                camera.enabled = false;

                if (camera.TryGetComponent(out AudioListener component))
                {
                    component.enabled = false;
                }
            }
            _inputHandler.enabled = false;
            _characterController.enabled = false;
            _jetpack.enabled = false;
            _weaponsManager.enabled = false;
        }
        public void Initialize(EventBus eventBus, IRecoilController recoilController, ISpreadController spreadController, GameConfig gameConfig)
        {
            _eventBus = eventBus;
            _recoilController = recoilController;
            _spreadController = spreadController;
            _gameConfig = gameConfig;

            _eventBus.RoundCompleted += RestartPlayer;
            _eventBus.HalfRoundsPassed += SwitchTeam;
        }
        public void SetAsLocalMultiplayer()
        {
            foreach (var camera in _cameras)
            {
                camera.enabled = true;

                if (camera.TryGetComponent(out AudioListener component))
                {
                    component.enabled = true;
                }
            }
            _inputHandler.enabled = true;
            _characterController.enabled = true;
            _jetpack.enabled = true;
            _weaponsManager.enabled = true;

            RefreshPlayer();
        }
        public void SetTeam(TeamType teamType)
        {
            TeamType = teamType;
            _photonView.RPC("SyncTeamType", RpcTarget.All, teamType);
        }
        [PunRPC]
        private void SyncTeamType(TeamType teamType)
        {
            TeamType = teamType;
        }
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // Writing data to the network
                stream.SendNext(TeamType);
            }
            else
            {
                // Reading data from the network
                TeamType = (TeamType)stream.ReceiveNext();
            }
        }
        public void SetSpawnPoint(SpawnPoint spawnPoint, PlayerSpawnInfo playerSpawnInfo)
        {
            SpawnPoint = spawnPoint;
            _playerSpawnInfo = playerSpawnInfo;
        }
        private void RestartPlayer()
        {
            transform.position = SpawnPoint.transform.position;
            Health.Recover();
        }
        private void SwitchTeam()
        {
            switch (TeamType)
            {
                case TeamType.Blue:
                    TeamType = TeamType.Red;
                    SpawnPoint = _playerSpawnInfo.spawnPointsHolder.RedTeamSpawnPoints[_playerSpawnInfo.index];
                    break;
                case TeamType.Red:
                    TeamType = TeamType.Blue;
                    SpawnPoint = _playerSpawnInfo.spawnPointsHolder.BlueTeamSpawnPoints[_playerSpawnInfo.index];
                    break;
            }

            RestartPlayer();
            RefreshPlayer();
        }
        private void RefreshPlayer()
        {
            switch (TeamType)
            {
                case TeamType.Blue:
                    _blueTeamSkin.gameObject.SetActive(true);
                    _redTeamSkin.gameObject.SetActive(false);
                    break;
                case TeamType.Red:
                    _blueTeamSkin.gameObject.SetActive(false);
                    _redTeamSkin.gameObject.SetActive(true);
                    break;
            }

            // Disable the player's own skin if it's the local player.
            if (_photonView.IsMine)
            {
                if (TeamType == TeamType.Blue)
                {
                    _blueTeamSkin.gameObject.SetActive(false);
                }
                else
                {
                    _redTeamSkin.gameObject.SetActive(false);
                }
            }
        }
    }
}

