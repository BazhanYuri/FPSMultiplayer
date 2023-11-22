using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Multiplayer;
using UnityEngine;

public class CanvasCameraSetter : MonoBehaviour
{
    public Canvas canvas;



    private void Update()
    {
        if (canvas.worldCamera != null)
        {
            return;
        }
        TrySetCamera();
    }
    private void TrySetCamera()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            canvas.worldCamera = player.GetCamera();
        }
    }
}
