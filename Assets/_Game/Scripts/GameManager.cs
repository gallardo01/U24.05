using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject Yellow;
    public int Score;

    public void Start()
    {
        UIManager.Ins.OpenUI<UIGameplay>();
        OnInit();
    }

    public void OnInit()
    {
        Score = 0;
    }

    public void AddScore(int score)
    {
        Score += score;
        UIManager.Ins.GetUI<UIGameplay>().UpdateTextScore();
    }
}
