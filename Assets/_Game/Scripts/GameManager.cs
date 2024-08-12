using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameState gameState;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        UIManager.Ins.OpenUI<UIMainmenu>();
        LevelManager.Ins.OnLoadLevel(0);

        //------------------------------------------------ Test gold
        if (!PlayerPrefs.HasKey(Constants.PP_CURRENT_GOLD))
        {
            DataManager.Ins.AdjustGold(5000);
        }
        //------------------------------------------------
    }

    public void ChangeGameState(GameState gameState)
    {
        this.gameState = gameState;
        this.PostEvent(EventID.OnGameStateChanged, gameState);
    }

    public bool IsState(GameState gameState) => this.gameState == gameState;
}
