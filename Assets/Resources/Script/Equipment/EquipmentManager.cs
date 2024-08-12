using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType 
{
    Weapon,
    Shield,
    Hat,
    Pant,
}

public class EquipmentManager : Singleton<EquipmentManager>, IGameStateListener
{
    [SerializeField] EquipmentDataSO[] weaponsData;
    [SerializeField] EquipmentDataSO[] shieldsData;
    [SerializeField] EquipmentDataSO[] hatsData;
    [SerializeField] EquipmentDataSO[] pantsData;

    [SerializeField] EquipmentContainer containerPrefab;

    [SerializeField] Transform weaponTab;
    [SerializeField] Transform shieldTab;
    [SerializeField] Transform hatTab;
    [SerializeField] Transform pantTab;


    [SerializeField] ShopStateButton stateButton;

    private GameObject equiptedWeapon;
    private GameObject equiptedShield;
    private GameObject equiptedHat;
    private Material equiptedPant;

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

    public void CreatContainer(EquipmentDataSO data, Transform parentTab)
    {
        EquipmentContainer newContainer = Instantiate(containerPrefab, parentTab);
        newContainer.OnInit(data);
        newContainer.Button.onClick.AddListener(() => SaveItemData(newContainer));
    }

    public void SaveItemData(EquipmentContainer container)
    {
        switch (container.ItemType)
        {
            case ItemType.Weapon:
                equiptedWeapon = container.Prefab;
                break;
            case ItemType.Shield:
                equiptedShield = container.Prefab;
                break;
            case ItemType.Hat:
                equiptedHat = container.Prefab;
                break;
            case ItemType.Pant:
                equiptedPant = container.Material;
                break;
        }
    }

    public void AddItem()
    {
        PlayersManager.Instance.player.CharacterEquipment.GetWeapon(equiptedWeapon);
        PlayersManager.Instance.player.CharacterEquipment.GetShield(equiptedShield);
        PlayersManager.Instance.player.CharacterEquipment.GetHat(equiptedHat);
        PlayersManager.Instance.player.CharacterEquipment.GetPant(equiptedPant);
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
            case GameState.GAME:
                AddItem();
                break;
        }
    }
}
