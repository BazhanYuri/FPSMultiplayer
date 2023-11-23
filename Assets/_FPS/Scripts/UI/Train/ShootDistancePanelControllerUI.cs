using System;
using Unity.FPS.Enums;
using UnityEngine;
using UnityEngine.UI;

public class ShootDistancePanelControllerUI : MonoBehaviour
{
    public Button UpMode;
    public Button NormalMode;
    public Button DownMode;
    public Toggle IsMovingEnemies;
    public Toggle IsRespawnEnemies;

    public event Action<HighType> OnChangeHighType;
    public event Action<bool> OnChangeIsMovingEnemies;
    public event Action<bool> OnChangeIsMovingObstacles;


    private void Awake()
    {
        UpMode.onClick.AddListener(() => OnChangeHighType?.Invoke(HighType.Up));
        NormalMode.onClick.AddListener(() => OnChangeHighType?.Invoke(HighType.Middle));
        DownMode.onClick.AddListener(() => OnChangeHighType?.Invoke(HighType.Down));
        IsMovingEnemies.onValueChanged.AddListener(OnMovingEnemiesChanged);
        IsRespawnEnemies.onValueChanged.AddListener(OnRespawnEnemiesChanged);
    }
    private void OnMovingEnemiesChanged(bool value)
    {
        OnChangeIsMovingEnemies?.Invoke(value);
    }
    private void OnRespawnEnemiesChanged(bool value)
    {
        OnChangeIsMovingObstacles?.Invoke(value);
    }
}

