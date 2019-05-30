using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager
{
    public static void SaveNewHighScore(LevelData levelData, int score)
    {
        int currentHighScore = LoadHighScore(levelData);
        PlayerPrefs.SetInt(levelData.name, Mathf.Max(currentHighScore, score));
        PlayerPrefs.Save();
    }

    public static int LoadHighScore(LevelData levelData)
    {
        if (PlayerPrefs.HasKey(levelData.name)) return PlayerPrefs.GetInt(levelData.name);
        return 0;
    }
}
