using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject UIPanel;
    public GameObject InGamePanel;
    public GameObject JoyStickPanel;
    public GameObject SettingPanel;
    public GameObject MainMenuObj;

    public Button playGame;
    public Button mainMenu;
    public Button quitMainMenu;
    public Button settingButton;
    
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

    private void MainMenuClick()
    {
        SettingPanel.SetActive(false);
        InitGameState(1);
        GameController.Ins.PlayAgain();
    }

    // Update is called once per frame
    private void InitGameState(int state)
    {
        Debug.Log("InitGameState: " + (state == 2));
        UIPanel.SetActive(state == 1);
        InGamePanel.SetActive(state == 2 );
        JoyStickPanel.SetActive(state == 2);
        if (state == 2)
        {
            GameController.Ins.StartGame();
        }
    }
}
