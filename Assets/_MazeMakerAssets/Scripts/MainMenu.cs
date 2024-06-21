using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject stageUI;
    public GameObject menuUI;
    public void PlayGame()
    {
        //SceneManager.LoadSceneAsync(1);
        menuUI.SetActive(false);
        stageUI.SetActive(true);
    }

    public void QuitGame() 
    {
        Debug.Log("Quit");
        //Application.Quit();
    }

    public void ChooseLevel()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
