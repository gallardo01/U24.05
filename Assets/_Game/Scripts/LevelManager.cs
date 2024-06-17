using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    public Level currentLevel;
    public int currentLevelIndex = 0;

    public void Start()
    {
        InitLevel();
    }

    public void InitLevel()
    {
        OnLoadLevel(currentLevelIndex);
        this.PostEvent(EventID.OnInitLevel);
    }

    public void OnLoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[level]);
    }

    public void NextLevel()
    {
        currentLevelIndex++;
        
        if (currentLevelIndex + 1 <= levels.Count)
        {
            InitLevel();
        }
        else
        {
            Debug.Log("Congrats");
        }       
    }
}
