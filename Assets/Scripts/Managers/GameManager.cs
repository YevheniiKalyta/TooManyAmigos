using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerManager playerManager;
    public Transform PlayerTransform => playerManager.transform;

    private float startTime;
    private float survivedTime;
    private int enemiesKilled;

    public float SurvivedTime => survivedTime;
    public int EnemiesKilled => enemiesKilled;

    private void Awake()
    {
        EnemyManager.OnEnemyDeath += () => enemiesKilled++;
    }
    public void ResetGame()
    {
        playerManager.ResetPlayer();
        ObjectPooler.Instance.ResetPools();
        EnemySpawner.Instance.ToggleSpawn(true);
        enemiesKilled = 0;
        startTime = Time.time;
    }

    internal void TryToSaveProgress()
    {
        survivedTime = Time.time - startTime;
        List<LeaderboardEntry> leaderboardEntries = SaveLoadManager.Instance.leaderboardEntries;
        if (leaderboardEntries.Count < 10)
        {
            GameStateMachine.Instance.SwitchToState(GameState.PostGame);
            return;
        }
        for (int i = 0; i < leaderboardEntries.Count; i++)
        {
            if (survivedTime > leaderboardEntries[i].timeSurvived)
            {
                leaderboardEntries.RemoveAt(leaderboardEntries.Count - 1);
                GameStateMachine.Instance.SwitchToState(GameState.PostGame);
                return;
            }
        }
        GameStateMachine.Instance.SwitchToState(GameState.StandBy);

    }

    public void SaveProgress(string name)
    {
        LeaderboardEntry leaderboardEntry = new LeaderboardEntry(name, Mathf.FloorToInt(survivedTime), enemiesKilled);
        SaveLoadManager.Instance.AddAndSave(leaderboardEntry);
        GameStateMachine.Instance.SwitchToState(GameState.StandBy);
    }
}
