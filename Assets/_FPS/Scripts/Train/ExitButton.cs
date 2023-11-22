using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public Button Close;

    private void OnEnable()
    {
        Close.onClick.AddListener(Exit);
    }


    public void Exit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

