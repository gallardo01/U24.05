using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainmenu : UICanvas
{
    [SerializeField] Button btnPlay, btnWeapon, btnSkin;

    private void Awake()
    {
        btnPlay.onClick.AddListener(() =>
        {
            GameManager.Ins.ChangeGameState(GameState.Gameplay);
            CloseDirectly();
            UIManager.Ins.OpenUI<UIGameplay>();
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
}
