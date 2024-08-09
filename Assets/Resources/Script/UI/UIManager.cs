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
    public GameObject shopPanel;

    public Button playGame;
    public Button mainMenu;
    public Button quitSetting;
    public Button settingButton;
    public Button shopButton;
    public Button backButton;

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
        quitSetting.onClick.AddListener(() => { SettingsPanel.SetActive(false); });
        shopButton.onClick.AddListener(() => OpenShop());
        backButton.onClick.AddListener(() => BackHome());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackHome()
    {
        InitGameState(1);
        shopPanel.SetActive(false); 
    }

    public void OpenShop()
    {
        UIPanel.SetActive(false);
        shopPanel.SetActive(true);
        //shopPanel.SetActive(state == 3);
        CameraFollow.Instance.ChangeState(3);
    }

    public void OpenAwardUI(int gold)
    {
        awardPanel.SetActive(true);
        awardPanel.GetComponent<Award>().InitAwardUI(gold, GameController.Instance.botInStage.Count + 1);
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
        if(state == 2)
        {
            indicatorPanel.SetActive(true);
            GameController.Instance.StartGame();    
        }
        else if (state == 1) 
        {
            indicatorPanel.SetActive(false);    
        }
        //else if(state == 3)
        //{
        //    shopPanel.SetActive(true);
        //}
        CameraFollow.Instance.ChangeState(state);   
    }
}
