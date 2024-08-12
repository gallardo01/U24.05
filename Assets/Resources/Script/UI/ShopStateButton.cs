using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] private TextMeshProUGUI text;

    private EquipmentContainer currentContainer;
    private ButtonState currentState;

    public void ChangeButtonState(EquipmentContainer container)
    {
        currentContainer = container;

        if (!currentContainer.IsBought) currentState = ButtonState.NeedBuy;
        if (currentContainer.IsBought) currentState = ButtonState.NeedEquip;
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
                Equipted();
                break;
        }
    }

    public void NeedBuy()
    {

    }
    public void NeedEquip()
    {

    }
    public void Equipted()
    {

    }

    public void EquipmentManagerCallBack()
    {
        EquipmentManager.Instance.SaveItemData(currentContainer);
    }
}
