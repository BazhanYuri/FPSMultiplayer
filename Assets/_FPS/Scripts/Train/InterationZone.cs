using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;

public class InterationZone : MonoBehaviour
{
    private bool _isInteration;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacterController playerCharacterController))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _isInteration = true;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isInteration)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _isInteration = false;
        }
    }
}

