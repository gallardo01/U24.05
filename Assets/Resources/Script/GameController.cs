using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    private List<int> gameColors = new List<int>();
    public Player player;
    public List<Transform> startPoints;
    public Bot bot;
    public Transform finishPoints;
    public JoystickControl joystick;

    private List<Character> players = new List<Character>();

    public GameObject startGamePanel;
    public Button startGameButton;
    // Start is called before the first frame update
    void Start()
    {
        RandomGameColor();
        SetUpCharacterInGame();
        startGameButton.onClick.AddListener(() => PlayGame());
    }

    private void RandomGameColor()
    {
        // Chon 4 so trong 0 1 2 3 4 5 6 7 8 9
        for (int i = 0; i < 4; i++)
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

    private void SetUpCharacterInGame()
    {
        player.SetCharacterColor(gameColors[0]);
        int rand_pos = Random.Range(0, startPoints.Count);
        player.transform.position = startPoints[rand_pos].position;
        startPoints.RemoveAt(rand_pos);

        players.Add(player);
        for (int i = 0; i < 3; i++)
        {
            Bot botInGame = Instantiate(bot);
            botInGame.SetCharacterColor(gameColors[i + 1]);
            botInGame.transform.position = startPoints[i].position;
            players.Add(botInGame);
        }

        for (int i = 0; i < players.Count; i++)
        {
            players[i].enabled = false;
        }
        joystick.enabled = false;
    }

    private void PlayGame()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].enabled = true;
        }
        joystick.enabled = true;
        startGamePanel.SetActive(false);
    }

    public void EndGame(Character winner)
    {
        Debug.Log("End game");
        for (int i = 0; i < players.Count; i++)
        {
            if (winner == players[i])
            {
                // win
                Debug.Log("Victory");

                winner.ChangeAnim("victory");
                winner.transform.position = finishPoints.transform.position + Vector3.up;
            } else
            {
                Destroy(players[i].gameObject);
            }
        }
    }
}
