using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System;

// Lớp ItemJsonDatabase kế thừa từ Singleton<ItemJsonDatabase>
public class ItemJsonDatabase : Singleton<ItemJsonDatabase>
{
    // Dữ liệu JSON của các item
    private JsonData itemData;
    // Dữ liệu JSON của các item trong game
    private JsonData inGameItemData;
    // Danh sách các item
    public List<Item> listItem = new List<Item>();
    // Danh sách các item trong game
    public List<GameItem> listItemInGame = new List<GameItem>();
    // Đường dẫn file
    private string filePath = "MyItem.txt";
    // Thống kê người dùng
    public UserStats userStats = new UserStats();

    // Hàm Start() được gọi khi đối tượng được khởi tạo
    void Start()
    {
        // Tải tài nguyên từ file text
        LoadResourcesFromTxt();
        // Xây dựng cơ sở dữ liệu
        ConstructDatabase();
        // Tải dữ liệu từ cơ sở dữ liệu cục bộ
        LoadDataFromLocalDb();
        // Khởi tạo thống kê người dùng
        InitUserStats();
    }

    // Hàm InitUserStats() để khởi tạo thống kê người dùng
    private void InitUserStats()
    {
        userStats.Atk = 0;
        userStats.Def = 0;
        userStats.Speed = 0;

        // Cộng dồn các chỉ số từ các item đã trang bị
        for(int i = 0; i < listItemInGame.Count; i++)
        {
            if (listItemInGame[i].IsEquip == true)
            {
                userStats.Atk += listItemInGame[i].item.Atk;
                userStats.Def += listItemInGame[i].item.Def;
                userStats.Speed += listItemInGame[i].item.Spd;
            }
        }
        // Khởi tạo thống kê người dùng trong ShopController
        ShopController.Ins.InitUserStats();
    }

    // Hàm LoadDataFromLocalDb() để tải dữ liệu từ cơ sở dữ liệu cục bộ
    private void LoadDataFromLocalDb()
    {
        string filePathFull = Application.persistentDataPath + "/" + filePath;
        Debug.Log(filePathFull);
        if (!File.Exists(filePathFull))
        {
            // Nếu file chưa tồn tại, thêm item mới lần đầu và lưu lại
            AddNewItemFirstTime();
            Save();
        } else
        {
            // Nếu file đã tồn tại, đọc dữ liệu từ file
            byte[] jsonByte = null;
            try
            {
                jsonByte = File.ReadAllBytes(filePathFull);
            }
            catch
            {
            }
            string jsonData = Encoding.ASCII.GetString(jsonByte);
            inGameItemData = JsonMapper.ToObject(jsonData);
            ConstructMyItemDb();
        }
    }

    // Hàm LoadResourcesFromTxt() để tải tài nguyên từ file text
    private void LoadResourcesFromTxt()
    {
        string filePath = "StreamingAssets/item";
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        itemData = JsonMapper.ToObject(targetFile.text);
    }

    // Hàm AddNewItemFirstTime() để thêm item mới lần đầu
    private void AddNewItemFirstTime()
    {
        for (int i = 0; i < listItem.Count; i++)
        {
            GameItem newGameItem = new GameItem();
            newGameItem.item = listItem[i];
            newGameItem.Purchased = false;
            newGameItem.IsEquip = false;
            listItemInGame.Add(newGameItem);
        }
    }

    // Lớp UserStats để lưu trữ thống kê người dùng
    public class UserStats
    {
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Speed { get; set; }
    }

    // Hàm GetAllItemOfType() để lấy tất cả các item của một loại
    public List<GameItem> GetAllItemOfType(string type)
    {
        List<GameItem> listItem = new List<GameItem>();
        for (int i = 0; i < listItemInGame.Count; i++)
        {
            if (listItemInGame[i].item.Type == type)
            {
                listItem.Add(listItemInGame[i]);
            }
        }
        return listItem;
    }

    // Hàm GetIdOfItemsEquiped() để lấy ID của các item đã trang bị
    public int GetIdOfItemsEquiped(string type)
    {
        for (int i = 0; i < listItemInGame.Count; i++)
        {
            if (type == listItemInGame[i].item.Type && listItemInGame[i].IsEquip == true)
            {
                return listItemInGame[i].item.Id;
            }
        }
        return 0;
    }

    // Hàm Save() để lưu dữ liệu
    private void Save()
    {
        InitUserStats();
        string jsonData = JsonConvert.SerializeObject(listItemInGame.ToArray(), Formatting.Indented);
        string filePathFull = Application.persistentDataPath + "/" + filePath;
        byte[] jsonByte = Encoding.ASCII.GetBytes(jsonData);
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
        } catch (Exception e)
        {
            Debug.LogWarning("Cannot save" + e.Message);
        }
    }

    // Hàm EquipItem() để trang bị item
    public void EquipItem(GameItem item)
    {
        UnequipItem(item);
        for (int i = 0; i < listItemInGame.Count; i++)
        {
            if (item.item.Type == listItemInGame[i].item.Type && item.item.Id == listItemInGame[i].item.Id)
            {
                listItemInGame[i].IsEquip = true;
                break;
            }
        }
        Save();
    }

    // Hàm UnequipItem() để gỡ trang bị item
    public void UnequipItem(GameItem item)
    {
        for (int i = 0; i < listItemInGame.Count; i++)
        {
            if (item.item.Type == listItemInGame[i].item.Type)
            {
                listItemInGame[i].IsEquip = false;
            }
        }
        Save();
    }

    // Hàm PurchaseItem() để mua item
    public void PurchaseItem(GameItem item)
    {
        for (int i = 0; i < listItemInGame.Count; i++)
        {
            if (item.item.Type == listItemInGame[i].item.Type && item.item.Id == listItemInGame[i].item.Id)
            {
                listItemInGame[i].Purchased = true;
                break;
            }
        }
        Save();
    }

    // Hàm ConstructDatabase() để xây dựng cơ sở dữ liệu
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

    // Hàm ConstructMyItemDb() để xây dựng cơ sở dữ liệu item của người chơi
    private void ConstructMyItemDb()
    {
        for (int i = 0; i < inGameItemData.Count; i++)
        {
            GameItem gameItem = new GameItem();
            gameItem.item = new Item();
            gameItem.item.Id = (int)inGameItemData[i]["item"]["Id"];
            gameItem.item.Type = (string)inGameItemData[i]["item"]["Type"];
            gameItem.item.Price = (int)inGameItemData[i]["item"]["Price"];
            gameItem.item.Atk = (int)inGameItemData[i]["item"]["Atk"];
            gameItem.item.Def = (int)inGameItemData[i]["item"]["Def"];
            gameItem.item.Spd = (int)inGameItemData[i]["item"]["Spd"];
            gameItem.IsEquip = (bool)inGameItemData[i]["IsEquip"];
            gameItem.Purchased = (bool)inGameItemData[i]["Purchased"];
            listItemInGame.Add(gameItem);
        }
    }
}

// Lớp GameItem để lưu trữ thông tin item trong game
public class GameItem
{
    public bool Purchased { get; set; }
    public bool IsEquip { get; set; }
    public Item item { get; set; }
}

// Lớp Item để lưu trữ thông tin item
public class Item
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int Price { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Spd { get; set; }
}