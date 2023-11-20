using System;
using Unity.FPS.Enums;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerDamageConfig))]
public class PlayerDamageConfigEditor : Editor
{
    private SerializedProperty damageMultipliers;

    private void OnEnable()
    {
        damageMultipliers = serializedObject.FindProperty("damageMultipliers");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (GUILayout.Button("Add All Parts"))
        {
            AddAllParts();
        }

        EditorGUILayout.PropertyField(damageMultipliers, true);

        serializedObject.ApplyModifiedProperties();
    }

    private void AddAllParts()
    {
        PlayerDamageConfig damageConfig = (PlayerDamageConfig)target;

        if (damageConfig.damageMultipliers == null)
        {
            damageConfig.damageMultipliers = new PlayerDamageConfig.DamageMultiplier[0];
        }

        // Clear existing elements
        damageConfig.damageMultipliers = new PlayerDamageConfig.DamageMultiplier[0];

        // Add all parts automatically
        foreach (PlayerPart part in Enum.GetValues(typeof(PlayerPart)))
        {
            PlayerDamageConfig.DamageMultiplier newMultiplier = new PlayerDamageConfig.DamageMultiplier
            {
                playerPart = part,
                multiplier = 1f // You can set the default multiplier here
            };

            ArrayUtility.Add(ref damageConfig.damageMultipliers, newMultiplier);
        }
    }
}
