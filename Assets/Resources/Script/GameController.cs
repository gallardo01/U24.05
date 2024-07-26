using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public Canvas indicatorCanvas;
    public Bot Bot;
    public int botNumber = 10;
    public Player player;
    public TargetIndicator indicator;
    public TextMeshProUGUI aliveCountText;
    private int aliveCount;

    public List<Transform> listSpawn = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        CreateBotNewGame();
        if (botNumber > listSpawn.Count-1)
        {
            botNumber = listSpawn.Count - 1;
        }
        
        aliveCount = botNumber + 1;
        UpdateAliveCountUI();
    }
    
    public void UpdateAliveCountUI()
    {
        aliveCountText.text = "Alive: " + aliveCount;
    }
    
    public void DecreaseAliveCount()
    {
        aliveCount--;
        UpdateAliveCountUI();
    }
    
    public void CreateBotNewGame()
    {
        player.transform.position = listSpawn[listSpawn.Count - 1].position;
        TargetIndicator playerIndicator = Instantiate(indicator, indicatorCanvas.transform);
        player.indicator = playerIndicator;
        playerIndicator.character = player;
        playerIndicator.InitTarget(Color.black, 1, "Player");

        for (int i = 0; i < botNumber; i++)
        {
            Bot bot = Instantiate(Bot);
            bot.transform.position = listSpawn[i].position;
            TargetIndicator botIndicator = Instantiate(indicator, indicatorCanvas.transform);
            botIndicator.character = bot;
            bot.indicator = botIndicator;
            Color color = UnityEngine.Random.ColorHSV();
            string botName = Constant.PlayerName[Random.Range(0, Constant.PlayerName.Length)];
            botIndicator.InitTarget(color, 1, botName);
        }
    }
    
}