using UnityEngine;
using GD.MinMaxSlider;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "FPS/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    [Header("Recoil")]
    public ShootData[] recoils;
    [Header("Spread")]
    public ShootData[] spreads;

    [Header("MAIN")]
    public float timeToRecover = 0.5f;
    public float standartReloadTime = 1f;
}

[System.Serializable]
public struct ShootData
{
    public Vector2 delta;
    [Range (0, 1)] public float randomize;
}