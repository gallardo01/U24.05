using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWin : UICanvas
{

    [SerializeField] Button btnPlay, btnQuit;

    private void Awake()
    {
        btnPlay.onClick.AddListener(() =>
        {
            CloseDirectly();
            LevelManager.Ins.PlayAgain();
        });

        btnQuit.onClick.AddListener(() =>
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIMainmenu>();
        });
    }
}
