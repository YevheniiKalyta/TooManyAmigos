using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState : MonoBehaviour, IState
{
    [SerializeField] GameState gameState = GameState.Main;
    public void EnterState()
    {
        GameManager.Instance.ResetGame();
    }

    public void ExitState()
    {
        EnemySpawner.Instance.ToggleSpawn(false);
    }

    public GameState GetGameState()
    {
        return gameState;
    }
}
