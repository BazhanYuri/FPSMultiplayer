using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Multiplayer;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "FPS/GameConfig")]
public class GameConfig : ScriptableObject
{
    public int timeToRestartGameWhenGameWinned;
    public int maxBlueTeamPlayers;
    public int maxRedTeamPlayers;
    public Player bluePlayerPrefab;
    public Player redPlayerPrefab;
}
