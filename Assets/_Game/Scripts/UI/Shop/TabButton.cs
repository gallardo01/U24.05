using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabButton : MonoBehaviour
{
    [SerializeField] Button btnTab;
    [SerializeField] Image imageIcon;

    private void Awake()
    {
        btnTab.onClick.AddListener(() =>
        {
            SelectTab();
        });
    }

    public void SelectTab()
    {
        this.PostEvent(EventID.OnTabSelected, this);
    }

    public void SelectTab(bool isSelected)
    {

    }
}
