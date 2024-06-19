using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject stage;
    [SerializeField] GameObject shop;

    [SerializeField] Button stageButton;
    [SerializeField] Button shopButton;
    // Stage
    [SerializeField] Button backButton;
    [SerializeField] Button[] listStageButton;
    private int currentStage;
    // Shop
    [SerializeField] Button backButtonShop;

    [SerializeField] GameObject[] shopItem;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Stage"))
        {
            PlayerPrefs.SetInt("Stage", 1);
            currentStage = 1;
        }
        else
        {
            currentStage = PlayerPrefs.GetInt("Stage");
        }

        if (!PlayerPrefs.HasKey("Brick_1"))
        {
            PlayerPrefs.SetInt("Brick_1", 1);
        }
        if (!PlayerPrefs.HasKey("Brick_2"))
        {
            PlayerPrefs.SetInt("Brick_2", 0);
        }
        if (!PlayerPrefs.HasKey("Brick_3"))
        {
            PlayerPrefs.SetInt("Brick_3", 0);
        }
        if (!PlayerPrefs.HasKey("Brick_4"))
        {
            PlayerPrefs.SetInt("Brick_4", 0);
        }
        if (!PlayerPrefs.HasKey("Pick"))
        {
            PlayerPrefs.SetInt("Pick", 1);
        }
        stageButton.onClick.AddListener(() => OnStageClick());
        shopButton.onClick.AddListener(() => OnShopClick());
        backButton.onClick.AddListener(() => OnMenuClick());

        listStageButton[0].onClick.AddListener(() => LoadSceneStage(1));
        listStageButton[1].onClick.AddListener(() => LoadSceneStage(2));
        listStageButton[2].onClick.AddListener(() => LoadSceneStage(3));
        listStageButton[3].onClick.AddListener(() => LoadSceneStage(4));
        listStageButton[4].onClick.AddListener(() => LoadSceneStage(5));
        listStageButton[5].onClick.AddListener(() => LoadSceneStage(6));
        listStageButton[6].onClick.AddListener(() => LoadSceneStage(7));
        listStageButton[7].onClick.AddListener(() => LoadSceneStage(8));

        InitStageButton();

        InitShopItem();
    }

    public void InitShopItem()
    {
        for (int i = 0; i < shopItem.Length; i++)
        {
            if (i + 1 == PlayerPrefs.GetInt("Pick"))
            {
                shopItem[i].GetComponent<BrickShop>().InitButtonState(3);
            }
            else if (PlayerPrefs.GetInt("Brick_" + (i + 1).ToString()) == 1)
            {
                shopItem[i].GetComponent<BrickShop>().InitButtonState(2);
            }
            else
            {
                shopItem[i].GetComponent<BrickShop>().InitButtonState(1);
            }
        }
    }
    private void LoadSceneStage(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    private void InitStageButton()
    {
        for (int i = 0; i < listStageButton.Length; i++)
        {
            if (i < currentStage)
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
        menu.SetActive(false);
        stage.SetActive(true);
        shop.SetActive(false);
    }

    private void OnShopClick()
    {
        menu.SetActive(false);
        stage.SetActive(false);
        shop.SetActive(true);
    }

    private void OnMenuClick()
    {
        menu.SetActive(true);
        stage.SetActive(false);
        shop.SetActive(false);
    }
}
