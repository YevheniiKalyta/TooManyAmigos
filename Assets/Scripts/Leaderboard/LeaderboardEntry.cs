using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int timeSurvived;
    public int enemiesKilled;

    public LeaderboardEntry(string name, int time, int count)
    {
        playerName = name;
        timeSurvived = time;
        enemiesKilled = count;
    }


}
