using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UILose : UICanvas
{
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] Button btnPlayAgain;
    private void Awake()
    {
        btnPlayAgain.onClick.AddListener(() =>
        {
            CloseDirectly();
            LevelManager.Ins.ResetLevel();
        });
    }

    public override void Open()
    {
        base.Open();
        textScore.text = GameManager.Ins.Score.ToString();
    }
}
