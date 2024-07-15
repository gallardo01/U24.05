using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : Singleton<MenuController>
{
    private Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();

    [SerializeField] Button playButton;
    [SerializeField] Button settingButton;
    [SerializeField] Button shopButton;

    [SerializeField] Button settingBackButton;
    [SerializeField] Button shopBackButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() => LoadMainGame());

        settingButton.onClick.AddListener(() => ShowPanel("SettingPanel"));
        shopButton.onClick.AddListener(() => ShowPanel("ShopPanel"));
        //settingBackButton.onClick.AddListener(() => ShowPanel("MenuPanel"));
        //shopBackButton.onClick.AddListener(() => ShowPanel("MenuPanel"));
    }

    public void RegisterPanel(string panelName, GameObject panel)
    {
        if (!panels.ContainsKey(panelName))
        {
            panels.Add(panelName, panel);
        }
    }

    public void ShowPanel(string panelName)
    {
        if (panels.ContainsKey(panelName))
        {
            HideAllPanels();
            panels[panelName].SetActive(true);
        }
    }

    public void HideAllPanels()
    {
        foreach (var panel in panels.Values)
        {
            panel.SetActive(false);
        }
    }

    public void LoadMainGame()
    {
        SceneManager.LoadScene(1);
    }

    
}
