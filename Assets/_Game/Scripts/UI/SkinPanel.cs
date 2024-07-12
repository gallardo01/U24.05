using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinPanel : MonoBehaviour
{
    [SerializeField] GameColor color;

    [SerializeField] Button btnBuy, btnEquip;

    public void InitSkinPanel()
    {
        btnBuy.onClick.AddListener(() =>
        {
            
        });
        btnEquip.onClick.AddListener(() =>
        {

        });
    }
}
