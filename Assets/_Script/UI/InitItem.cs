using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;
using static ItemJSONDatabase;
public class InitItem : MonoBehaviour
{
    public Image itemImage;
    public GameObject lockItem;
    public GameObject buyButton;
    public GameObject equipButton;
    public TMPro.TextMeshProUGUI priceText;
    public TMPro.TextMeshProUGUI equipItem;

    public enum PurchaseState
    {
        EQUIPED,
        EQUIP,
        PURCHASE
    }


    public void InitItemUI(GameItem item)
    {
        itemImage.sprite = Resources.Load<Sprite>("UI/Hat" + "/" + item.item.name);
        if (!item.Purchase)
        {
            BuyStateUI(PurchaseState.PURCHASE);
            priceText.text = item.item.price.ToString();
        }
        else
        {
            if (!item.Equip)
            {
               BuyStateUI(PurchaseState.EQUIPED);
               equipButton.GetComponent<Button>().enabled = false;
            }
            else
            {
                BuyStateUI(PurchaseState.EQUIP);
                equipButton.GetComponent<Button>().enabled = true;
            }
        }
    }

    public void BuyStateUI(PurchaseState state)
    {
        switch (state)
        {
            case PurchaseState.EQUIPED:
                lockItem.SetActive(false);
                buyButton.SetActive(false);
                equipItem.text = "Equiped";
                equipButton.SetActive(true);
                break;
            case PurchaseState.EQUIP:
                lockItem.SetActive(false);
                buyButton.SetActive(false);
                equipItem.text = "Equip";
                equipButton.SetActive(true);
                break;
            case PurchaseState.PURCHASE:
                lockItem.SetActive(true);
                buyButton.SetActive(true);
                equipButton.SetActive(false);
                break;
        }
    }
}
