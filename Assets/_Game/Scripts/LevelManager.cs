using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new();
    public Level currentLevel;
    public int currentLevelIndex;


    public void OnLoadLevel(int levelIndex)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        if (levelIndex < levels.Count)
        {
            currentLevelIndex = levelIndex;
            currentLevel = Instantiate(levels[levelIndex]);
            currentLevel.InitLevel();
        }
    }

    public void PlayAgain()
    {
        OnLoadLevel(currentLevelIndex);
    }
}
