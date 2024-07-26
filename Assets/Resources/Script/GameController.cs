using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : Singleton<GameController>
{
    public Canvas indicatorCanvas;
    public Bot Bot;
    public int botNumber = 10;
    public Player player;
    public TargetIndicator indicator;
    public List<Transform> listSpawn = new List<Transform>();
    public TextMeshProUGUI aliveText;
    private int totalCharacter;

    // Start is called before the first frame update
    void Start()
    {
        totalCharacter = botNumber + 1;
        CreateBotNewGame();
        if (botNumber > listSpawn.Count-1)
        {
            botNumber = listSpawn.Count - 1;
        }
        InitTextAlive();
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

    public void EndGame()
    {
        JoystickControl.direct = Vector3.zero;
        JoystickControl.Instance.gameObject.SetActive(false);
        // UI?
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
            string botName = Constant.PlayerName[Random.Range(0, Constant.PlayerName.Length)] + Random.Range(0, 10000);
            botIndicator.InitTarget(color, 1, botName);
        }
    }
    
}
