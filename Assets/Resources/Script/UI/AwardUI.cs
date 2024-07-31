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
    // Start is called before the first frame update
    void Start()
    {
        claimButton.onClick.AddListener(ClaimButtonClick);
    }
    public void InitAwardUI(int gold, int position)
    {
        textGold.text = gold.ToString();
        textPosition.text = position.ToString();
    }

    private void ClaimButtonClick()
    {
        
    }
}
