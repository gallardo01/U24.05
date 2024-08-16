using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopStateButton : MonoBehaviour
{
    public enum ButtonState
    {
        NeedBuy,
        NeedEquip,
        Equipted,
    }

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    private EquipmentContainer currentContainer;
    private ButtonState currentState;

    public void ChangeButtonState(EquipmentContainer container)
    {
        currentContainer = container;

        if (!currentContainer.IsPurchase) currentState = ButtonState.NeedBuy;
        if (currentContainer.IsPurchase) currentState = ButtonState.NeedEquip;
        if (currentContainer.IsEquip) currentState = ButtonState.Equipted;

        Configue();
    }

    public void Configue()
    {
        switch(currentState)
        {
            case ButtonState.NeedBuy:
                NeedBuy();
                break;
            case ButtonState.NeedEquip:
                NeedEquip();
                break;
            case ButtonState.Equipted:
                Equipped();
                break;
        }
    }

    public void NeedBuy()
    {
        buttonText.text = currentContainer.Price.ToString();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => BuyCallBack());

        if (CurrencyManager.Instance.CurrentCurrency < currentContainer.Price)
        {
            button.interactable = false;
            buttonText.text = "Not Enough";
        }
        else
        {
            button.interactable = true;
            buttonText.text = currentContainer.Price.ToString();
        }
    }
    public void NeedEquip()
    {
        button.interactable = true;
        buttonText.text = "EQUIP";
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => EquipCallBack());

    }
    public void Equipped()
    {
        button.interactable = false;
        buttonText.text = "EQUIPPED !";
    }

    public void BuyCallBack()
    {
        currentContainer.SavePurchase();
        CurrencyManager.Instance.SpendCurrency(currentContainer.Price);
        currentState = ButtonState.NeedEquip;
        Configue();
    }

    public void EquipCallBack()
    {
        List<EquipmentContainer> others = new List<EquipmentContainer>();
        List<EquipmentContainer> containers = EquipmentManager.Instance.containerList;

        for(int i = 0; i < containers.Count; i++)
        {
            if( containers[i].ItemType == currentContainer.ItemType )
            {
                others.Add(containers[i]);
            }
        }
        for(int i = 0;i < others.Count;i++)
        {
            others[i].SaveEquipped(false);
        }
        currentContainer.SaveEquipped(true);

        EquipmentManager.Instance.SaveItemData(currentContainer);
        EquipmentManager.Instance.AddItemToPlayer();
        currentState = ButtonState.Equipted;
        Configue();
    }
}
