using UnityEngine;
using GD.MinMaxSlider;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "FPS/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    [Header("Recoil")]
    public Vector2 recoilForce = new Vector2(0, 1);
    public Vector2 recoilDeltaOverTime = new Vector2(0, 0);
    public Vector2 maxLimitForRecoil;
    [Header("Spread")]
    public Spread[] spreads;

    [Header("MAIN")]
    public float timeToRecover = 0.5f;
}

[System.Serializable]
public struct Spread
{
    public Vector2 delta;
    [Range (0, 1)] public float randomize;
}