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
    public Button buyButton;
    public Button equipButton;
    public TMPro.TextMeshProUGUI priceText;
    public TMPro.TextMeshProUGUI equipItem;
    GameItem thisItem;

    public enum PurchaseState
    {
        EQUIPED,
        EQUIP,
        PURCHASE
    }

    public void OnClickBuy(int state) //1 la purchase; 2 la equip
    {
        if (state == 1)
        {
            int price = thisItem.item.price;
            int currentGold = GameController.instance.goldNumber;
            if (currentGold >= price)
            {
                GameController.instance.ChangeGold(-price);
                ItemJSONDatabase.instance.UpdatePurchaseItem(thisItem);
                BuyStateUI(PurchaseState.EQUIP);
            }
        }
        if (state == 2)
        {
            Debug.Log("click");
            ItemJSONDatabase.instance.UpdateEquipItem(thisItem);
            CheckEquipItemState(thisItem.item.type);
            ShopController.instance.CreatItemInShop(thisItem.item.type);
        }
    }

    public void InitItemUI(GameItem item)
    {
        thisItem = item;
        itemImage.sprite = Resources.Load<Sprite>("UI/" + item.item.type + "/" + item.item.name);
        if (!item.Purchase)
        {
            BuyStateUI(PurchaseState.PURCHASE);
            priceText.text = item.item.price.ToString();
        }
        else
        {
            if (!item.Equip)
            {
               BuyStateUI(PurchaseState.EQUIP);
               equipButton.GetComponent<Button>().enabled = true;
            }
            else
            {
                BuyStateUI(PurchaseState.EQUIPED);
                equipButton.GetComponent<Button>().enabled = false;
            }
        }
    }
    private void CheckEquipItemState(string type)
    {
        List<GameItem> items = ItemJSONDatabase.instance.SplitTypeItem(type);
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != thisItem)
            {
                BuyStateUI(PurchaseState.EQUIP);
            }
            else
            {
                BuyStateUI(PurchaseState.EQUIPED);
            }
        }
    }
    public void BuyStateUI(PurchaseState state)
    {
        switch (state)
        {
            case PurchaseState.EQUIPED:
                lockItem.SetActive(false);
                buyButton.gameObject.SetActive(false);
                equipItem.text = "Equiped";
                equipButton.image.color = Color.gray;
                equipButton.gameObject.SetActive(true);
                break;
            case PurchaseState.EQUIP:
                lockItem.SetActive(false);
                buyButton.gameObject.SetActive(false);
                equipItem.text = "Equip";
                equipButton.image.color = Color.white;
                equipButton.gameObject.SetActive(true);
                break;
            case PurchaseState.PURCHASE:
                lockItem.SetActive(true);
                buyButton.gameObject.SetActive(true);
                equipButton.gameObject.SetActive(false);
                break;
        }
    }
}
