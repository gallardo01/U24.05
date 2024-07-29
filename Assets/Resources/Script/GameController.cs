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
        botNumber = Random.Range(1, 21);
        CreateBotNewGame();
        if (botNumber > listSpawn.Count-1)
        {
            botNumber = listSpawn.Count - 1;
        }
        
        aliveCount = botNumber + 1;
        UpdateAliveCountUI();
    }
    
    public void EndGame()
    {
        JoystickControl.direct = Vector3.zero;
        JoystickControl.instance.gameObject.SetActive(false);
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

        List<Transform> spawnPoints = new List<Transform>(listSpawn); // Copy the list of spawn points
        spawnPoints.RemoveAt(spawnPoints.Count - 1); // Remove the player's spawn point

        for (int i = 0; i < botNumber; i++)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count); // Choose a random spawn point
            Bot bot = Instantiate(Bot);
            bot.transform.position = spawnPoints[spawnIndex].position;
            spawnPoints.RemoveAt(spawnIndex); // Remove the used spawn point

            TargetIndicator botIndicator = Instantiate(indicator, indicatorCanvas.transform);
            botIndicator.character = bot;
            bot.indicator = botIndicator;
            Color color = UnityEngine.Random.ColorHSV();
            string botName = Constant.PlayerName[Random.Range(0, Constant.PlayerName.Length)];
            botIndicator.InitTarget(color, 1, botName);
        }
    }
    
}