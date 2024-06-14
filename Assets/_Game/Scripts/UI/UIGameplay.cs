using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameplay : UICanvas
{
    [SerializeField] TextMeshProUGUI textScore;

    private void Start()
    {
        updateTextScore();
    }
    public void updateTextScore()
    {
        textScore.text = GameController.Ins.Score.ToString();
    }
}
