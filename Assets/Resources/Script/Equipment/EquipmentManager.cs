using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ItemType 
{
    Weapon,
    Shield,
    Hat,
    Pant,
}

public class EquipmentManager : Singleton<EquipmentManager>, IGameStateListener
{
    [SerializeField] ItemDataSO itemData;

    private Item[] weaponsDatas;
    private Item[] shieldsDatas;
    private Item[] hatsDatas;
    private Item[] pantsDatas;

    [SerializeField] EquipmentContainer containerPrefab;
    [HideInInspector] public List<EquipmentContainer> weaponContainers = new List<EquipmentContainer>();
    [HideInInspector] public List<EquipmentContainer> shieldContainers = new List<EquipmentContainer>();
    [HideInInspector] public List<EquipmentContainer> hatContainers = new List<EquipmentContainer>();
    [HideInInspector] public List<EquipmentContainer> pantContainers = new List<EquipmentContainer>();

    [SerializeField] Transform weaponTab;
    [SerializeField] Transform shieldTab;
    [SerializeField] Transform hatTab;
    [SerializeField] Transform pantTab;

    [SerializeField] ShopStateButton stateButton;
    [SerializeField] TextMeshProUGUI[] statsValueText;
    private bool DoneSetUp = false;

    private Item equiptWeapon;
    private Item equiptShield;
    private Item equiptHat;
    private Item equiptPant;

    private ModelMode modelMode;

    private void Configue()
    {
        weaponsDatas = itemData.weaponItems;
        shieldsDatas = itemData.shieldItems;
        hatsDatas = itemData.hatItems;
        pantsDatas = itemData.pantItems;

        equiptWeapon = weaponsDatas[0];
        equiptShield = shieldsDatas[0];
        equiptHat = hatsDatas[0];
        equiptPant = pantsDatas[0];

        CreatContainers();
    }

    private void CreatContainers()
    {
        modelMode = CharactersManager.Instance.player.GetComponent<ModelMode>();

        for (int i = 0; i < weaponsDatas.Length; i++) CreatContainer(weaponsDatas[i], weaponTab);
        for (int i = 0; i < shieldsDatas.Length; i++) CreatContainer(shieldsDatas[i], shieldTab);
        for (int i = 0; i < hatsDatas.Length; i++) CreatContainer(hatsDatas[i], hatTab);
        for (int i = 0; i < pantsDatas.Length; i++) CreatContainer(pantsDatas[i], pantTab);
    }

    public void CreatContainer(Item data, Transform parentTab)
    {
        EquipmentContainer newContainer = Instantiate(containerPrefab, parentTab);
        newContainer.OnInit(data);
        newContainer.Button.onClick.AddListener(() => stateButton.ChangeButtonState(newContainer));
        newContainer.Button.onClick.AddListener(() => OnContainerSelect(newContainer));

        if (data.itemType == ItemType.Weapon)           weaponContainers.Add(newContainer);
        else if (data.itemType == ItemType.Shield)      shieldContainers.Add(newContainer);
        else if (data.itemType == ItemType.Hat)         hatContainers.Add(newContainer);
        else if (data.itemType == ItemType.Pant)        pantContainers.Add(newContainer);

        if (newContainer.IsEquip)
        {
            OnContainerEquip(newContainer);
            if (newContainer.Data.itemType == ItemType.Weapon)
            {
                UpdateStatDisplay(newContainer);
                stateButton.ChangeButtonState(newContainer);
            }
        }
    }

    private void OnContainerSelect(EquipmentContainer container)
    {
        List<EquipmentContainer> sameTypes = new List<EquipmentContainer>();
        if (container.ItemType == ItemType.Weapon)       sameTypes = weaponContainers;
        else if(container.ItemType == ItemType.Shield)   sameTypes = shieldContainers;
        else if(container.ItemType == ItemType.Hat)      sameTypes = hatContainers;
        else if(container.ItemType == ItemType.Pant)     sameTypes = pantContainers;

        for (int i = 0; i < sameTypes.Count; i++)
        {
            if (sameTypes[i] == container) sameTypes[i].Select();
            else sameTypes[i].UnSelect();
        }
        UpdateStatDisplay(container);
        SoundManager.ButtonClick();
    }

    public void OnContainerEquip(EquipmentContainer container)
    {
        List<EquipmentContainer> others = new List<EquipmentContainer>();
        if (container.ItemType == ItemType.Weapon) others = weaponContainers;
        else if (container.ItemType == ItemType.Shield) others = shieldContainers;
        else if (container.ItemType == ItemType.Hat) others = hatContainers;
        else if (container.ItemType == ItemType.Pant) others = pantContainers;

        for (int i = 0; i < others.Count; i++)
        {
            if (others[i] == container) others[i].SetEquip(true);
            else others[i].SetEquip(false);
        }
        SaveItemData(container.Data);
    }

    private void UpdateStatDisplay(EquipmentContainer container)
    {
        StatData[] statData = container.Data.itemStats;

        for (int i = 0; i < statData.Length; i++)
        {
            float index = statData[i].index;
            statsValueText[i].text = "+ " + index.ToString();
        }
        for(int i = statData.Length; i < statsValueText.Length; i++)
        {
            statsValueText[i].text = "+ " + 0;
        }
    }

    public void SaveItemData(Item data)
    {
        switch (data.itemType)
        {
            case ItemType.Weapon:
                equiptWeapon = data;
                break;
            case ItemType.Shield:
                equiptShield = data;
                break;
            case ItemType.Hat:
                equiptHat = data;
                break;
            case ItemType.Pant:
                equiptPant = data;
                break;
        }
        AddItemToPlayer();
    }

    public void OnGameStateChange(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                if(weaponTab.childCount == 0)
                {
                    Invoke(nameof(Configue), 0.01f);
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
        player.SetEquipMent(equiptWeapon, equiptShield, equiptHat, equiptPant);
        modelMode.PlayRandomAnim();
    }

    private void AddItemForBots()
    {
        List<Character> characters = CharactersManager.Instance.CharacterList;
        for (int i = 0; i < characters.Count; i++) 
        {
            if (characters[i].gameObject.layer == 7) continue;

            Item weaponData = weaponsDatas[Random.Range(0, weaponsDatas.Length)];
            Item shieldData = shieldsDatas[Random.Range(0, shieldsDatas.Length)];
            Item hatData = hatsDatas[Random.Range(0, hatsDatas.Length)];
            Item pantData = pantsDatas[Random.Range(0, pantsDatas.Length)];

            characters[i].SetEquipMent(weaponData, shieldData, hatData, pantData);
        }
    }
}
