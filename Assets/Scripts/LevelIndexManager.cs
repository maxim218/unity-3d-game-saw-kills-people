using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelIndexManager {
    private const int Zero = 0;

    public static int GetLevelIndex() {
        const int defaultVal = Zero;
        int levelNumber = PlayerPrefs.GetInt("LEVEL_INDEX_NUMBER", defaultVal);
        return levelNumber;
    }

    public static void MakeNextLevelIndex() {
        int levelNumber = GetLevelIndex();
        int nextNewLevelIndex = 1 + levelNumber;
        PlayerPrefs.SetInt("LEVEL_INDEX_NUMBER", nextNewLevelIndex);
        PlayerPrefs.Save();
    }

    public static void ResetIndexToZero() {
        PlayerPrefs.SetInt("LEVEL_INDEX_NUMBER", Zero);
        PlayerPrefs.Save();
    }
}
