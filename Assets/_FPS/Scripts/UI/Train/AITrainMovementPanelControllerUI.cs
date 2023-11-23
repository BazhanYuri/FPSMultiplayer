using System;
using UnityEngine;
using UnityEngine.UI;

public class AITrainMovementPanelControllerUI : MonoBehaviour
{
    public Toggle EnableAI;
    public Button RespawnEnemies;
    public event Action<bool> OnChangeEnableAI;
    public event Action OnRespawnEnemies;



    private void Awake()
    {
        EnableAI.onValueChanged.AddListener(OnEnableAIChanged);
        RespawnEnemies.onClick.AddListener(OnRespawnEnemiesClick);
    }
    private void OnEnableAIChanged(bool value)
    {
        OnChangeEnableAI?.Invoke(value);
    }
    public void OnRespawnEnemiesClick()
    {
        OnRespawnEnemies?.Invoke();
    }
    
}

