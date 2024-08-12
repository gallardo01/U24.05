using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : Singleton<ShopController>
{
    public GameObject itemPrefabs;
    public GameObject parent;

    public Button [] listButton;
    private static string[] listType = {"Weapons", "Hat", "Pants", "Shield"};
    
    // Start is called before the first frame update
    void Start()
    {
        CLickButtonType(0);
      listButton[0].onClick.AddListener( () => CLickButtonType(0));
      listButton[1].onClick.AddListener( () => CLickButtonType(1));
      listButton[2].onClick.AddListener( () => CLickButtonType(2));
      listButton[3].onClick.AddListener( () => CLickButtonType(3));

    }
    
    private void CLickButtonType(int type)
    {
        for (int i = 0; i < 4; i++)
        {
            listButton[i].GetComponent<Image>().color = Color.white;
        }
        
        listButton[type].GetComponent<Image>().color = Color.yellow;
       CreateItem(listType[type]);
    }
    

    public void CreateItem(string type)
    {
        ClearItem();
        List<GameItem> itemIngame = ItemJsonDatabase.Ins.GetAllItemOfType(type);
        for (int i = 0; i < itemIngame.Count; i++)
        {
            GameObject item = Instantiate(itemPrefabs, parent.transform);
            item.GetComponent<InitItem>().InitItemUI(itemIngame[i]);
        }
    }

    private void ClearItem()
    {
        foreach(Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }
    
}
