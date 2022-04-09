using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    StandBy,
    Main,
    PostGame

}
public class GameStateMachine : Singleton<GameStateMachine>
{
    [SerializeField] private IState[] states;
    [SerializeField]
    private IState currentState;
    public Action<GameState> OnSwitchGameState;

    public GameState CurrentGameState { get => currentState.GetGameState(); }

    private void Start()
    {
        states = GetComponents<IState>();
        currentState = GetIStateByGameState(GameState.StandBy);
        currentState.EnterState();
        PlayerManager.OnPlayerDeath += () => GameManager.Instance.TryToSaveProgress();
    }

    public void SwitchToState(GameState gameState)
    {
        if (gameState != currentState.GetGameState())
        {
           currentState?.ExitState(); 
            foreach (var state in states)
            {
                if (gameState == state.GetGameState())
                {
                    currentState = state;
                    break;
                }
            }
            currentState.EnterState();
            OnSwitchGameState?.Invoke(gameState);
        }
    }

    public IState GetIStateByGameState(GameState gameState)
    {
        foreach (var state in states)
        {
            if (state.GetGameState() == gameState) return state;
        }
        Debug.LogError($"[{gameState}]: GameState not found");
        return null;
    }
}
