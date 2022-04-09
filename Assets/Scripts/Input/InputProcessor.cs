using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProcessor : MonoBehaviour, IInputProcessor
{
    protected bool canHandleInput;
    private void Awake()
    {
        GameStateMachine.Instance.OnSwitchGameState += ToggleInput;
    }
    public virtual void ProcessInput()
    {
    }

    public virtual bool ShouldProcessInput()
    {
        return canHandleInput;
    }

    public virtual void ToggleInput(GameState gameState)
    {
        canHandleInput = gameState == GameState.Main;
    }
}
