using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public bool isLeftMouseButton;
    public bool isRightMouseButton;
    public Vector3 mousePosition;

    void Start()
    {
        isLeftMouseButton = false;
        isRightMouseButton = false;
        mousePosition = Vector3.zero;
    }

    public void UpdateInput()
    {
        isLeftMouseButton = Input.GetMouseButton(0);
        isRightMouseButton = Input.GetMouseButton(1);
        mousePosition = Input.mousePosition;
    }
}
