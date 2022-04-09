using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonsProcessor : InputProcessor, IInputProcessor
{
    public static Action OnReloadButton;
    public override void ProcessInput()
    {
        if (InputManager.Instance.ReloadPressed())
        {
            OnReloadButton?.Invoke();
        }
        if (InputManager.Instance.ExitButtonPressed())
        {
            Application.Quit();
        }
    }

    public override bool ShouldProcessInput()
    {
        return true;
    }

}
