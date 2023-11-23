using UnityEngine;

public class AITrainMoverManager : MonoBehaviour
{
    public AITrainMover[] aiTrainMovers;
    public Transform pointsForMovement;
    public TestLevelConfig testLevelConfig;

    private AITrainMovementPanelControllerUI _aiTrainMovementPanelControllerUi;



    private void Awake()
    {
        _aiTrainMovementPanelControllerUi = FindObjectOfType<AITrainMovementPanelControllerUI>();
        _aiTrainMovementPanelControllerUi.OnChangeEnableAI += OnAiTrainTogle;
        _aiTrainMovementPanelControllerUi.OnRespawnEnemies += RespawnAiMovers;
    }

    private void OnAiTrainTogle(bool value)
    {
        if (value)
        {
            StartMovingEnemies();
        }
        else
        {
            StopMovingEnemies();
        }
    }
    private void StartMovingEnemies()
    {
        foreach (var aiTrainMover in aiTrainMovers)
        {
            aiTrainMover.OnReachPoint += () =>
            {
                aiTrainMover.MoveToPoint(GetRandomPoint().position);
            };
            aiTrainMover.agent.speed = testLevelConfig.aiMovementData.speed;
            aiTrainMover.agent.isStopped = false;
            aiTrainMover.MoveToPoint(GetRandomPoint().position);
        }
    }
    private void StopMovingEnemies()
    {
        foreach (var aiTrainMover in aiTrainMovers)
        {
            aiTrainMover.agent.isStopped = true;
        }
    }
    private Transform GetRandomPoint()
    {
        return pointsForMovement.GetChild(UnityEngine.Random.Range(0, pointsForMovement.childCount));
    }
    private void RespawnAiMovers()
    {
        for (int i = 0; i < aiTrainMovers.Length; i++)
        {
            AITrainMover newaITrainMover = Instantiate(testLevelConfig.aiMovementData.aITrainMoverPrefab, aiTrainMovers[i].transform.position, aiTrainMovers[i].transform.rotation);
            Destroy(aiTrainMovers[i].gameObject);
            aiTrainMovers[i] = newaITrainMover;
        }
    }
}
