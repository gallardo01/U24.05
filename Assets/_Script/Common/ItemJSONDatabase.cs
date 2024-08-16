using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using UnityEditorInternal;

public class ItemJSONDatabase : MonoBehaviour
{
    public static ItemJSONDatabase instance;
    private JsonData itemData;
    private JsonData inGameItemData;
    private List<Item> listItem = new List<Item>();
    public List<GameItem> listInGameItem = new List<GameItem>();
    private string filePath = "MyItem.txt";

    private void Awake()
    {
        instance = this;
    }
    public class GameItem
    {
        public bool Purchase { get; set; }
        public bool Equip { get; set; }
        public Item item { get; set; }
    }
    public class Item
    {
        public int id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int speed { get; set; }
    }
    void Start()
    {
        LoadResourceFromTxt();
        ConstructDatabase();
        LoadDataFromLocalDb();
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
                id = (int)itemData[i]["id"],
                type = (string)itemData[i]["type"],
                name = (string)itemData[i]["name"],
                price = (int)itemData[i]["price"],
                atk = (int)itemData[i]["atk"],
                def = (int)itemData[i]["def"],
                speed = (int)itemData[i]["speed"]
            });
        }
    }
    private void LoadDataFromLocalDb()
    {
        string filePathFull = Application.persistentDataPath + "/" + filePath;
        if (File.Exists(filePathFull))
        {
            byte[] jsonByte = null;
            try
            {
                jsonByte = File.ReadAllBytes(filePathFull);
            } catch
            {

            }
            string jsonData = Encoding.ASCII.GetString(jsonByte);
            inGameItemData = JsonMapper.ToObject(jsonData);
            ConstructMyItemDatabase();
        }
        else
        {
            AddNewItemFirstTime();
            SaveDataToLocalDb();
        }
    }
    private void AddNewItemFirstTime()
    {
        for (int i = 0; i < listItem.Count; i++)
        {
            GameItem gameItem = new GameItem();
            gameItem.item = listItem[i];
            gameItem.Purchase = false;
            gameItem.Equip = false;
            listInGameItem.Add(gameItem);
        }
    }
    //Chuyen du lieu tu game obj thanh Json
    private void SaveDataToLocalDb()
    {
        string JsonData = JsonConvert.SerializeObject(listInGameItem.ToArray(),Formatting.Indented);
        string filePathFull = Application.persistentDataPath + "/" + filePath;
        Debug.Log(filePathFull);
        byte[] jsonByte = Encoding.ASCII.GetBytes(JsonData);
        if (!Directory.Exists(Path.GetDirectoryName(filePathFull)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePathFull));
        }
        if (!File.Exists(filePathFull))
        {
            File.Create(filePathFull).Close(); 
        }
        try
        {
            File.WriteAllBytes(filePathFull, jsonByte);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    private void ConstructMyItemDatabase()
    {
        for (int i = 0; i < inGameItemData.Count; i++)
        {
            listInGameItem.Add(new GameItem()
            {
                Purchase = (bool)inGameItemData[i]["Purchase"],
                Equip = (bool)inGameItemData[i]["Equip"],
                item = new Item()
                {
                    id = (int)inGameItemData[i]["item"]["id"],
                    type = (string)inGameItemData[i]["item"]["type"],
                    name = (string)inGameItemData[i]["item"]["name"],
                    price = (int)inGameItemData[i]["item"]["price"],
                    atk = (int)inGameItemData[i]["item"]["atk"],
                    def = (int)inGameItemData[i]["item"]["def"],
                    speed = (int)inGameItemData[i]["item"]["speed"]
                }               
            });
        }
    }

    public string CheckEquipItem(string type)
    {
        List<GameItem> list = SplitTypeItem(type);
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Equip == true)
            {
                return list[i].item.name;
            }
        }
        return "Null";
    }
    public List<GameItem> SplitTypeItem(string type)
    {
        List<GameItem> list = new List<GameItem>();
        for (int i = 0; i < listInGameItem.Count; i++)
        {
            if (type == listInGameItem[i].item.type)
            {
                list.Add(listInGameItem[i]);
            }
        }
        return list;
    }

    public void UpdatePurchaseItem(GameItem item)
    {
        for (int i = 0; i < listInGameItem.Count; i++)
        {
            if (item.item.type == listInGameItem[i].item.type && item.item.name == listInGameItem[i].item.name)
            {
                listInGameItem[i].Purchase = true;
                break;
            }
        }
        SaveDataToLocalDb();
    }
    public void UpdateEquipItem(GameItem item)
    {
        for (int i = 0; i < listInGameItem.Count; i++)
        {
            if (item.item.type == listInGameItem[i].item.type)
            {
                if(item.item.name == listInGameItem[i].item.name)
                {
                    listInGameItem[i].Equip = true;
                }
                else
                {
                    listInGameItem[i].Equip = false;
                }
            }
        }
        SaveDataToLocalDb();
    }
}


