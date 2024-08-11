using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public InitItem initItemPrefabs;
    [SerializeField] Transform contentUI;
    // Start is called before the first frame update
    void Start()
    {
        CreatHatItemInShop();
    }

    private void CreatHatItemInShop()
    {
        for (int i = 0; i < ItemJSONDatabase.instance.listInGameItem.Count; i++)
        {
            InitItem gameObject = Instantiate(initItemPrefabs.gameObject, contentUI).GetComponent<InitItem>();
            gameObject.InitItemUI(ItemJSONDatabase.instance.listInGameItem[i]);
        }
    }
}
