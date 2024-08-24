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
    [SerializeField] private GameObject coinIcon;

    private EquipmentContainer currentContainer;
    private ButtonState currentState;

    public void Configue()
    {

    }

    public void ChangeButtonState(EquipmentContainer container)
    {
        currentContainer = container;

        if (!currentContainer.IsPurchase) currentState = ButtonState.NeedBuy;
        if (currentContainer.IsPurchase) currentState = ButtonState.NeedEquip;
        if (currentContainer.IsEquip) currentState = ButtonState.Equipted;

        UpdateButtonBehavior();
    }

    public void UpdateButtonBehavior()
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
        coinIcon.SetActive(true);

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
        coinIcon.SetActive(false);
    }
    public void Equipped()
    {
        coinIcon.SetActive(false);
        button.interactable = false;
        buttonText.text = "EQUIPPED !";
    }

    public void BuyCallBack()
    {
        currentContainer.SetPurchase(true);
        CurrencyManager.Instance.SpendCurrency(currentContainer.Price);
        currentState = ButtonState.NeedEquip;
        UpdateButtonBehavior();
        SoundManager.CurrencyClick();
    }

    public void EquipCallBack()
    {
        EquipmentManager.Instance.OnContainerEquip(currentContainer);
        currentState = ButtonState.Equipted;
        UpdateButtonBehavior();
        SoundManager.CurrencyClick();
    }
}
