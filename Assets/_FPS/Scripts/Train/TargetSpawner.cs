using System.Collections;
using Unity.FPS.Game;
using Unity.FPS.Multiplayer;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public TestLevelConfig testLevelConfig;




    private void Start()
    {
        RespawnTarget();
    }
    private void RespawnTarget()
    {
        StartCoroutine(Respawn());
    }
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(testLevelConfig.timeToRespawnEnemies);
        SpawnTarget();
    }
    private void SpawnTarget()
    {
        CharacterController player = Instantiate(testLevelConfig.trainTarget, transform.position, transform.rotation);
        player.transform.SetParent(transform);
        if (testLevelConfig.isRespawnEnemies)
        {
            player.GetComponent<Health>().OnDie += RespawnTarget;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }
}
