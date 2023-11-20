using Unity.FPS.Multiplayer;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "FPS/GameConfig")]
public class GameConfig : ScriptableObject
{
    public int countsOfRounds;
    public int timeToRestartGameWhenGameWinned;
    public int maxBlueTeamPlayers;
    public int maxRedTeamPlayers;
    public Player playerPrefab;

    [Header("For testing")]
    public bool enableTrace;
    public float traceTime;
    public float traceWidth;
    public Color traceColor;
    public Material traceMaterial;
}
