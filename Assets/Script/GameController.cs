using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Button buyButton;
    [SerializeField] int maxGold = 10;
    [SerializeField] int live;

    private int currentGold;
    private int highScore = 0;

    private static GameController instance;
    public static GameController Instance {  get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        live = 3;
        currentGold = maxGold;
        DisplayGold();
        scoreText.gameObject.SetActive(false);
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
        live --;
        if(live <= 0) EndScene();
    }
    
    private void EndScene()
    {
        highScore = currentGold;
        if (highScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        scoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        scoreText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
