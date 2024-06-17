using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    public Level currentLevel;
    public int currentLevelIndex = 0;

    [SerializeField] Player player;

    public void Start()
    {       
        OnInit();
    }

    public void OnInit()
    {
        OnLoadLevel(currentLevelIndex);
        player.OnInit(currentLevel.startPos);
    }

    public void OnLoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[level]);
    }

    public void ResetLevel()
    {
        GameManager.Ins.OnInit();
        OnInit();
    }

    public void NextLevel()
    {
        currentLevelIndex++;
        
        if (currentLevelIndex + 1 <= levels.Count)
        {
            ResetLevel();
        }
        else
        {
            Debug.Log("Congrats");
        }       
    }
}
