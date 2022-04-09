using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PostGameGameState : MonoBehaviour, IState
{
    [SerializeField] GameState gameState = GameState.StandBy;
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup canvasGroup;

    public void EnterState()
    {
        canvas.gameObject.SetActive(true);
        StartCoroutine(Utils.LerpCanvasGroup(canvasGroup, 1, 0.2f));
    }
    public void ExitState()
    {
        StartCoroutine(Utils.LerpCanvasGroup(canvasGroup, 0, 0.2f, () => canvas.gameObject.SetActive(false)));
    }

    public GameState GetGameState()
    {
        return gameState;
    }
}
