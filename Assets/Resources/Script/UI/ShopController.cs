using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public GameObject itemPrefabs;
    public GameObject parent;

    public Button weaponButton;
    public Button hatButton;
    public Button pantsButton;
    public Button leftHandButton;
    // Start is called before the first frame update
    void Start()
    {
        CreateItemHat();
        
    }

    private void CreateItemHat()
    {
        List<GameItem> itemIngame = ItemJsonDatabase.Ins.listItemInGame;
        for (int i = 0; i < itemIngame.Count; i++)
        {
            GameObject item = Instantiate(itemPrefabs, parent.transform);
            item.GetComponent<InitItem>().InitItemUI(itemIngame[i]);
        }
    }
}
