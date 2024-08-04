using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AwardUI : MonoBehaviour
{
    public TextMeshProUGUI textGold;

    public TextMeshProUGUI textPosition;

    public Button claimButton;

    private int gold = 0;
    // Start is called before the first frame update
    void Start()
    {
        claimButton.onClick.AddListener( () => ClaimButtonClick());
    }
    public void InitAwardUI(int gold, int position)
    {
        this.gold = gold;
        textGold.text = " x " + gold;
        textPosition.text = " You are at " + position + "th position";
    }

    private void ClaimButtonClick()
    {
        GameController.Ins.GainGold(gold);
        UIManager.Ins.MainMenuClick();
    }
}
