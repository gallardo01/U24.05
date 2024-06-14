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
        OnInit();
    }

    //khoi tao trang thai bat dau game
    public void OnInit()
    {
        //OnLoadLevel(currentLevelIndex);
        //player.OnInit();
    }

    //reset trang thai khi ket thuc game
    public void OnReset()
    {
        //player.OnDespawn();
        //for (int i = 0; i < bots.Count; i++)
        //{
        //    bots[i].OnDespawn();
        //}

        //bots.Clear();
        //SimplePool.CollectAll();
    }

    //tao prefab level moi
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

    }

    public void NextLevel()
    {

    }
}
