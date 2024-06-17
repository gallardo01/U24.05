using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameplay : UICanvas
{
    [SerializeField] TextMeshProUGUI textScore;

    public override void Open()
    {
        base.Open();
        UpdateTextScore();
    }

    public void UpdateTextScore()
    {
        textScore.text = GameManager.Ins.Score.ToString();
    }
}
