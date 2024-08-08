using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

public class ItemJSONDatabase : MonoBehaviour
{
    private JsonData itemData;
    private List<Item> listItem = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        LoadResourceFromTxt();
        ConstructDatabase();
        Debug.Log(listItem[0].iD);
    }

    private void LoadResourceFromTxt()
    {
        string filepath = "StreamingAsset/Item";
        TextAsset targetFile = Resources.Load<TextAsset>(filepath);
        itemData = JsonMapper.ToObject(targetFile.text);

    }

    private void ConstructDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            listItem.Add(new Item()
            {
                iD = (int)itemData[i]["Id"],
                type = (string)itemData[i]["Type"],
                price = (int)itemData[i]["Price"],
                atk = (int)itemData[i]["Atk"],
                def = (int)itemData[i]["Def"],
                speed = (float)itemData[i]["Spd"]
            });
        }
    }
}

public class Item
{
    public int iD { get; set; }
    public string type { get; set; }
    public int price { get; set; }
    public int atk { get; set; }
    public int def { get; set; }
    public float speed { get; set; }
}
