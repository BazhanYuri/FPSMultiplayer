using Photon.Pun;
using UnityEngine;


namespace Unity.FPS.Multiplayer
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private PhotonView _photonView;

        public bool IsFree { get; set; } = true;



        [PunRPC]
        public void SetAsNotFree(bool value)
        {
            IsFree = false;
             
           // _photonView.RPC("SyncBoolValue", RpcTarget.AllBufferedViaServer, false);
        }

        private void SyncBoolValue(bool newValue)
        {
            IsFree = newValue;
           // Debug.Log(string.Format("ChatMessage {0} {1}", gameObject.name, " is busy"));
        }
    }
}

