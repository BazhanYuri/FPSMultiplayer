using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using Unity.FPS.Enums;
using UnityEngine;
using Zenject;

namespace Unity.FPS.Multiplayer
{

    public class PhotonGameplayManager : IInitializable, IOnEventCallback, IPhotonManager
    {
        private const byte PlayerAddedEventCode = 2;


        private int blueTeamPlayerCount;
        private int redTeamPlayerCount;

        public int BlueTeamPlayerCount { get => blueTeamPlayerCount;}
        public int RedTeamPlayerCount { get => redTeamPlayerCount;}

        public event Action TeamCountUpdated;


        

        public void Initialize()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void OnDestroy()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }


        public void AddPlayerToTeam(TeamType teamType)
        {
            switch (teamType)
            {
                case TeamType.Blue:
                    blueTeamPlayerCount++;
                    break;
                case TeamType.Red:
                    redTeamPlayerCount++;
                    break;
            }

            SendPlayerAddedEvent();
        }
        private void SendPlayerAddedEvent()
        {
            // Send a custom event to notify other players of the added player
            Hashtable eventData = new Hashtable
            {
                { "blueTeamPlayerCount", blueTeamPlayerCount },
                { "redTeamPlayerCount", redTeamPlayerCount }
            };
            PhotonNetwork.RaiseEvent(
                PlayerAddedEventCode,
                eventData,
                new RaiseEventOptions { Receivers = ReceiverGroup.All },
                new SendOptions { Reliability = true }
            );
        }
        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == PlayerAddedEventCode)
            {
                // Received a player added event, handle it
                Hashtable eventData = (Hashtable)photonEvent.CustomData;

                blueTeamPlayerCount = (int)eventData["blueTeamPlayerCount"];
                redTeamPlayerCount = (int)eventData["redTeamPlayerCount"];

                TeamCountUpdated?.Invoke();
            }
        }

    }
}

