using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class ItemJsonDatabase : MonoBehaviour
{
    private JsonData itemData;
    private List<Item> listItem = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        LoadResourceFromTxt();
        ConstructDatabase();
        Debug.Log(listItem[1].Id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadResourceFromTxt()
    {
        string filePath = "StreamingAsset/Item";
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        itemData = JsonMapper.ToObject(targetFile.text);
    }

    private void ConstructDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            Item item = new Item();
            item.Id = (int)itemData[i]["Id"];
            item.Type = (string)itemData[i]["Type"];
            item.Price = (int)itemData[i]["Price"];
            item.Atk = (int)itemData[i]["Atk"];
            item.Def = (int)itemData[i]["Def"];
            item.Spd = (int)itemData[i]["Spd"];
            listItem.Add(item); 
        }
    }
}

public class Item
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int Price { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Spd { get; set; }
}
