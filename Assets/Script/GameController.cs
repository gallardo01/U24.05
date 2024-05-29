using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Button buyButton;
    [SerializeField] Button replayButton;

    [SerializeField] int maxGold = 10;
    [SerializeField] int live;

    private int currentGold;
    private int highScore = 0;

    private void OnEnable()
    {
        StartGame();
    }
    private void StartGame()
    {
        live = 3;
        currentGold = maxGold;
        DisplayGold();

        replayButton.onClick.AddListener(RestartGame);
        scoreText.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (currentGold < 10)
        {
            buyButton.interactable = false;
        }
        else buyButton.interactable = true;
    }

    public void PayGold(int cost)
    {
        currentGold -= cost;
        DisplayGold();
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        DisplayGold();
    }

    public void DisplayGold()
    {
        goldText.text = currentGold.ToString();
    }

    public void DecreaseLive()
    {
        live--;
        if(live <= 0) GameOver();
    }
    
    private void GameOver()
    {
        StopAllCoroutines();
        GameObject[] listMonster = GameObject.FindGameObjectsWithTag("Monster");
        for (int i = 0;i < listMonster.Length; i++)
        {
            Destroy(listMonster[i].gameObject);
        }
        GameObject[] listBullet = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < listMonster.Length; i++)
        {
            Destroy(listMonster[i].gameObject);
        }

        scoreText.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(true);


        Time.timeScale = 0;
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
