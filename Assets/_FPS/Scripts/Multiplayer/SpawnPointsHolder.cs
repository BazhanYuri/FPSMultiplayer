using Photon.Pun;
using UnityEngine;


namespace Unity.FPS.Multiplayer
{
    public class SpawnPointsHolder : MonoBehaviour
    {
        [SerializeField] private SpawnPoint[] _blueTeamSpawnPoints;
        [SerializeField] private SpawnPoint[] _redTeamSpawnPoints;

        public SpawnPoint[] BlueTeamSpawnPoints { get => _blueTeamSpawnPoints; set => _blueTeamSpawnPoints = value; }
        public SpawnPoint[] RedTeamSpawnPoints { get => _redTeamSpawnPoints; set => _redTeamSpawnPoints = value; }

        public Transform GetFreeRedSpawnPoint()
        {
            return GetSpawnPointIfFree(_redTeamSpawnPoints);
        }
        public Transform GetFreeBlueSpawnPoint()
        {
            return GetSpawnPointIfFree(_blueTeamSpawnPoints);
        }
        public Transform GetSpawnPointIfFree(SpawnPoint[] spawnPoints)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                if (spawnPoint.IsFree)
                {
                    spawnPoint.GetComponent<PhotonView>().RPC("SetAsNotFree", RpcTarget.AllBufferedViaServer, false);
                    return spawnPoint.transform;
                }
            }
            return null;
        }
    }
}