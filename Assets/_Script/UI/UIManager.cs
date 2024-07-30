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
    public GameObject settingInGame;



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
}
