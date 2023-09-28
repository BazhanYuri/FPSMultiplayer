using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
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
        GameObject _player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().SetAsLocalPlayer();
    }

}
