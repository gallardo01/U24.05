using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InitItem : MonoBehaviour
{
    public Image itemImage;

    public GameObject price;
    public TextMeshProUGUI textPrice;
    public Button actionButton;
    public TextMeshProUGUI textButton;

    public int state = 0;
    // state = 1 : chua mua , state = 2 : da mua , chua mac , state = 3 : da mua va mac
    // Start is called before the first frame update
    void Start()
    {
    }

    public void InitItemsUI(GameItem item)
    {
        itemImage.sprite = Resources.Load<Sprite>("UI/Hat/" + item.item.iD);
        if (item.Purchase == false)
        {
            state = 1;
            price.SetActive(true);
            textPrice.text = item.item.price.ToString();
        } else
        {
            price.SetActive(false);
            if (item.Equip)
            {
                state = 3;
            } else
            {
                state = 2;
            }
        }
        InitButtonState();
    }
    
    public void InitButtonState()
    {
        if (state == 1)
        {
            textButton.text = "Buy";
        } else if (state == 2)
        {
            textButton.text = "Equip";
        } else
        {
            textButton.text = "Used";
        }
    }
}
