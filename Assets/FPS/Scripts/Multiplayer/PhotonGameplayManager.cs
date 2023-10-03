using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using Unity.FPS.Enums;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Unity.FPS.Multiplayer
{
    public class PhotonGameplayManager : MonoBehaviourPunCallbacks, IPhotonManager
    {
        private Player _player;

        private const string BlueTeamPlayerCountKey = "BlueTeamPlayerCount";
        private const string RedTeamPlayerCountKey = "RedTeamPlayerCount";

        public int BlueTeamPlayerCount { get; private set; }
        public int RedTeamPlayerCount { get; private set; }

        public event Action TeamCountUpdated;

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
        private void UpdateTeamsCount()
        {
            Hashtable playerCountProps = new Hashtable
            {
                { BlueTeamPlayerCountKey, BlueTeamPlayerCount },
                { RedTeamPlayerCountKey, RedTeamPlayerCount }
            };
            PhotonNetwork.CurrentRoom.SetCustomProperties(playerCountProps);

            SendPlayerAddedEvent();
        }
        private void SendPlayerAddedEvent()
        {
            // Notify all players of the updated player counts through room properties
            Hashtable eventData = new Hashtable
            {
                { BlueTeamPlayerCountKey, BlueTeamPlayerCount },
                { RedTeamPlayerCountKey, RedTeamPlayerCount }
            };
            PhotonNetwork.CurrentRoom.SetCustomProperties(eventData);
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            // Check if player count properties were updated
            if (propertiesThatChanged.ContainsKey(BlueTeamPlayerCountKey))
            {
                BlueTeamPlayerCount = (int)propertiesThatChanged[BlueTeamPlayerCountKey];
            }

            if (propertiesThatChanged.ContainsKey(RedTeamPlayerCountKey))
            {
                RedTeamPlayerCount = (int)propertiesThatChanged[RedTeamPlayerCountKey];
            }

            TeamCountUpdated?.Invoke();
        }
    }
}
