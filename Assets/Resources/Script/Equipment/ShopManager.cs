using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType 
{
    Weapon,
    Shield,
    Hat,
    Pant,
}

public class ShopManager : Singleton<ShopManager>, IGameStateListener
{
    [SerializeField] EquipmentData[] weaponsData;
    [SerializeField] EquipmentData[] shieldsData;
    [SerializeField] EquipmentData[] hatsData;
    [SerializeField] EquipmentData[] pantsData;

    [SerializeField] EquipmentContainer containerPrefab;

    [SerializeField] Transform weaponTab;
    [SerializeField] Transform shieldTab;
    [SerializeField] Transform hatTab;
    [SerializeField] Transform pantTab;

    public void CreatContainers()
    {
        for(int i = 0; i < weaponsData.Length; i++)
        {
            CreatContainer(weaponsData[i], weaponTab);
        }
        for (int i = 0; i < shieldsData.Length; i++)
        {
            CreatContainer(shieldsData[i], shieldTab);
        }
        for (int i = 0; i < hatsData.Length; i++)
        {
            CreatContainer(hatsData[i], hatTab);
        }
        for (int i = 0; i < pantsData.Length; i++)
        {
            CreatContainer(pantsData[i], pantTab);
        }
    }

    public void CreatContainer(EquipmentData data, Transform parentTab)
    {
        EquipmentContainer newContainer = Instantiate(containerPrefab, parentTab);
        newContainer.OnInit(data.itemName, data.itemType, data.itemPrefab, data.itemMat, data.itemIcon);
        newContainer.Button.onClick.AddListener(() => AddItem(newContainer));
    }

    public void AddItem(EquipmentContainer container)
    {
        GameObject newItem = container.Prefab;
        Material newMaterial = container.Mat;

        switch (container.ItemType)
        {
            case ItemType.Weapon:
                PlayersManager.Instance.player.CharacterEquipment.GetWeapon(newItem);
                break;                         
            case ItemType.Shield:              
                PlayersManager.Instance.player.CharacterEquipment.GetShield(newItem);
                break;                         
            case ItemType.Hat:                 
                PlayersManager.Instance.player.CharacterEquipment.GetHat(newItem);
                break;                         
            case ItemType.Pant:
                PlayersManager.Instance.player.CharacterEquipment.GetPant(newMaterial);
                break;
        }
    }

    public void OnGameStateChange(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.SHOP:
                if(weaponTab.childCount == 0)
                {
                    CreatContainers();
                }
                break;
        }
    }
}
