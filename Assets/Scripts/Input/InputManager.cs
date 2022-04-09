using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public Vector3 GetPlayerMovement()
    {
        Vector3 movementInput = new Vector3(GetWalkSidewaysValue(), 0, GetMovementWalkForwardValue());
        return movementInput.magnitude > 1 ? movementInput.normalized : movementInput;
    }

    public bool ReloadPressed()
    {
        return Input.GetKeyDown(KeyCode.R);
    }
    public bool ExitButtonPressed()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    public bool FirePressedContinually()
    {
        return Input.GetMouseButton(0);
    }
    public bool FireReleased()
    {
        return Input.GetMouseButtonUp(0);
    }

    public Vector3 GetMousePositionWorld()
    {
        Vector2 mousePos = GetMousePosition();
        return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.transform.position.y));
    }

    private float GetWalkSidewaysValue()
    {
        return Input.GetAxis("Horizontal");
    }

    private float GetMovementWalkForwardValue()
    {
        return Input.GetAxis("Vertical");
    }
    public Vector2 GetMousePosition()
    {
        return Input.mousePosition;
    }
}
