using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    public List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
    string dirName = "/Saves/";
    string fileName = "Save.json";


    private void Awake()
    {
        Load();
    }
    private void Save()
    {
        string dir = Application.persistentDataPath + dirName;
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        string json = JsonConvert.SerializeObject(leaderboardEntries);
        File.WriteAllText(dir + fileName, json);
    }
    public List<LeaderboardEntry> Load()
    {
        string path = Application.persistentDataPath + dirName + fileName;
        List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            entries = JsonConvert.DeserializeObject<List<LeaderboardEntry>>(json);
        }
        leaderboardEntries = entries;
        return entries;
    }

    public void AddAndSave(LeaderboardEntry leaderboardEntry) 
    {
        leaderboardEntries.Add(leaderboardEntry);
        leaderboardEntries.Sort((e1, e2) => e2.timeSurvived.CompareTo(e1.timeSurvived));
        Save();
    }
}
