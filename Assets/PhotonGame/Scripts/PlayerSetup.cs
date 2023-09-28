using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public Movement movement;
    public Camera camera;




    private void Awake()
    {
        movement.enabled = false;
        camera.enabled = false;
    }
    public void SetAsLocalPlayer()
    {
        movement.enabled = true;
        camera.enabled = true;
    }
}
