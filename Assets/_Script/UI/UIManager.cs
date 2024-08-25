using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SoundManager;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject menuIngameUI;
    public GameObject inGamePanelUI;
    public GameObject shopPanelUI;
    public GameObject JoyStick;
    public GameObject endGameUI;
    public GameObject winGameUI;
    public GameObject settingInGame;
    public GameObject upgradeStatUI;
    public TMP_Text goldCoin;
    public TMP_Text claimLoseGoldCoin;
    public TMP_Text claimWinGoldCoin;

    private int goldGain;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        goldCoin.text = GameController.instance.InitPlayerGold().ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void InitGameState(int state)
    {
        menuIngameUI.SetActive(state == 1);
        inGamePanelUI.SetActive(state == 2);
        JoyStick.SetActive(state == 2);
        upgradeStatUI.SetActive(state == 3);
        settingInGame.SetActive(state == 4);
        endGameUI.SetActive(state == 4);
        winGameUI.SetActive(state == 4);
        shopPanelUI.SetActive(state == 4);

        if (state == 1)
        {           
            SoundManager.instance.PlayBackgroundMusic(SoundList.BackgroundMainMenu);
        }

        else if (state == 2)
        {
            SoundManager.instance.PlayBackgroundMusic(SoundList.BackgroundIngame);
        }
    }

    public void SettingIngame()
    {
        settingInGame.SetActive(true);
        PauseGame();
    }
    public void StartGame()
    {
        InitGameState(2);
        Time.timeScale = 1f;
        GameController.instance.StartGame();
        Camera.instance.ChangeState(3);
        SoundManager.instance.PlayOneShot(SoundList.ButtonClick);
    }

    public void DisplayLoseGameUI()
    {
        endGameUI.SetActive(true);
        GameController.instance.EndGame();
        goldGain = GameController.instance.playerPrebs.level;
        claimLoseGoldCoin.text = "+" + goldGain;
        PauseGame();
    }
    public void DisplayWinGameUI()
    {
        winGameUI.SetActive(true);
        PauseGame();
        goldGain = 20 + GameController.instance.playerPrebs.level;
        claimWinGoldCoin.text = "+" + goldGain;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        SoundManager.instance.PlayOneShot(SoundList.ButtonClick);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        settingInGame.SetActive(false);
        SoundManager.instance.PlayOneShot(SoundList.ButtonClick);
    }

    public void MoveToMainMenu()
    {
        InitGameState(1);
        Time.timeScale = 1f;
        GameController.instance.PlayAgain();
        Camera.instance.ChangeState(1);
        SoundManager.instance.PlayOneShot(SoundList.ButtonClick);
        GameController.instance.ChangeGold(goldGain);
        SoundManager.instance.PlayOneShot(SoundList.ButtonClick);
    }

    public void BackButton()
    {
        InitGameState(1);
        Time.timeScale = 1f;
        Camera.instance.ChangeState(1);
        SoundManager.instance.PlayOneShot(SoundList.ButtonClick);
    }

    public void TryANewGame()
    {
        GameController.instance.PlayAgain();
        StartGame();
        settingInGame.SetActive(false);
        SoundManager.instance.PlayOneShot(SoundList.ButtonClick);
    }

    public void OpenShop()
    {
        Camera.instance.ChangeState(2);
        menuIngameUI.SetActive(false);
        shopPanelUI.SetActive(true);
        SoundManager.instance.PlayOneShot(SoundList.ButtonClick);
        SoundManager.instance.PlayBackgroundMusic(SoundList.Shop);
    }

    public void OpenUpgradeUI()
    {
        InitGameState(3);
        SoundManager.instance.PlayOneShot(SoundList.ButtonClick);
    }
}
