using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BrickShop : MonoBehaviour
{
    [SerializeField] Button buyButton;
    [SerializeField] Button equipButton;
    [SerializeField] Button usedButton;
    [SerializeField] int brickNumber;
    [SerializeField] int gold;

    [SerializeField] TextMeshProUGUI goldText;
    // Start is called before the first frame update
    void Start()
    {
        buyButton.onClick.AddListener(() => BuyButton(gold));
        equipButton.onClick.AddListener(() => EquipButton());
        goldText.text = gold.ToString();
    }

    private void BuyButton(int gold)
    {
        if (gold > 0)
        {
            PlayerPrefs.SetInt("Brick_" + brickNumber, 1);
        }
        UIManager.Instance.InitShopItem();
    }

    private void EquipButton()
    {
        PlayerPrefs.SetInt("Pick", brickNumber);
        UIManager.Instance.InitShopItem();
    }

    public void InitButtonState(int state)
    {
        buyButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        usedButton.gameObject.SetActive(false);

        if (state == 1) // buy
        {
            buyButton.gameObject.SetActive(true);
        } else if (state == 2) { // equip
            equipButton.gameObject.SetActive(true);
        } else  // used
        {
            usedButton.gameObject.SetActive(true);
        }
    }
}
