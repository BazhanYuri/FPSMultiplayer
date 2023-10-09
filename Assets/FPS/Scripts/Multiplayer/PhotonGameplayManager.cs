using Photon.Pun;
using System;
using Unity.FPS.Enums;
using UnityEngine;
using Zenject;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Unity.FPS.Multiplayer
{
    public class PhotonGameplayManager : MonoBehaviourPunCallbacks, IPhotonManager
    {
        public TeamScoreHanlder teamScoreHanlder;

        private Player _player;
        private GameConfig _gameConfig;

        private const string BlueTeamPlayerCountKey = "BlueTeamPlayerCount";
        private const string RedTeamPlayerCountKey = "RedTeamPlayerCount";
        private const string DeadBlueTeamPlayerCountKey = "DeadBlueTeamPlayerCount";
        private const string DeadRedTeamPlayerCountKey = "DeadRedTeamPlayerCount";

        public int BlueTeamPlayerCount { get; private set; }
        public int RedTeamPlayerCount { get; private set; }
        public int DiedBlueTeamPlayerCount { get; private set; }
        public int DiedRedTeamPlayerCount { get; private set; }

        public TeamScoreHanlder  TeamScoreHanlder { get { return teamScoreHanlder; }}

        public GameConfig GameConfig { get => _gameConfig;}

        public event Action TeamCountUpdated;
        public event Action<TeamType> TeamWon;




        [Inject]
        public void Construct(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }
        public override void OnEnable()
        {
            base.OnEnable();
            PhotonNetwork.AddCallbackTarget(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            PhotonNetwork.RemoveCallbackTarget(this);
        }
        private void Awake()
        {
            InitializePlayerCounts();
        }
        private void InitializePlayerCounts()
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(BlueTeamPlayerCountKey, out object blueCountObj))
            {
                BlueTeamPlayerCount = (int)blueCountObj;
            }

            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(RedTeamPlayerCountKey, out object redCountObj))
            {
                RedTeamPlayerCount = (int)redCountObj;
            }

            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(DeadBlueTeamPlayerCountKey, out object deadBlueCountObj))
            {
                DiedBlueTeamPlayerCount = (int)deadBlueCountObj;
            }
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(DeadRedTeamPlayerCountKey, out object deadRedCountObj))
            {
                DiedRedTeamPlayerCount = (int)deadRedCountObj;
            }

            UpdateTeamsCount();

            TeamCountUpdated?.Invoke();
        }
        public void AddPlayerToTeam(TeamType teamType, Player player)
        {
            if (PhotonNetwork.LocalPlayer == null)
            {
                Debug.LogWarning("PhotonNetwork.LocalPlayer is null. Cannot add player to team.");
                return;
            }
            _player = player;
            _player.Health.OnDie += OnPlayerDied;
            // Update the player count based on the chosen team
            if (teamType == TeamType.Blue)
            {
                BlueTeamPlayerCount++;
            }
            else if (teamType == TeamType.Red)
            {
                RedTeamPlayerCount++;
            }

            // Update room properties with the new player counts
            UpdateTeamsCount();
        }
        private void OnPlayerDied()
        {
            switch (_player.TeamType)
            {
                case TeamType.Blue:
                    DiedBlueTeamPlayerCount++;
                    break;
                case TeamType.Red:
                    DiedRedTeamPlayerCount++;
                    break;
                default:
                    break;
            }

            UpdateDeadTeamsCount();
        }
        private void UpdateTeamsCount()
        {
            Hashtable playerCountProps = new Hashtable
            {
                { BlueTeamPlayerCountKey, BlueTeamPlayerCount },
                { RedTeamPlayerCountKey, RedTeamPlayerCount }
            };
            PhotonNetwork.CurrentRoom.SetCustomProperties(playerCountProps);
        }
        

        private void UpdateDeadTeamsCount()
        {
            Hashtable DeadplayerCountProps = new Hashtable
            {
                { DeadBlueTeamPlayerCountKey, DiedBlueTeamPlayerCount },
                { DeadRedTeamPlayerCountKey, DiedRedTeamPlayerCount }
            };
            PhotonNetwork.CurrentRoom.SetCustomProperties(DeadplayerCountProps);
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            // Check if player count properties were updated
            CheckTeams(propertiesThatChanged);
            CheckDeathTeams(propertiesThatChanged);
        }
        private void CheckTeams(Hashtable propertiesThatChanged)
        {
            if (propertiesThatChanged.ContainsKey(BlueTeamPlayerCountKey))
            {
                BlueTeamPlayerCount = (int)propertiesThatChanged[BlueTeamPlayerCountKey];
                TeamCountUpdated?.Invoke();
            }
            if (propertiesThatChanged.ContainsKey(RedTeamPlayerCountKey))
            {
                RedTeamPlayerCount = (int)propertiesThatChanged[RedTeamPlayerCountKey];
                TeamCountUpdated?.Invoke();
            }

        }
        private void CheckDeathTeams(Hashtable propertiesThatChanged)
        {
            if (propertiesThatChanged.ContainsKey(DeadBlueTeamPlayerCountKey))
            {
                DiedBlueTeamPlayerCount = (int)propertiesThatChanged[DeadBlueTeamPlayerCountKey];
            }
            if (propertiesThatChanged.ContainsKey(DeadRedTeamPlayerCountKey))
            {
                DiedRedTeamPlayerCount = (int)propertiesThatChanged[DeadRedTeamPlayerCountKey];
            }
            CalculateEndMatchResult();
        }
        private void CalculateEndMatchResult()
        {
            if (BlueTeamPlayerCount <= 0 || RedTeamPlayerCount <= 0)
            {
                return;
            }

            TeamType winners = TeamType.None;
            if (DiedBlueTeamPlayerCount == BlueTeamPlayerCount)
            {
                winners = TeamType.Red;
            }
            else if (DiedRedTeamPlayerCount == RedTeamPlayerCount)
            {
                winners = TeamType.Blue;
            }
            if (winners == TeamType.None)
            {
                return;
            }

            TeamWon?.Invoke(winners);
            DiedBlueTeamPlayerCount = 0;
            DiedRedTeamPlayerCount = 0;
        }
    }
}
