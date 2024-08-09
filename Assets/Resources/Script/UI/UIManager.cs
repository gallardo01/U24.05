using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : 
    Singleton<UIManager>
{
    public GameObject UIPanel;
    public GameObject InGamePanel;
    public GameObject JoyStickPanel;
    public GameObject SettingPanel;
    public GameObject indicatorPanel;
    public GameObject AwardPanel;
    public GameObject shopPanel;

    public Button playGame;
    public Button mainMenu;
    public Button quitMainMenu;
    public Button settingButton;
    public Button shopButton;
    public TextMeshProUGUI goldText;
    
    // Start is called before the first frame update
    void Start()
    {
        InitGameState(1);
        playGame.onClick.AddListener(() =>
        {
            SettingPanel.SetActive(false);
            InitGameState(2);
        }); 
        InitGold();
        settingButton.onClick.AddListener(() =>
           SettingPanel.SetActive(true)
        );
        mainMenu.onClick.AddListener(MainMenuClick);
        quitMainMenu.onClick.AddListener(() =>
           SettingPanel.SetActive(false)
        );
        shopButton.onClick.AddListener(ShowShopPanel);
    }

    public void ShowAwardPanel(int gold)
    {
        AwardPanel.SetActive(true);
        AwardPanel.GetComponent<AwardUI>().InitAwardUI(gold, GameController.Ins.bots.Count + 1);
    }
    
    public void ShowShopPanel()
    {
        shopPanel.SetActive(true);
        UIPanel.SetActive(false);
        CameraFollower.Ins.ChangeState(3);
    }
    public void MainMenuClick()
    {
        AwardPanel.SetActive(false);
        SettingPanel.SetActive(false);
        InitGameState(1);
        GameController.Ins.PlayAgain();
        InitGold();
    }

    public void InitGold()
    {
        goldText.text = GameController.Ins.gold.ToString();
    }

    public void ResetUI()
    {
        UIPanel.SetActive(true);
        InGamePanel.SetActive(false);
        JoyStickPanel.SetActive(false);
        SettingPanel.SetActive(false);
        indicatorPanel.SetActive(false);
        AwardPanel.SetActive(false);
        goldText.text = GameController.Ins.gold.ToString();
    }
    
    
    // Update is called once per frame
    private void InitGameState(int state)
    {
        UIPanel.SetActive(state == 1);
        InGamePanel.SetActive(state == 2 );
        JoyStickPanel.SetActive(state == 2);
        if (state == 2)
        {
            indicatorPanel.SetActive(true);
            GameController.Ins.StartGame();
        }

        else if (state == 1)
        {
            indicatorPanel.SetActive(false);
        }
        CameraFollower.Ins.ChangeState(state);
    }
}
