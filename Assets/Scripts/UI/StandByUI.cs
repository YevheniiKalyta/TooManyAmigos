using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandByUI : MonoBehaviour
{
   [SerializeField] private List<LeaderboardEntryUI> leaderboardEntryUIs;
    [SerializeField] private GameObject leaderboardPanel;

    private void Start()
    {
        PopulateLeaderboard();
    }
    private void OnEnable()
    {
        PopulateLeaderboard();
    }

    private void PopulateLeaderboard()
    {
        foreach (var leaderboardEntryUI in leaderboardEntryUIs)
        {
            leaderboardEntryUI.gameObject.SetActive(false);
        }
        List<LeaderboardEntry> leaderboardEntries = SaveLoadManager.Instance.leaderboardEntries;
        if (leaderboardEntries.Count == 0)
        {
            leaderboardPanel.SetActive(false);
            return;
        }
        leaderboardPanel.SetActive(true);
        int entriesCount = Mathf.Min(leaderboardEntryUIs.Count, leaderboardEntries.Count);
        for (int i = 0; i < entriesCount; i++)
        {
            leaderboardEntryUIs[i].gameObject.SetActive(true);
            leaderboardEntryUIs[i].SetLeaderboard(leaderboardEntries[i]);
        }
    }
}
