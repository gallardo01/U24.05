using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] List<Transform> listNodeStart = new List<Transform>();
    [SerializeField] List<Transform> listNodeMove = new List<Transform>();

    [SerializeField] List<Bot> listBot = new List<Bot>(); 

    WaitForSeconds countDownTime = new WaitForSeconds(0.5f);

    int botAtSameTime = 6;
    int botTotal = 49;
    public int alive;
    public int reviveCount;

    private void Awake()
    {
        this.RegisterListener(EventID.OnCharacterDead, (param) =>
        {
            Character character = (Character)param;
            if (character is Bot bot)
            {
                if (alive - botAtSameTime > 1)
                {
                    GenerateBot();
                }

                alive--;
                UIManager.Ins.GetUI<UIGameplay>().UpdateTextBotAlive(alive);            
                listBot.Remove(bot);
            }
            else if (character is Player player)
            {
                player.rank = alive;
            }

            if (alive == 1)
            {
                LevelManager.Ins.Finish();
            }
        });
    }

    public void InitLevel()
    {
        reviveCount = 1;

        alive = botTotal + 1;

        for (int i = 0; i < botAtSameTime; i++)
        {
            GenerateBot();
        }
    }

    public Transform GetRandomNodeMove()
    {
        int index = Random.Range(0, listNodeMove.Count);
        return listNodeMove[index];
    }

    public Transform GetRandomNodeStart()
    {
        int index = Random.Range(0, listNodeStart.Count);
        Transform node = listNodeStart[index];
        listNodeStart.RemoveAt(index);
        StartCoroutine(CountdownNodeReady(listNodeStart, node));
        return node;
    }

    IEnumerator CountdownNodeReady(List<Transform> listNode, Transform node)
    {
        yield return countDownTime;
        listNode.Add(node);
    }

    private void GenerateBot()
    {
        Transform NodeStart = GetRandomNodeStart();
        Bot bot = (Bot)SimplePool.Spawn(PoolType.Bot, NodeStart.position, Quaternion.identity);
        int playerLevel = LevelManager.Ins.player.Level;
        int botLevel = playerLevel + Random.Range(0, 2);
        bot.InitCharacter((WeaponType)Random.Range(0, DataManager.Ins.GetWeaponDataList().Count), botLevel);

        listBot.Add(bot);
    }

    public void ResetLevel()
    {
        for (int i = 0; i < listBot.Count; i++)
        {
            listBot[i].StopMove();
            listBot[i].ResetCharacter();
            listBot[i].RemoveAllTarget();
            SimplePool.Despawn(listBot[i]);
        }
        listBot.Clear();

        InitLevel();
    }
}
