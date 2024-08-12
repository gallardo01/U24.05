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

    private void Start()
    {
        string[] keys = ES3.GetKeys();
        if (keys.Length == 0)
        {
            Debug.Log("No keys found in the save file.");
        }
        else
        {
            Debug.Log("Keys found in the save file:");
            foreach (string key in keys)
            {
                Debug.Log(key);
            }
        }
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
        button.interactable = true;
        buttonText.text = currentContainer.Price.ToString();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => BuyCallBack());

        if(CurrencyManager.Instance.CurrentCurrency < currentContainer.Price)
        {
            this.gameObject.SetActive(false);
        }
        else this.gameObject.SetActive(true);
    }
    public void NeedEquip()
    {
        this.gameObject.SetActive(true);
        button.interactable = true;
        buttonText.text = "EQUIP";
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => EquipCallBack());

    }
    public void Equipped()
    {
        this.gameObject.SetActive(true);
        button.interactable = false;
        buttonText.text = "EQUIPPED !";
    }

    public void BuyCallBack()
    {
        ES3.Save<bool>("IsPurchase_" + currentContainer.ItemName.text, true);
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
            others[i].Equip(false);
        }
        currentContainer.Equip(true);

        EquipmentManager.Instance.SaveItemData(currentContainer);
        currentState = ButtonState.Equipted;
        Configue();
    }
}
