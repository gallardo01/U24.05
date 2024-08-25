using DG.Tweening;
using Lean.Pool;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;


public class CharactersManager : Singleton<CharactersManager>, IGameStateListener
{
    [SerializeField] Bot botPrefab;
    [SerializeField] Player playerPrefab;
    [SerializeField] Indicator indicatorPrefab;
    [SerializeField] Canvas mainCanvas;
    [SerializeField] int numberChar;
    [HideInInspector] public Player player;
    
    public List<Character> CharacterList { get; private set; } = new List<Character>();

    [SerializeField] TextMeshProUGUI aliveText; 

    private void OnEnable()
    {
        EventManager.OnCharacterDeath += OnCharacterDeath;
    }

    private void OnDisable()
    {
        EventManager.OnCharacterDeath -= OnCharacterDeath;
    }

    public virtual void OnCharacterDeath(Character sender, Character victim)
    {
        sender.UpdateLevel();
        if (sender.gameObject.layer == 7)
        {

            Camera.main.DOFieldOfView(Camera.main.fieldOfView + 3, 1f)
                       .SetEase(Ease.InOutSine)
                       .OnComplete(() => SoundManager.LevelUp());

            GameManager.AddPoint(20);
        }
        RemoveCharacter(victim);
        DisplayAlive();
    }

    public void OnInit()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        player = Instantiate(playerPrefab);
        player.transform.position = new Vector3(1000, 1000, 1000);
        Indicator playerIndicator = Instantiate(indicatorPrefab, mainCanvas.transform);
        playerIndicator.OnInit(player);
        player.SetIndicator(playerIndicator);
        player.OnInit();
        CharacterList.Add(player);
    }

    private void SpawnBots()
    {
        for (int i = 1; i < numberChar; i++)
        {
            Bot newBot = LeanPool.Spawn(botPrefab);
            newBot.transform.position = new Vector3(-1000, -1000, -1000);
            Indicator botIndicator = Instantiate(indicatorPrefab, mainCanvas.transform);
            newBot.SetIndicator(botIndicator);
            newBot.OnInit();
            botIndicator.OnInit(newBot);
            CharacterList.Add(newBot);
        }

    }

    private void SetPosition()
    {
        List<Vector3> startGamePos = GetSpawnPos();
        for(int i = 0; i < CharacterList.Count; i++)
        {
            CharacterList[i].transform.position = startGamePos[i];
        }
    }

    public List<Vector3> GetSpawnPos()
    {
        List<Vector3> pos = new List<Vector3>();

        for (int i = 0; i < 100; i++)
        {
            Vector3 randomPoint = Vector3.zero + Random.insideUnitSphere * 50;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 50f, NavMesh.AllAreas))
            {
                pos.Add(hit.position);
                if (pos.Count == numberChar) break;
            }
        }
        return pos;
    }

    private void ActiveSetting(bool active)
    {
        for (int i = 0; i < CharacterList.Count; i++)
        {
            CharacterList[i].enabled = active;
            CharacterList[i].Indicator.gameObject.SetActive(active);
        }
        DisplayAlive();
    }

    private void RemoveCharacter(Character character)
    {
        CharacterList.Remove(character);
        SoundManager.CharacterDead();
        if (CharacterList.Count == 1 || character.GetType() == typeof(Player))
        {
            GameManager.Instance.GameOver();
        }
    }

    private void DisplayAlive()
    {
        aliveText.text = "Alive " + CharacterList.Count.ToString();
    }

    public void OnGameStateChange(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                ActiveSetting(false);
                break;

            case GameState.GAME:
                SpawnBots();
                SetPosition();
                ActiveSetting(true);
                break;

            case GameState.SETTING:
                ActiveSetting(false);
                break;

            case GameState.GAMEOVER:
                ActiveSetting(false);
                break;
        }
    }
}
