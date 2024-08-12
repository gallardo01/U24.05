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

    public virtual void InitItemUI(GameItem item)
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
    
    public class InitWeapon : InitItem
    {
        public override void InitItemUI(GameItem item)
        {
            itemImage.sprite = Resources.Load<Sprite>("UI/Weapon/" + item.item.Id);
            base.InitItemUI(item);
        }
    }

    public class InitPant : InitItem
    {
        public override void InitItemUI(GameItem item)
        {
            itemImage.sprite = Resources.Load<Sprite>("UI/Pant/" + item.item.Id);
            base.InitItemUI(item);
        }
    }

    public class InitLeftHand : InitItem
    {
        public override void InitItemUI(GameItem item)
        {
            itemImage.sprite = Resources.Load<Sprite>("UI/LeftHand/" + item.item.Id);
            base.InitItemUI(item);
        }
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
