using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : Singleton<ShopController>
{
    public GameObject itemPrefabs;
    public GameObject parent;
    public Button backButton;

    public Button[] listButton;
    public TextMeshProUGUI[] userStats;

    private static string[] listType = { "Weapons", "Hat", "Pants", "Shield" };
    // 0 weapons 1 hat 2 pants 3 shield
    // Start is called before the first frame update
    void Start()
    {
        ClickButtonType(0);
        listButton[0].onClick.AddListener(() => ClickButtonType(0));
        listButton[1].onClick.AddListener(() => ClickButtonType(1));
        listButton[2].onClick.AddListener(() => ClickButtonType(2));
        listButton[3].onClick.AddListener(() => ClickButtonType(3));

        backButton.onClick.AddListener(() => CloseShop());
        InitUserStats();
    }
    
    public void InitUserStats()
    {
        userStats[0].text = "ATK: " + ItemJsonDatabase.Instance.userStats.Atk;
        userStats[1].text = "DEF: " + ItemJsonDatabase.Instance.userStats.Def;
        userStats[2].text = "SPD: " + ItemJsonDatabase.Instance.userStats.Speed;
    }
    private void CloseShop()
    {
        Debug.Log("press");
        UIManager.Instance.InitGameState(1);
    }

    private void ClickButtonType(int type)
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
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
        List<GameItem> itemIngame = ItemJsonDatabase.Instance.GetAllItemOfType(type);
        for (int i = 0; i < itemIngame.Count; i++)
        {
            GameObject item = Instantiate(itemPrefabs, parent.transform);
            item.GetComponent<InitItem>().InitItemUI(itemIngame[i]);
        }
    }
}
