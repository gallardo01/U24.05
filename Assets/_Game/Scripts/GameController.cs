using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public GameObject Yellow;
    public int Score;

    private void Start()
    {
        Score = 0;
        UIManager.Ins.OpenUI<UIGameplay>();
    }

    public void AddScore(int score)
    {
        Score += score;
        UIManager.Ins.GetUI<UIGameplay>().updateTextScore();
    }
}
