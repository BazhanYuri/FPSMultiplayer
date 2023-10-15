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
    public Vector2 spreadForce = new Vector2(0, 0);
    public Vector2 spreadDeltaOverTime = new Vector2(0, 0);
    public Vector2 maxLimitForSpread;

    [Header("MAIN")]
    public float timeToRecover = 0.5f;
}