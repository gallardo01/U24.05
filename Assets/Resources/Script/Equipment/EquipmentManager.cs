using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using TMPro;
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
    [SerializeField] EquipmentDataSO[] weaponsDatas;
    [SerializeField] EquipmentDataSO[] shieldsDatas;
    [SerializeField] EquipmentDataSO[] hatsDatas;
    [SerializeField] EquipmentDataSO[] pantsDatas;

    [SerializeField] EquipmentContainer containerPrefab;
    [HideInInspector] public List<EquipmentContainer> containerList = new List<EquipmentContainer>();

    [SerializeField] Transform weaponTab;
    [SerializeField] Transform shieldTab;
    [SerializeField] Transform hatTab;
    [SerializeField] Transform pantTab;

    [SerializeField] ShopStateButton stateButton;
    [SerializeField] TextMeshProUGUI[] statsValueText;
    private bool DoneSetUp = false;

    private GameObject equiptWeapon;
    private GameObject projectile;
    private GameObject equiptShield;
    private GameObject equiptHat;
    private Material equiptPant;

    private ModelMode modelMode;

    private void Start()
    {
        equiptWeapon = weaponsDatas[0].itemPrefab;
        projectile = weaponsDatas[0].projectTilePrefab;
        equiptShield = shieldsDatas[0].itemPrefab;
        equiptHat = hatsDatas[0].itemPrefab;
        equiptPant = pantsDatas[0].itemMat;
    }

    private void CreatContainers()
    {
        modelMode = CharactersManager.Instance.player.GetComponent<ModelMode>();

        for (int i = 0; i < weaponsDatas.Length; i++)
        {
            CreatContainer(weaponsDatas[i], weaponTab);
        }
        for (int i = 0; i < shieldsDatas.Length; i++)
        {
            CreatContainer(shieldsDatas[i], shieldTab);
        }
        for (int i = 0; i < hatsDatas.Length; i++)
        {
            CreatContainer(hatsDatas[i], hatTab);
        }
        for (int i = 0; i < pantsDatas.Length; i++)
        {
            CreatContainer(pantsDatas[i], pantTab);
        }
    }

    public void CreatContainer(EquipmentDataSO data, Transform parentTab)
    {
        EquipmentContainer newContainer = Instantiate(containerPrefab, parentTab);
        newContainer.OnInit(data);
        newContainer.Button.onClick.AddListener(() => stateButton.ChangeButtonState(newContainer));
        newContainer.Button.onClick.AddListener(() => OnContainerSelect(newContainer));
        containerList.Add(newContainer);

        if (newContainer.IsEquip)
        {
            SaveItemData(newContainer.Data);
            AddItemToPlayer();
            if(newContainer.Data.itemType == ItemType.Weapon) UpdateStatDisplay(newContainer);
        }
    }

    private void OnContainerSelect(EquipmentContainer container)
    {
        List<EquipmentContainer> sameTypes = new List<EquipmentContainer>();
        List<EquipmentContainer> containers = containerList;

        for (int i = 0; i < containers.Count; i++)
        {
            if (containers[i].ItemType == container.ItemType)
            {
                sameTypes.Add(containers[i]);
            }
        }
        for (int i = 0; i < sameTypes.Count; i++)
        {
            if (sameTypes[i] == container) sameTypes[i].Select();
            else sameTypes[i].UnSelect();
        }
        UpdateStatDisplay(container);
        SoundManager.ButtonClick();
    }

    private void UpdateStatDisplay(EquipmentContainer container)
    {
        for (int i = 0; i < statsValueText.Length; i++)
        {
            float index = container.Data.statData[i].index;
            statsValueText[i].text = "+ " + index.ToString();
        }
    }

    public void SaveItemData(EquipmentDataSO data)
    {
        switch (data.itemType)
        {
            case ItemType.Weapon:
                equiptWeapon = data.itemPrefab;
                projectile = data.projectTilePrefab;
                break;
            case ItemType.Shield:
                equiptShield = data.itemPrefab;
                break;
            case ItemType.Hat:
                equiptHat = data.itemPrefab;
                break;
            case ItemType.Pant:
                equiptPant = data.itemMat;
                break;
        }
    }

    public void OnGameStateChange(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                if(weaponTab.childCount == 0)
                {
                    Invoke(nameof(CreatContainers), 0.01f);
                }
                break;
            case GameState.GAME:
                if (!DoneSetUp)
                {
                    AddItemForBots();
                    DoneSetUp = true;
                }
                break;
        }
    }

    public void AddItemToPlayer()
    {
        Player player = CharactersManager.Instance.player;
        player.SetEquipMent(equiptWeapon, equiptShield, equiptHat, equiptPant, projectile);
        modelMode.PlayRandomAnim();
    }

    private void AddItemForBots()
    {
        List<Character> characters = CharactersManager.Instance.CharacterList;
        for (int i = 0; i < characters.Count; i++) 
        {
            if (characters[i].gameObject.layer == 7) continue;

            EquipmentDataSO weaponData = weaponsDatas[Random.Range(0, weaponsDatas.Length)]; 
            EquipmentDataSO shieldData = shieldsDatas[Random.Range(0, shieldsDatas.Length)];
            EquipmentDataSO hatData = hatsDatas[Random.Range(0, hatsDatas.Length)];
            EquipmentDataSO pantData = pantsDatas[Random.Range(0, pantsDatas.Length)];

            BotEquipment(characters[i], weaponData, shieldData, hatData, pantData);
        }
    }

    private void BotEquipment(Character bot, EquipmentDataSO weaponData, EquipmentDataSO shieldData , EquipmentDataSO hatData, EquipmentDataSO pantData)
    {
        GameObject weaponItem = weaponData.itemPrefab;
        GameObject projectile = weaponData.projectTilePrefab;
        GameObject shieldItem = shieldData.itemPrefab;
        GameObject hatItem = hatData.itemPrefab;
        Material material = pantData.itemMat;

        bot.SetEquipMent(weaponItem, shieldItem, hatItem, material, projectile);
    }
}
