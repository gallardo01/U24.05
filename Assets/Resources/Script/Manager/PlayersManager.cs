using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class PlayersManager : Singleton<PlayersManager>
{
    [SerializeField] GameObject botPrefab;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Indicator indicatorPrefab;
    [SerializeField] Canvas mainCanvas;
    [SerializeField] int numberChar;
    [HideInInspector] public Player player;
    
    private List<Vector3> spawnPosList = new List<Vector3>();
    private List<Character> characterList = new List<Character>();

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
        player = Instantiate(playerPrefab, spawnPosList[0], Quaternion.identity).GetComponent<Player>();
        Indicator playerIndicator = Instantiate(indicatorPrefab, mainCanvas.transform);
        player.SetIndicator(playerIndicator);
        player.OnInit();
        playerIndicator.OnInit(player);
        characterList.Add(player);

        for (int i = 1; i < spawnPosList.Count; i++)
        {
            Bot newBot = LeanPool.Spawn(botPrefab, spawnPosList[i], Quaternion.identity).GetComponent<Bot>();
            Indicator botIndicator = Instantiate(indicatorPrefab, mainCanvas.transform);
            newBot.SetIndicator(botIndicator);
            newBot.OnInit();
            botIndicator.OnInit(newBot);
            characterList.Add(newBot);
        }
    }

    public void Reborn()
    {
        StartCoroutine(OnReborn());
    }

    IEnumerator OnReborn()
    {
        yield return new WaitForSeconds(1f);

        Vector3 random = Vector3.zero;
        for (int i = 0; i < 20; i++)
        {
            Vector3 randomPoint = Vector3.zero + Random.insideUnitSphere * 50;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 10f, NavMesh.AllAreas))
            {
                random = hit.position;
                break;
            }
        }
        player.transform.position = random;
        player.OnInit();
    }

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
