using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public GameObject UIPanel;
    public GameObject InGamePanel;
    public GameObject SettingsPanel;
    public GameObject indicatorPanel;
    public GameObject awardPanel;

    public Button playGame;
    public Button mainMenu;
    public Button quitSettings;

    public Button settingButton;
    public JoystickControl joystick;

    public TextMeshProUGUI goldText;

    // Start is called before the first frame update
    void Start()
    {
        InitGameState(1);
        playGame.onClick.AddListener(() => InitGameState(2));
        InitGold();
        settingButton.onClick.AddListener(() => { SettingsPanel.SetActive(true); });
        mainMenu.onClick.AddListener(() => MainMenuClick());
        quitSettings.onClick.AddListener(() => { SettingsPanel.SetActive(false); });
    }

    public void OpenAwardUI(int gold)
    {
        awardPanel.SetActive(true);
        awardPanel.GetComponent<AwardUI>().InitAwardUI(gold, GameController.Instance.bots.Count + 1);
    }

    public void MainMenuClick()
    {
        awardPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        InitGameState(1);
        GameController.Instance.ReplayGame();
        InitGold();
    }

    public void InitGold()
    {
        goldText.text = GameController.Instance.gold.ToString();
    }
    private void InitGameState(int state)
    {
        UIPanel.SetActive(state == 1);
        InGamePanel.SetActive(state == 2);
        joystick.gameObject.SetActive(state == 2);
        if (state == 2)
        {
            indicatorPanel.SetActive(true);
            GameController.Instance.StartGame();
        } else if (state == 1)
        {
            indicatorPanel.SetActive(false);
        }
        CameraFollower.Instance.ChangeState(state);
    }
}
