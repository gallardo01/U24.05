using System.Collections;
using System.Collections.Generic;
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
    public GameObject MainMenuObj;

    public Button playGame;
    public Button mainMenu;
    public Button quitMainMenu;
    public Button settingButton;
    public GameObject awardHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        InitGameState(1);
        playGame.onClick.AddListener(() =>
        {
            SettingPanel.SetActive(false);
            InitGameState(2);
        });
        settingButton.onClick.AddListener(() =>
           SettingPanel.SetActive(true)
        );
        mainMenu.onClick.AddListener(MainMenuClick);
        quitMainMenu.onClick.AddListener(() =>
           SettingPanel.SetActive(false)
        );
    }

    public void MainMenuClick()
    {
        SettingPanel.SetActive(false);
        InitGameState(1);
        GameController.Ins.PlayAgain();
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
