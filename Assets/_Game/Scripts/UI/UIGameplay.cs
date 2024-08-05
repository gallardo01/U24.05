using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameplay : UICanvas
{
    [SerializeField] TextMeshProUGUI textBotAlive;

    public void UpdateTextBotAlive(int botAlive)
    {
        textBotAlive.text = botAlive.ToString();
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeGameState(GameState.Gameplay);
        UpdateTextBotAlive(LevelManager.Ins.currentLevel.alive);
    }
}
