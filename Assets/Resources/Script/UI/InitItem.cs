using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InitItem : MonoBehaviour
{
    public Image itemImage;
    public GameObject price;
    public TextMeshProUGUI priceText;
    public Button actionButton;
    public TextMeshProUGUI textButton;
    private int state = 0;
    // State = 1: Chua mua, State = 2 mua roi, chua mac, State = 3 mua roi - da mac
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitItemUI(GameItem item)
    {
        itemImage.sprite = Resources.Load <Sprite> ("UI/Hat/" + item.item.Id);
        if (item.Purchased == false)
        {
            state = 1;
            price.SetActive(true);
            priceText.text = item.item.Price.ToString();
        } else
        {
            price.SetActive(false);
            if (item.IsEquip)
            {
                state = 3;
            } else
            {
                state = 2;
            }
        }
        InitButtonState();
    }

    private void InitButtonState()
    {
        if (state == 1)
        {
            textButton.text = "Buy";
        } else if(state == 2)
        {
            textButton.text = "Equip";
        } else if(state == 3)
        {
            textButton.text = "Used";
        }
    }

}
