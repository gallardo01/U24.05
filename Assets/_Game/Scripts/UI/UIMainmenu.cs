using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainmenu : UICanvas
{
    [SerializeField] Button btnPlay, btnWeapon, btnSkin;
    [SerializeField] TMP_InputField inputName;
    private void Awake()
    {
        btnPlay.onClick.AddListener(() =>
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIGameplay>();
            LevelManager.Ins.player.characterInfo.UpdateTextName(inputName.text);
        });

        btnWeapon.onClick.AddListener(() =>
        {
            CloseDirectly();
            //UIManager.Ins.OpenUI<UIShop>();
        });

        btnSkin.onClick.AddListener(() =>
        {
            CloseDirectly();
            //UIManager.Ins.OpenUI<UIShop>();
        });
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeGameState(GameState.Mainmenu);
    }
}
