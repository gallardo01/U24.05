using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AwardUI : MonoBehaviour
{
    public TextMeshProUGUI textGold;
    public TextMeshProUGUI textPosition;
    public Button claimButton;
    private int gold = 0;
    // Start is called before the first frame update
    void Start()
    {
        claimButton.onClick.AddListener(() => ClaimButtonClick());
    }

    public void InitAwardUI(int gold, int pos)
    {
        this.gold = gold;
        textGold.text = "x " + gold;
        textPosition.text = "You are at " + pos + "th";
    }
    
    private void ClaimButtonClick()
    {
        GameController.Instance.GainGold(gold);
        UIManager.Instance.MainMenuClick();
    }
}
