using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] List<Transform> listNodeStart = new List<Transform>();
    [SerializeField] List<Transform> listNodeMove = new List<Transform>();

    WaitForSeconds countDownTime = new WaitForSeconds(0.5f);

    int botAtSameTime = 8;
    public int botTotal = 100;

    private void Awake()
    {
        this.RegisterListener(EventID.OnCharacterDead, (param) =>
        {
            if ((Character)param is Bot && botTotal > 0)
            {
                GenerateBot();
            }
        });
    }

    public void InitLevel()
    {
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

    protected void GenerateBot()
    {
        botTotal--;
        Transform NodeStart = GetRandomNodeStart();
        Bot bot = (Bot)SimplePool.Spawn(PoolType.Bot, NodeStart.position, Quaternion.identity);
        bot.InitCharacter(NodeStart, (WeaponType)Random.Range(0, DataManager.Ins.GetWeaponDataList().Count), 0);
    }
}
