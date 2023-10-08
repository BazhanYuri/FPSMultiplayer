using Photon.Pun;
using System;
using UnityEngine;
using Unity.FPS.Enums;
using Zenject;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Runtime.CompilerServices;

namespace Unity.FPS.Multiplayer
{
    public class TeamScoreHanlder : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PhotonGameplayManager _photonManager;


        private const string BlueTeamScoreKey = "BlueTeamScore";
        private const string RedTeamScoreKey = "RedTeamScore";


        public int BlueTeamScore { get; private set; }
        public int RedTeamScore { get; private set; }

        public event Action ScoreUpdated;


      
        private void Awake()
        {
            Initialize();

            _photonManager.TeamWon += OnTeamWinned;
        }
        private void Initialize()
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(BlueTeamScoreKey, out object blueCountObj))
            {
                BlueTeamScore = (int)blueCountObj;
            }

            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(RedTeamScoreKey, out object redCountObj))
            {
                RedTeamScore = (int)redCountObj;
            }
        }

        private void OnTeamWinned(TeamType teamType)
        {

            if (teamType == TeamType.Blue)
            {
                BlueTeamScore++;
            }
            else if (teamType == TeamType.Red)
            {
                RedTeamScore++;
            }

            UpdateScoreCount();
        }

        private void UpdateScoreCount()
        {
            Debug.Log($"BlueTeamScore: {BlueTeamScore} - RedTeamScore: {RedTeamScore}");

               Hashtable ScoreCount = new Hashtable
               {
                   { BlueTeamScoreKey, BlueTeamScore },
                   { RedTeamScoreKey, RedTeamScore }
               };
               PhotonNetwork.CurrentRoom.SetCustomProperties(ScoreCount);
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            bool isHaveScore = false;

            if (propertiesThatChanged.ContainsKey(BlueTeamScoreKey))
            {
                BlueTeamScore = (int)propertiesThatChanged[BlueTeamScoreKey];
                isHaveScore = true;
            }
            if (propertiesThatChanged.ContainsKey(RedTeamScoreKey))
            {
                RedTeamScore = (int)propertiesThatChanged[RedTeamScoreKey];
                isHaveScore = true;
            }

            if (isHaveScore == true)
            {
                //Debug.Log($"BlueTeamScore: {BlueTeamScore} - RedTeamScore: {RedTeamScore}");
                ScoreUpdated?.Invoke();
            }

            
        }
    }
}
