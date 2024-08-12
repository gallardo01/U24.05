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
    [HideInInspector] public List<EquipmentContainer> containerList = new List<EquipmentContainer>();

    [SerializeField] Transform weaponTab;
    [SerializeField] Transform shieldTab;
    [SerializeField] Transform hatTab;
    [SerializeField] Transform pantTab;


    [SerializeField] ShopStateButton stateButton;

    private GameObject equiptWeapon;
    private GameObject equiptShield;
    private GameObject equiptHat;
    private Material equiptPant;

    private void Start()
    {
        equiptWeapon = weaponsData[0].itemPrefab;
        equiptShield = shieldsData[0].itemPrefab;
        equiptHat = hatsData[0].itemPrefab;
        equiptPant = pantsData[0].itemMat;
    }

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
        newContainer.Button.onClick.AddListener(() => stateButton.ChangeButtonState(newContainer));
        containerList.Add(newContainer);
    }

    public void SaveItemData(EquipmentContainer container)
    {
        switch (container.ItemType)
        {
            case ItemType.Weapon:
                equiptWeapon = container.Prefab;
                break;
            case ItemType.Shield:
                equiptShield = container.Prefab;
                break;
            case ItemType.Hat:
                equiptHat = container.Prefab;
                break;
            case ItemType.Pant:
                equiptPant = container.Material;
                break;
        }
    }

    public void AddItem()
    {
        CharacterEquipment playerEquipment = PlayersManager.Instance.player.CharacterEquipment;
        playerEquipment.GetEquipMent(equiptWeapon, equiptShield, equiptHat, equiptPant);
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
