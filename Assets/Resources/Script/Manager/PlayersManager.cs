using DG.Tweening;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class PlayersManager : Singleton<PlayersManager>, IGameStateListener
{
    [SerializeField] Bot botPrefab;
    [SerializeField] Player playerPrefab;
    [SerializeField] Indicator indicatorPrefab;
    [SerializeField] Canvas mainCanvas;
    [SerializeField] int numberChar;
    [HideInInspector] public Player player;
    
    private List<Vector3> spawnPosList = new List<Vector3>();
    private List<Character> characterList = new List<Character>();

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
        sender.transform.DOScale(transform.localScale.x + 0.2f, 1f);
        sender.UpdateLevel();
        if (sender.gameObject.layer == 7)
        {
            Camera.main.fieldOfView += 1;
            CurrencyManager.Instance.AddCurrency(20);
        }

        characterList.Remove(victim);
        DisplayAlive();
    }

    public void OnInit()
    {
        spawnPosList = GetSpawnPos();
        SpawnCharacter();
    }

    public List<Vector3> GetSpawnPos()
    {
        List<Vector3> pos = new List<Vector3>();

        for(int i = 0; i < 100; i++)
        {
            Vector3 randomPoint = Vector3.zero + Random.insideUnitSphere * 50;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 10f, NavMesh.AllAreas))
            {
                pos.Add(hit.position);
                if (pos.Count == numberChar) break;
            }
        }
        return pos;
    }

    public void SpawnCharacter()
    {
        player = Instantiate(playerPrefab, spawnPosList[0], Quaternion.identity);
        Indicator playerIndicator = Instantiate(indicatorPrefab, mainCanvas.transform);
        player.SetIndicator(playerIndicator);
        player.OnInit();
        playerIndicator.OnInit(player);
        characterList.Add(player);

        for (int i = 1; i < spawnPosList.Count; i++)
        {
            Bot newBot = LeanPool.Spawn(botPrefab, spawnPosList[i], Quaternion.identity);
            Indicator botIndicator = Instantiate(indicatorPrefab, mainCanvas.transform);
            newBot.SetIndicator(botIndicator);
            newBot.OnInit();
            botIndicator.OnInit(newBot);
            characterList.Add(newBot);
        }
    }

    private void SetPlayersActive(bool active)
    {
        for(int i = 0; i < characterList.Count; i++)
        {
            characterList[i].enabled = active;
            characterList[i].Indicator.gameObject.SetActive(active);
        }
        DisplayAlive();
    }

    private void DisplayAlive()
    {
        aliveText.text = characterList.Count.ToString();
    }

    public void OnGameStateChange(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                SetPlayersActive(false);
                break;

            case GameState.GAME:
                SetPlayersActive(true);

                break;

            case GameState.WEAPONSECTION:

                break;

            case GameState.SETTING:
                SetPlayersActive(false);
                break;

            case GameState.GAMEOVER:
                SetPlayersActive(false);

                break;

            case GameState.SHOP:

                break;
        }
    }

    //public void Reborn()
    //{
    //    StartCoroutine(OnReborn());
    //}

    //IEnumerator OnReborn()
    //{
    //    yield return new WaitForSeconds(1f);

    //    Vector3 random = Vector3.zero;
    //    for (int i = 0; i < 20; i++)
    //    {
    //        Vector3 randomPoint = Vector3.zero + Random.insideUnitSphere * 50;
    //        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 10f, NavMesh.AllAreas))
    //        {
    //            random = hit.position;
    //            break;
    //        }
    //    }
    //    player.transform.position = random;
    //    player.OnInit();
    //}

    //public void Recycle(Character bot)
    //{
    //    StartCoroutine(OnRecycle(bot));
    //}

    //IEnumerator OnRecycle(Character bot)
    //{
    //    yield return new WaitForSeconds(1.5f);

    //    Vector3 random = Vector3.zero;
    //    for (int i = 0; i < 20; i++)
    //    {
    //        Vector3 randomPoint = Vector3.zero + Random.insideUnitSphere * 50;
    //        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 10f, NavMesh.AllAreas))
    //        {
    //            random = hit.position;
    //            break;
    //        }
    //    }
    //    LeanPool.Spawn(bot, random, Quaternion.identity);
    //    bot.OnInit();
    //}
}
