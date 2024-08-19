using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : Singleton<ShopController> 
{ 
    public GameObject itemPrefabs;
    public GameObject parents;

    public Button[] listButton;

    private static string[] listType = { "Weapons", "Hat", "Pants", "Shield" };
    // Start is called before the first frame update
    void Start()
    {
        //CreateItemHat(type);
        //CreateItemPants();  
        ClickButtonType(0);
        listButton[0].onClick.AddListener(() => ClickButtonType(0));
        listButton[1].onClick.AddListener(() => ClickButtonType(1));
        listButton[2].onClick.AddListener(() => ClickButtonType(2));
        listButton[3].onClick.AddListener(() => ClickButtonType(3));
    }

    private void ClickButtonType(int type)
    { 
        for(int i = 0; i < 4; i++)
        {
            listButton[i].GetComponent<Image>().color = Color.white;
        }
        listButton[type].GetComponent<Image>().color = Color.green;
        CreateItem(listType[type]);
    }

    public void CreateItem(string type)
    {
        foreach(Transform child in parents.transform)
        {
            Destroy(child.gameObject);
        }    

        List<GameItem> itemInGame = ItemJsonDatabase.Instance.GetAllItemOfType(type);
        for(int i = 0; i < itemInGame.Count; i++)
        {
            GameObject item = Instantiate(itemPrefabs, parents.transform);
            item.GetComponent<InitItem>().InitItemUI(itemInGame[i]);
        }
    }

    //private void CreateItemPants()
    //{
    //    List<GameItem> itemInGame = ItemJsonDatabase.Instance.GetAllItemOfType("Pants");
    //    for (int i = 0; i < itemInGame.Count; i++)
    //    {
    //        GameObject item = Instantiate(itemPrefabs, parents.transform);
    //        item.GetComponent<InitItem>().InitItemUI(itemInGame[i]);
    //    }
    //}
}
