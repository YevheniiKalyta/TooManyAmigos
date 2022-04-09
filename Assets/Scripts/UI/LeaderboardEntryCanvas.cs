using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utilities;
using System;

public class LeaderboardEntryCanvas : MonoBehaviour
{
   [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI timeSurvivedText;
    [SerializeField] private TextMeshProUGUI killCountText;

    private void Start()
    {
        GameStateMachine.Instance.OnSwitchGameState += UpdateInfo;
    }

    private void UpdateInfo(GameState gameState)
    {
        if(gameState == GameState.PostGame)
        timeSurvivedText.text = "Time Survived - " + Utils.ToNormalTime(Mathf.FloorToInt(GameManager.Instance.SurvivedTime)).ToString();
        killCountText.text = "Enemies killed - " + GameManager.Instance.EnemiesKilled.ToString();
    }

    public void SubmitLeaderboardEntry()
    {
        GameManager.Instance.SaveProgress(inputField.text);
    }
}
