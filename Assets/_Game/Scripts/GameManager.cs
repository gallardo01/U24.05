using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject Yellow;

    private int score;
    public int Score => score;

    public void Start()
    {
        this.RegisterListener(EventID.OnInitLevel, (param) => OnInit());
        UIManager.Ins.OpenUI<UIGameplay>();
        OnInit();
    }

    public void OnInit()
    {
        score = 0;
        UIManager.Ins.GetUI<UIGameplay>().UpdateTextScore();
    }

    public void AddScore(int score)
    {
        this.score += score;
        UIManager.Ins.GetUI<UIGameplay>().UpdateTextScore();
    }
}
