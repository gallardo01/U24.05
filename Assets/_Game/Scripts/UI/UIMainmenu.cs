using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainmenu : UICanvas
{
    [SerializeField] Button btnPlay, btnShop;

    private void Awake()
    {
        btnPlay.onClick.AddListener(() =>
        {
            CloseDirectly();
            
        });

        btnShop.onClick.AddListener(() =>
        {
            CloseDirectly();
            
        });
    }

    
}
