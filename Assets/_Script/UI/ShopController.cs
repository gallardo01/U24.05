using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static ItemJSONDatabase;

public class ShopController : MonoBehaviour
{
    [SerializeField] Transform contentUI;
    public static ShopController instance;
    public InitItem initItemPrefabs;
    public Button[] listButton;
    private static string[] listType = { "Head", "Pant", "Weapon", "Shield" };

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        CreatItemInShop(listType[0]);
        listButton[0].onClick.AddListener(() => OnClickButton(0));
        listButton[1].onClick.AddListener(() => OnClickButton(1));
        listButton[2].onClick.AddListener(() => OnClickButton(2));
        listButton[3].onClick.AddListener(() => OnClickButton(3));
    }
    public void CreatItemInShop(string type)
    {
        for (int i = 0; i < contentUI.childCount; i++)
        {
            Destroy(contentUI.GetChild(i).gameObject);
        }
        List<GameItem> gameItem = ItemJSONDatabase.instance.SplitTypeItem(type);
        bool check = ItemJSONDatabase.instance.CheckEquipItemForTheFirstTime(type);
        for (int i = 0; i < gameItem.Count; i++)
        {
            if (!check)
            {
                gameItem[0].Equip = true;
                InitItem gameObject = Instantiate(initItemPrefabs.gameObject, contentUI).GetComponent<InitItem>();
                gameObject.InitItemUI(gameItem[i]);
            } else
            {
                InitItem gameObject = Instantiate(initItemPrefabs.gameObject, contentUI).GetComponent<InitItem>();
                gameObject.InitItemUI(gameItem[i]);
            }
        }
    }

    public void OnClickButton(int type)
    {
        CreatItemInShop(listType[type]);
        for (int i = 0; i < listButton.Length; i++)
        {
            if (i == type)
            {
                listButton[i].image.color = Color.yellow;
            } else
            {
                listButton[i].image.color = Color.white;
            }
        }
    }
}
