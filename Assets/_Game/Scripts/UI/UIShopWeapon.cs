using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopWeapon : UICanvas
{
    [SerializeField] Button btnQuit;
    [SerializeField] TextMeshProUGUI textGold;

    private void Awake()
    {
        btnQuit.onClick.AddListener(() =>
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIMainmenu>();
        });
    }

    private void UpdateTextGold(int gold)
    {
        textGold.text = gold.ToString();
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeGameState(GameState.ShopWeapon);
        UpdateTextGold(DataManager.Ins.GetCurrentGold());
    }
}
