using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [SerializeField] Bot botPrefab;
    [SerializeField] Transform[] startPos;
    [SerializeField] Button playButton;
    [SerializeField] Transform Top1;
    private List<int> gameColors = new List<int>(); public List<int> GameColors => gameColors;
    private List<Character> playerList = new List<Character>();

    private void Awake()
    {
        playButton.onClick.AddListener(StartGame);
        OnInit();
    }

    public void OnInit()
    {
        SetUpPlayer();
        TurnObjectOnOff(false);
    }

    void TurnObjectOnOff(bool check)
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            playerList[i].enabled = check;
            playerList[i].GetComponent<CharacterBrick>().enabled = check;
        }
        JoystickControl.Instance.enabled = check;
    }

    void StartGame()
    {
        playButton.gameObject.SetActive(false);
        TurnObjectOnOff(true);
    }

    private void SetUpPlayer()
    {
        List<int> number = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for (int i = 0; i < 6; i++)
        {
            int random = number[Random.Range(0, number.Count)];
            gameColors.Add(random);
            number.Remove(random);
        }

        Player player = FindObjectOfType<Player>();
        player.SetCharacterColor(gameColors[0]);
        player.transform.position = startPos[startPos.Length - 1].position;
        playerList.Add(player);

        for (int i = 1; i < gameColors.Count; i++)
        {
            Bot newBot = Instantiate(botPrefab, startPos[i - 1].position, Quaternion.identity);
            newBot.SetCharacterColor(gameColors[i]);
            playerList.Add(newBot);
        }
    }

    public void EndGame(Character character)
    {
        TurnObjectOnOff(false);
        character.transform.position = Top1.position;
        LeanPool.DespawnAll();
    }
}
