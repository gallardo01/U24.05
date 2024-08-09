using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDefeat : UICanvas
{
    [SerializeField] Button btnContinue;
    [SerializeField] TextMeshProUGUI textRank, textKilledBy;

    private void Awake()
    {
        btnContinue.onClick.AddListener(() =>
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIMainmenu>();
            LevelManager.Ins.PlayAgain();
        });
    }

    private void UpdateTextRank(int rank)
    {
        textRank.text = "#" + rank.ToString();
    }

    private void UpdateTextKilledBy(string name)
    {
        textKilledBy.text = name;
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeGameState(GameState.Finish);
        UpdateTextRank(LevelManager.Ins.player.rank);
        UpdateTextKilledBy(LevelManager.Ins.player.killedBy);
    }
}
