using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public TMP_Text goldCoin;

    public int goldNumber;
    private int goldGain;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

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
    }

    public void SettingIngame()
    {
        settingInGame.SetActive(true);
        PauseGame();
    }
    public void StartGame()
    {
        InitGameState(2);
        goldCoin.text = GameController.instance.InitPlayerGold().ToString();
        Time.timeScale = 1f;
        GameController.instance.StartGame();
        Camera.instance.ChangeState(3);
    }

    public void EndGameUI()
    {
        endGameUI.SetActive(true);
        GameController.instance.EndGame();
        PauseGame();
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        settingInGame.SetActive(false);
    }

    public void MoveToMainMenu()
    {
        InitGameState(1);
        Time.timeScale = 1f;
        GameController.instance.PlayAgain();
        settingInGame.SetActive(false);
        endGameUI.SetActive(false);
        winGameUI.SetActive(false);
        shopPanelUI.SetActive(false);
        Camera.instance.ChangeState(1);
    }

    public void TryANewGame()
    {
        GameController.instance.PlayAgain();
        StartGame();
        settingInGame.SetActive(false);
    }

    public void WinAGame()
    {
        winGameUI.SetActive(true);
        PauseGame();
        goldGain = 20 + GameController.instance.countPlayers[0].GetComponent<Player>().level;
    }

    public void WinAndMoveToMainMenu()
    {
        MoveToMainMenu();
        GameController.instance.GainGold(goldGain);
    }

    public void OpenShop()
    {
        Camera.instance.ChangeState(2);
        menuIngameUI.SetActive(false);
        shopPanelUI.SetActive(true);
    }
}
