using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : Singleton<GameController>
{
    [SerializeField] public List<GameObject> playerList = new List<GameObject>();
    [SerializeField] List<Transform> startPoint;
    [SerializeField] GameObject botPrefab;
    [SerializeField] GameObject player;
    [SerializeField] GameObject finishPoint;
    public JoystickControl joystick;
    public GameObject gamePanel;

    private void OnEnable()
    {
        joystick.enabled = false; joystick.enabled = false;
        gamePanel.SetActive(true);
    }

    private void SetUpPlayerList()
    {
        for (int i = 0; i < startPoint.Count; i++)
        {
            GameObject bot = Instantiate(botPrefab, startPoint[i].position, Quaternion.identity);
            playerList.Add(bot);
        }
        playerList.Add(player);
    }
    public void CreatColorPlayer()
    {
        int[] colorExists = new int[playerList.Count];
        for (int i = 0; i < playerList.Count; i++)
        {
            int colorIndex;
            do
            {
                colorIndex = Random.Range(0, ColorController.Instance.materials.Count);
                playerList[i].GetComponent<Character>().SetPlayerColor(colorIndex);
            } while (Array.Exists(colorExists, num => num == colorIndex));
            colorExists[i] = colorIndex;
        }
    }
    public void CreatColorBrick(Brick brick, Character player)
    {
        int colorBrick = player.colorIndex;
        brick.SetBrickColor(colorBrick);
    }
    public void EndGame(Character character)
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            if (character.gameObject == playerList[i])
            {
                playerList[i].transform.position = finishPoint.transform.position + Vector3.up ;
            }
            else
            {
                playerList[i].SetActive(false);
            }
        }
    }
    public void StartGame()
    {
        SetUpPlayerList();
        CreatColorPlayer();
        player.SetActive(true);
        joystick.enabled = true;
        gamePanel.SetActive(false);      
    }
}
