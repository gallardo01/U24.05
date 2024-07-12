using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UICanvas
{
    [SerializeField] Button btnBack;

    [SerializeField] SkinPanel skinPanelPrefab;
    [SerializeField] Transform skinPanelParent;

    private void Awake()
    {
        btnBack.onClick.AddListener(() =>
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIMainmenu>();
        });

        List<Skin> skinList = DataManager.Ins.GetSkinList();

        if (skinList.Count > 0)
        {
            for (int i = 0; i < skinList.Count; i++)
            {
                SkinPanel skinPanel = Instantiate(skinPanelPrefab, skinPanelParent);
                skinPanel.InitSkinPanel(skinList[i]);
            }
        }
    }
}