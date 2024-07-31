using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject MenuPanel;
    public GameObject InGamePanel;
    public GameObject JoyStick;
    public GameObject EndGame;
    public GameObject WinGame;
    public GameObject settingInGame;
    public int goldNumber;



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
        MenuPanel.SetActive(state == 1);
        InGamePanel.SetActive(state == 2);
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
        Time.timeScale = 1f;
        GameController.instance.StartGame();
        Camera.instance.ChangeState(2);
    }

    public void EndGameUI()
    {
        EndGame.SetActive(true);
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
        EndGame.SetActive(false);
        WinGame.SetActive(false);
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
        WinGame.SetActive(true);
        PauseGame();
        goldNumber = GameController.instance.countPlayers[0].GetComponent<Player>().level;
    }

    public void WinAndMoveToMainMenu()
    {
        MoveToMainMenu();
        GameController.instance.goldNumber += goldNumber;
        GameController.instance.goldCoin.text = GameController.instance.goldNumber.ToString();
    }
}
