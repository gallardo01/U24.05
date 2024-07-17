using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public List<Transform> summonPoint;
    [SerializeField] GameObject playerPrebs;
    [SerializeField] GameObject botPrefs;
    private int botNumber = 4;

    public static GameController instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        CreatPlayerAndBot();   
    }
    private void CreatPlayerAndBot()
    {
        List<int> randomPos = new List<int>();
        for (int i = 0; i < botNumber; i++)
        {
            while (true)
            {
                int randomIndex = Random.Range(0, summonPoint.Count);
                if (!randomPos.Contains(randomIndex))
                {
                    randomPos.Add(randomIndex);
                    Bot bot = Instantiate(botPrefs, summonPoint[randomIndex].position, Quaternion.identity).GetComponent<Bot>();
                    break;
                }
            }
        }
        while (true)
        {
            int randomIndex = Random.Range(0, summonPoint.Count);
            if (!randomPos.Contains(randomIndex))
            {
                randomPos.Add(randomIndex);
                playerPrebs.GetComponent<Player>().body.position = summonPoint[randomIndex].position;
                break;
            }
        }
    }
}
