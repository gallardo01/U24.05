using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject UIPanel;
    public GameObject InGamePanel;
    public GameObject SettingsPanel;

    public Button playGame;
    public Button mainMenu;
    public Button quitSettings;

    public Button settingButton;
    

    // Start is called before the first frame update
    void Start()
    {
        InitGameState(1);
        playGame.onClick.AddListener(() => InitGameState(2));

        settingButton.onClick.AddListener(() => { SettingsPanel.SetActive(true); });
        mainMenu.onClick.AddListener(() => MainMenuClick());
        quitSettings.onClick.AddListener(() => { SettingsPanel.SetActive(false); });
    }

    private void MainMenuClick()
    {
        SettingsPanel.SetActive(false);
        InitGameState(1);
        GameController.Instance.ReplayGame();
    }

    private void InitGameState(int state)
    {
        UIPanel.SetActive(state == 1);
        InGamePanel.SetActive(state == 2);
        if (state == 2)
        {
            GameController.Instance.StartGame();
        }
    }
}
