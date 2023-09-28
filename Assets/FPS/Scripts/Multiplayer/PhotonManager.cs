using Photon.Pun;
using UnityEngine;


namespace Unity.FPS.Multiplayer
{
    public class PhotonManager : MonoBehaviourPunCallbacks
    {
        public Player playerPrefab;
        public Transform spawnPoint;

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

            PhotonNetwork.JoinOrCreateRoom("test", null, null);

            Debug.Log("Connected to room");
        }
        public override void OnJoinedRoom()
        {
            Player _player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity).GetComponent<Player>();
            _player.SetAsLocalMultiplayer();
        }
    }
}

