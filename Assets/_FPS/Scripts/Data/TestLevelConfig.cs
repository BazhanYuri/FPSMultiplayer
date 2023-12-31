﻿using Unity.FPS.Multiplayer;
using UnityEngine;

[CreateAssetMenu(fileName = "TestLevelConfig", menuName = "FPS/TestLevelConfig")]
public class TestLevelConfig : ScriptableObject
{
    public CharacterController trainTarget;
    public int stepHeight;
    public float timeToRespawnEnemies;
    public bool isRespawnEnemies;
    public EnemiesMovingTrainData enemiesMovingTrainData;
    public ObstacleMovingTrainData obstacleMovingTrainData;
    public AIMovementData aiMovementData;
}

[System.Serializable]
public struct EnemiesMovingTrainData
{
    public float amplitude;
    public float time;
}
[System.Serializable]
public struct ObstacleMovingTrainData
{
    public float time;
    public float distance;
}
[System.Serializable]
public struct AIMovementData
{
    public AITrainMover aITrainMoverPrefab;
    public float speed;
}