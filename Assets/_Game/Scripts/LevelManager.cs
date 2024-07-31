using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new();
    public Level currentLevel;
    public int currentLevelIndex;

    public Player player;

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

        InitPlayer();
    }

    public void InitPlayer()
    {
        player.gameObject.SetActive(true);
        player.InitCharacter(WeaponType.Axe, 0);
        player.tf.position = currentLevel.GetRandomNodeStart().position;
    }

    public void PlayAgain()
    {
        currentLevel.ResetLevel();
        player.ResetCharacter();
        InitPlayer();
    }
}

