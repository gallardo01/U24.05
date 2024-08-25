using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static ItemJSONDatabase;

public class GameController : MonoBehaviour
{
    [SerializeField] public List<Transform> summonPoint;
    [SerializeField] public Player playerPrebs;
    [SerializeField] GameObject botPrefs;
    [SerializeField] TMP_Text numberAlive;
    [SerializeField] public List<GameObject> weaponList;

    private int botNumber = 10;

    public int goldNumber;
    public List<string> weaponTag;
    public static GameController instance;
    public List<GameObject> countBots;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        CreatPlayerAndBot();
        for (int i = 0; i < weaponList.Count; i++)
        {
            weaponTag.Add(weaponList[i].tag);
        }
    }

    private void Update()
    {
        if (countBots.Count > 0)
        {
            CountPlayer(countBots.Count + 1);
        }
        else
        {
            UIManager.instance.DisplayWinGameUI();
        }
    }
    public void EquipNewItem()
    {
        playerPrebs.initSkin.PlayerEquipItem();
    }
    public void DeleteOldItem()
    {
        playerPrebs.initSkin.DeleteOldItem();
    }
    public int InitPlayerGold()
    {
        if (!PlayerPrefs.HasKey("Gold"))
        {
            PlayerPrefs.SetInt("Gold", 200);
            return goldNumber = 200;
        }
        else
        {
            return goldNumber = PlayerPrefs.GetInt("Gold");
        }
    }
    public void ChangeGold(int num)
    {
        goldNumber += num;
        PlayerPrefs.SetInt("Gold", goldNumber);
        UIManager.instance.goldCoin.text = InitPlayerGold().ToString();
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
                    bot.initSkin.GetComponent<InitSkin>().BotEquipItem();
                    bot.UseWeapon();
                    countBots.Add(bot.gameObject);
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
                playerPrebs.body.position = summonPoint[randomIndex].position;
                playerPrebs.initSkin.self = playerPrebs;
                playerPrebs.SetNewPlayer();
                break;
            }
        }     
    }
    public void StartGame()
    {
        for (int i = 0; i < countBots.Count; i++)
        {
            countBots[i].GetComponent<Bot>().OnInit();
        }
        playerPrebs.OnInit();
        playerPrebs.radarIndicator.CreatAllMark();
    }
    public void PlayAgain()
    {
        for (int i = 0; i < countBots.Count; i++)
        {
            if (!countBots[i].GetComponent<Player>())
            {
                Destroy(countBots[i]);
            }
        }
        countBots.Clear();
        playerPrebs.gameObject.SetActive(true);
        playerPrebs.radarIndicator.targetList.Clear();
        playerPrebs.HPbar.GetComponent<TargetIndicator>().ChangeHealth(playerPrebs.BASE_HEALTH);
        CreatPlayerAndBot();
    }
    public void EndGame()
    {
        JoystickControl.instance.gameObject.SetActive(false);
        JoystickControl.direct = Vector3.zero;
    }

    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
    public void CountPlayer(int numberPlayer)
    {
        numberAlive.text = numberPlayer.ToString();
    }
}
