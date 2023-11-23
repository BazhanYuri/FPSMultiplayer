using DG.Tweening;
using System;
using Unity.FPS.Enums;
using UnityEngine;
public class EnemiesShootTrainController : MonoBehaviour
{
    public TargetSpawner[] targetSpawners;
    public Transform[] obstacleRoots;


    public Transform root;
    public TestLevelConfig testLevelConfig;

    private ShootDistancePanelControllerUI _shootDistancePanelControllerUI;
    private int normalHeight;


    private void Awake()
    {
        _shootDistancePanelControllerUI = FindObjectOfType<ShootDistancePanelControllerUI>();
        normalHeight = (int)root.position.y;
        _shootDistancePanelControllerUI.OnChangeHighType += OnChangeHighType;
        _shootDistancePanelControllerUI.OnChangeIsMovingEnemies += OnMovingEnemiesTogleChanged;
        _shootDistancePanelControllerUI.OnChangeIsMovingObstacles += OnMovingObstacleTogleChanged;

    }

    private void OnChangeHighType(HighType type)
    {
        switch (type)
        {
            case HighType.Up:
                root.position = new Vector3(root.position.x, root.position.y + testLevelConfig.stepHeight, root.position.z);
                break;
            case HighType.Middle:
                root.position = new Vector3(root.position.x, normalHeight, root.position.z);
                break;
            case HighType.Down:
                root.position = new Vector3(root.position.x, root.position.y - testLevelConfig.stepHeight, root.position.z);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
    private void OnMovingEnemiesTogleChanged(bool value)
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
    private void OnMovingObstacleTogleChanged(bool value)
    {
        if (value)
        {
            StartObstacleMoving();
        }
        else
        {
            StopObstacleMoving();
        }
    }
    private void StartMovingEnemies()
    {
        foreach (var targetSpawner in targetSpawners)
        {
            float initialZ = targetSpawner.transform.localPosition.z;
            Sequence sequence = DOTween.Sequence();

            // Move from right to left based on initial Z position
            sequence.Append(targetSpawner.transform.DOLocalMoveZ(initialZ - testLevelConfig.enemiesMovingTrainData.amplitude, testLevelConfig.enemiesMovingTrainData.time));
            sequence.Append(targetSpawner.transform.DOLocalMoveZ(initialZ + testLevelConfig.enemiesMovingTrainData.amplitude * 2, testLevelConfig.enemiesMovingTrainData.time * 2));

            sequence.SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void StopMovingEnemies()
    {
        foreach (var targetSpawner in targetSpawners)
        {
            targetSpawner.transform.DOKill();
        }
    }
    private void StartObstacleMoving()
    {
        foreach (var item in obstacleRoots)
        {
            item.gameObject.SetActive(true);
            float initialZ = item.transform.localPosition.z;
            Sequence sequence = DOTween.Sequence();

            // Move from right to left based on initial Z position
            sequence.Append(item.transform.DOLocalMoveZ(initialZ - testLevelConfig.obstacleMovingTrainData.distance, testLevelConfig.obstacleMovingTrainData.time));
            sequence.Append(item.transform.DOLocalMoveZ(initialZ + testLevelConfig.obstacleMovingTrainData.distance * 2, testLevelConfig.obstacleMovingTrainData.time * 2));

            sequence.SetLoops(-1, LoopType.Yoyo);
        }
    }   
    private void StopObstacleMoving()
    {
        foreach (var item in obstacleRoots)
        {
            item.gameObject.SetActive(false);
            item.transform.DOKill();
        }
    }
}
