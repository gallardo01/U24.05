using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameState gameState;

    void Start()
    {
        ChangeGameState(GameState.Mainmenu);
        LevelManager.Ins.OnLoadLevel(0);
        UIManager.Ins.OpenUI<UIMainmenu>();
    }

    public void ChangeGameState(GameState gameState)
    {
        this.gameState = gameState;
        this.PostEvent(EventID.OnGameStateChanged, gameState);
    }

    public bool IsState(GameState gameState) => this.gameState == gameState;
}
