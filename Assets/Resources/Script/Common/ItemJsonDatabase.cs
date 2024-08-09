using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System;

public class ItemJsonDatabase : Singleton<ItemJsonDatabase>
{
    
    private JsonData itemData;
    private JsonData inGameItemData;
    private List<Item> listItems = new List<Item>();
    public List<GameItem> listInGameItems = new List<GameItem>();
    private string filePath = "MyItem.txt";
    
    void Start()
    {
        LoadRescourcesFromTxt();
        ContructDatabase();
        LoadDataFromLocalDb();
    }

    private void LoadDataFromLocalDb()
    {
        string filePathFull = Application.persistentDataPath + "/" + filePath;
        Debug.Log(filePathFull);
        if(!File.Exists(filePathFull))
        {
            Debug.Log("Count: " + listInGameItems.Count);

            AddNewItemFirstTime();
            Save();
        }   
        else
        {
            byte[] jsonByte = null;
            try
            {
                jsonByte = File.ReadAllBytes(filePathFull);
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading file: " + e.Message);
            }
            
            string jsonData = Encoding.ASCII.GetString(jsonByte);
            listInGameItems =  JsonMapper.ToObject<List<GameItem>>(jsonData);
            ContructMyItemDb();
        }
    }

    private void Save()
    {
        string jsonData = JsonConvert.SerializeObject(listInGameItems.ToArray(), Formatting.Indented);
        string filePathFull = Application.persistentDataPath + "/" + filePath;
        byte[] jsonByte = Encoding.ASCII.GetBytes(jsonData);
        
        if(!Directory.Exists(Path.GetDirectoryName(filePathFull)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePathFull));
        }
        
        if(!File.Exists(filePathFull))
        {
            File.Create(filePathFull).Close();
        }
        
        try
        {
            File.WriteAllBytes(filePathFull, jsonByte);
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving file: " + e.Message);
        }
    }

    private void AddNewItemFirstTime()
    {
        for(int i = 0; i < listItems.Count; i++)
        {
            GameItem newGameItem = new GameItem();
            newGameItem.item = listItems[i];
            newGameItem.Purchased = false;
            newGameItem.isEquip = false;
            
            listInGameItems.Add(newGameItem);
        }
    }

    public class GameItem : Item
    {
        public bool Purchased { get; set; }
        public bool isEquip { get; set; }
        
        public Item item { get; set; }
    }
    void LoadRescourcesFromTxt()
    {
        string filePath = "StreamingAssets/item";
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        itemData = JsonMapper.ToObject(targetFile.text);
    }
    
    public void ContructDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            Item item = new Item();
            item.Id = (int)itemData[i]["Id"];
            item.Type = itemData[i]["Type"].ToString();
            item.Price = (int)itemData[i]["Price"];
            item.Atk = (int)itemData[i]["Atk"];
            item.Def = (int)itemData[i]["Def"];
            item.Spd = (int)itemData[i]["Spd"];
            
            listItems.Add(item);
        }
    }
    
    public void ContructMyItemDb()
    {
        for (int i = 0; i < inGameItemData.Count; i++)
        {
            GameItem gameItem = new GameItem();
            gameItem.item = new Item();
            gameItem.item.Id = (int)inGameItemData[i]["item"]["Id"];
            gameItem.item.Type = inGameItemData[i]["item"]["Type"].ToString();
            gameItem.item.Price = (int)inGameItemData[i]["item"]["Price"];
            gameItem.item.Atk = (int)inGameItemData[i]["item"]["Atk"];
            gameItem.item.Def = (int)inGameItemData[i]["item"]["Def"];
            gameItem.item.Spd = (int)inGameItemData[i]["item"]["Spd"];
            gameItem.Purchased = (bool)inGameItemData[i]["Purchased"];
            gameItem.isEquip = (bool)inGameItemData[i]["isEquip"];
            
            listInGameItems.Add(gameItem);
        }
        Save();
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
}

