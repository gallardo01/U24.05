using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class GameController : Singleton<GameController> 
{
    public Player player;
    public Bot botPrefabs;
    public int botNumber = 10;
    public Canvas indicatorCanvas;
    public TargetIndicator indicator;
    public TextMeshProUGUI aliveText;

    private int totalCharacter;
    public List<Bot> botInStage = new List<Bot>(); 
    public bool isStartGame = false;
    public int gold;

    // Start is called before the first frame update
    void Start()
    {
        totalCharacter = botNumber + 1;
        InitTextAlive();
        //Create indicator player
        TargetIndicator playerIndicator = Instantiate(indicator, indicatorCanvas.transform);
        player.indicator = playerIndicator;
        playerIndicator.character = player;
        
        SetUpCharacterInGame();
        InitGold(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitGold()
    {
        if (!PlayerPrefs.HasKey("Gold"))
        {
            gold = 0;
            PlayerPrefs.SetInt("Gold", 0);
        }
        else 
        {
            gold = PlayerPrefs.GetInt("Gold");
        }
    }

    public void GainGold(int num)
    {
        gold += num;
        PlayerPrefs.SetInt("Gold", gold);
    }

    public void StartGame()
    {
        for(int i = 0; i < botInStage.Count; i++)
        {
            botInStage[i].OnInit(); 
        }
    }

    public void ReplayGame()
    {
        for (int i = 0; i < botInStage.Count; i++)
        {
            Destroy(botInStage[i].indicator.gameObject);
            Destroy(botInStage[i].gameObject); 
        }
        botInStage.Clear();
        player.OnDespawn(); 
        SetUpCharacterInGame();
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

    private void SetUpCharacterInGame()
    {
        //TargetIndicator playerIndicator = Instantiate(indicator, indicatorCanvas.transform);
        //player.indicator = playerIndicator;
        //playerIndicator.character = player;
        //playerIndicator.InitTarget(Color.black, 1, "Player");

        player.OnInit();    

        for(int i = 0; i < botNumber; i++)
        {
            NavMeshHit hit;
            Vector3 point = new Vector3(Random.Range(-4f, 104f), 0f, Random.Range(1f, 99f));
            if (NavMesh.SamplePosition(point, out hit, 2.0f, NavMesh.AllAreas))
            {
                Bot bot = Instantiate(botPrefabs, hit.position, Quaternion.identity);
            
                TargetIndicator botIndicator = Instantiate(indicator, indicatorCanvas.transform);
                bot.indicator = botIndicator;
                botIndicator.character = bot;
                botInStage.Add(bot);
                Color color = Random.ColorHSV();
                string botname = GetRandomCharacterNames();
                botIndicator.InitTarget(color, 1, botname);
            }
        }
    }

    private string GetRandomCharacterNames()
    {
        int index = Random.Range(0, Contanst.characterNames.Count);
        return Contanst.characterNames[index];
    }

    public void EndGame()
    {
        JoystickControl.direct = Vector3.zero;
        JoystickControl.Instance.gameObject.SetActive(false);   
    }
}
