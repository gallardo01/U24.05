using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController> 
{
    private List<int> gameColors = new List<int>();
    public Player player;
    public List<Transform> startPoints = new List<Transform>();
    public Bot bot;
    public Transform finishPoints;
    public JoystickControl joystick;
    public GameObject startGamePanel;
    public GameObject endGamePanel;
    public Button startGameButton;

    private List<Character> players = new List<Character>();    

    // Start is called before the first frame update
    void Start()
    {
        endGamePanel.SetActive(false);  
        RandomGameColor();
        SetUpCharacterInGame();
    }

    private void SetUpCharacterInGame()
    {
        player.SetCharacterColor(gameColors[0]);
        int rand_pos = Random.Range(0, startPoints.Count);
        player.transform.position = startPoints[rand_pos].position;
        startPoints.RemoveAt(rand_pos);  

        
        for(int i = 0; i < 1; i++)
        {
            Bot botInGame = Instantiate(bot);
            bot.SetCharacterColor(gameColors[i + 1]);
            botInGame.transform.position = startPoints[i].position;
            players.Add(botInGame);
        }

        for (int i = 0; i < players.Count; i++) 
        {
            players[i].enabled = false;
        }
        players.Add(player);
        joystick.enabled = false;
    }

    public void PlayGame()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].enabled = true;
        }
        joystick.enabled = true;
        startGamePanel.SetActive(false);  
    }

    private void RandomGameColor()
    {
        for (int i = 0; i < 5; i++)
        {
            int randColor;
            while (true)
            {
                randColor = Random.Range(0, 10);
                bool sameColor = false;
                for (int j = 0; j < gameColors.Count; j++)
                {
                    if (randColor == gameColors[j])
                    {
                        sameColor = true;
                        break;
                    }
                }
                if (!sameColor)
                {
                    break;
                }
            }
            gameColors.Add(randColor);
        }
    }

    public void EndGame(Character winner)
    {
        Debug.Log("End Game");
        for(int i = 0; i < players.Count; i++)
        {
            if(winner == players[i])
            {
                winner.ChangeAnim("victory");
                winner.transform.position = finishPoints.transform.position + Vector3.up * 9f;
                Debug.Log(winner.transform.position);
                //joystick.enabled = false;
                endGamePanel.SetActive(true);
            }
            else
            {
                Destroy(players[i].gameObject);    
            }
        }
    }
}
