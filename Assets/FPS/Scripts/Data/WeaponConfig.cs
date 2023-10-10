using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "FPS/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    public Vector2 recoilForce = new Vector2(0, 1);
    public float recoilDeltaOverTime = 1.1f;
    public Vector2 maxLimitForRecoil;
}