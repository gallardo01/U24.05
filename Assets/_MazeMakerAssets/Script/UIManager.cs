using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Stage;
    [SerializeField] GameObject Shop;

    [SerializeField] Button stageButton;
    [SerializeField] Button shopButton;
    [SerializeField] Button backButton;
    [SerializeField] Button[] listStageButton;
    private int currentStage;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Stage"))
        {
            PlayerPrefs.SetInt("Stage", 1);
            currentStage = 1;
        } else
        {
            currentStage = PlayerPrefs.GetInt("Stage");
        }
        stageButton.onClick.AddListener(() => OnStageClick());
        shopButton.onClick.AddListener(() => OnShopClick());
        backButton.onClick.AddListener(() => OnBackButton());
        InitStageButton();

    }

    private void InitStageButton()
    {
        for (int i = 0; i < listStageButton.Length; i++)
        {
            if(i < currentStage)
            {
                listStageButton[i].interactable = true;
            } else
            {
                listStageButton[i].interactable = false;
            }
        }
    }

    private void OnStageClick()
    {
        Stage.SetActive(true);
        Menu.SetActive(false);
        Shop.SetActive(false);
    }

    private void OnShopClick()
    {
        Stage.SetActive(false);
        Menu.SetActive(false);
        Shop.SetActive(true);
    }

    private void OnBackButton()
    {
        Stage.SetActive(false);
        Menu.SetActive(true);
        Shop.SetActive(false);
    }
    
}
