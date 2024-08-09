using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject parents;
    // Start is called before the first frame update
    void Start()
    {
        CreateItemHat();
    }

    private void CreateItemHat()
    {
        List<GameItem> itemIngame = ItemJsonDatabase.Ins.listInGameItem;
        for (int i = 0; i < itemIngame.Count; i++)
        {
            if (itemIngame[i].item.type == "Hat")
            {
                GameObject item = Instantiate(itemPrefab, parents.transform);
                item.GetComponent<InitItem>().InitItemsUI(itemIngame[i]);
            }
        }
    }
}
