using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    public Canvas indicatorCanvas;
    public Bot Bot;
    public int botNumber = 10;
    public Player player;
    public TargetIndicator indicator;
    public TextMeshProUGUI aliveText;
    private int totalCharacter;
    public List<Bot> bots;
    public int gold;
    

    public List<Transform> listSpawn = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        totalCharacter = botNumber + 1;
        if (botNumber > listSpawn.Count-1)
        {
            botNumber = listSpawn.Count - 1;
        }
        InitTextAlive();
        JoystickControl.instance.gameObject.SetActive(false);
        TargetIndicator playerIndicator = Instantiate(indicator, indicatorCanvas.transform);
        player.indicator = playerIndicator;
        playerIndicator.character = player;
        CreateBotNewGame();
        InitGold();

    }

    public void InitGold()
    {
        if(!PlayerPrefs.HasKey("Gold"))
        {
            gold = 0;
            PlayerPrefs.SetInt("Gold", 0);
        } else
        {
            gold = PlayerPrefs.GetInt("Gold");
        }
    }
    
    public void GainGold(int number)
    {
        gold += number;
        PlayerPrefs.SetInt("Gold", gold);
    }
   
    
    public void StartGame()
    {
        // for (int i = 0; i < bots.Count; i++)
        //     bots[i].OnInit();
        foreach (Bot bot in bots)
        {
            bot.OnInit();
        }
    }
    
    public void PlayAgain()
    {
        botNumber = Random.Range(1, 20);

        // Update totalCharacter and aliveText
        totalCharacter = botNumber + 1;
        aliveText.text = "Alive: " + totalCharacter;
        
        // Destroy all bots and their indicators
        foreach (Bot bot in bots)
        {
            Destroy(bot.indicator.gameObject);
            Destroy(bot.gameObject);
        }
        
        Destroy(player.indicator.gameObject);


        // Clear the bots list
        bots.Clear();
        player.OnDespawn();
        CreateBotNewGame();
        
        TargetIndicator playerIndicator = Instantiate(indicator, indicatorCanvas.transform);
        player.indicator = playerIndicator;
        playerIndicator.character = player;
        player.indicator.InitTarget(Color.black, 1, "Player"); // Add this line
        
        // Reset the UI
        
        UIManager.Ins.ResetUI();
            
    }

    
    
    public void EndGame()
    {
        JoystickControl.direct = Vector3.zero;
        JoystickControl.instance.gameObject.SetActive(false);
    }
    
    public void InitTextAlive()
    {
        aliveText.text = "Alive: " + totalCharacter;
    }
    
    public void CharacterDead()
    {
        totalCharacter--;
        aliveText.text = "Alive: " + totalCharacter;
    }

    
    public void CreateBotNewGame()
    {
        player.transform.position = listSpawn[listSpawn.Count - 1].position;
        player.OnInit();
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
            bots.Add(bot);
            bot.targetCircle.SetActive(false);
        }
    }
}