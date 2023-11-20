using Unity.FPS.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDamageConfig", menuName = "FPS/PlayerDamageConfig")]
public class PlayerDamageConfig : ScriptableObject
{
    public DamageMultiplier[] damageMultipliers;


    public float GetMultiplayer(PlayerPart playerPart)
    {
        foreach (var damageMultiplier in damageMultipliers)
        {
            if (damageMultiplier.playerPart == playerPart)
            {
                return damageMultiplier.multiplier;
            }
        }

        return 1;
    }
    [System.Serializable]
    public struct DamageMultiplier
    {
        public PlayerPart playerPart;
        public float multiplier;
    }
}
