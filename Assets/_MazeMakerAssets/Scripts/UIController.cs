using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Button playButton, shopButton, backButton;

    [SerializeField] GameObject menuPanel, stagePanel, shopPanel;

    [SerializeField] Button[] levelButtons;

    private int currentLevel;
    private void Awake()
    {
        playButton.onClick.AddListener(OpenStagePanel);
        shopButton.onClick.AddListener(OpenShopPanel);
        backButton.onClick.AddListener(OpenMenuPanel);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
            currentLevel = 1;
        }
        else
        {
            currentLevel = PlayerPrefs.GetInt("Level");
        }
    }

    void OpenStagePanel()
    {

    }

    void OpenShopPanel()
    {

    }

    void OpenMenuPanel()
    {

    }

    // 
}
