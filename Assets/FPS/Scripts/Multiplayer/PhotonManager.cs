using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.FPS.Multiplayer
{
    public class PhotonManager : MonoBehaviourPunCallbacks
    {
        public event Action JoinedRoom;

        void Start()
        {
            Debug.Log("Connecting...");

            PhotonNetwork.ConnectUsingSettings();
        }
        public override void OnConnectedToMaster()
        {
            base.OnConnected();
            Debug.Log("Connected to server");

            PhotonNetwork.JoinLobby();
        }
        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();

            PhotonNetwork.JoinOrCreateRoom("test2", null, null);

            Debug.Log("Connected to room");
        }
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            PhotonNetwork.LoadLevel("MainScene");
        }
       
    }
}

