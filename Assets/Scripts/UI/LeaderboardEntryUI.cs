using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Utilities;

public class LeaderboardEntryUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI name;
   [SerializeField] private TextMeshProUGUI time;
   [SerializeField] private TextMeshProUGUI killCount;

    public void SetLeaderboard(LeaderboardEntry leaderboardEntry)
    {
        name.text = leaderboardEntry.playerName;
        time.text = Utils.ToNormalTime(leaderboardEntry.timeSurvived);
        killCount.text = leaderboardEntry.enemiesKilled.ToString();
    }
}
