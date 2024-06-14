using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameplay : UICanvas
{
    [SerializeField] TextMeshProUGUI textScore;
  
    public void updateTextScore()
    {
        textScore.text = GameManager.Ins.Score.ToString();
    }
}
