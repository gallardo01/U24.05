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
    public List<GameObject> countPlayers;

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
        if (countPlayers.Count > 1)
        {
            CountPlayer(countPlayers.Count);
        }
        else
        {
            if (countPlayers[0].GetComponent<Player>())
            {
                UIManager.instance.WinAGame();

            }
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
                    countPlayers.Add(bot.gameObject);
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
                countPlayers.Add(playerPrebs.gameObject);
                break;
            }
        }     
    }
    public void StartGame()
    {
        for (int i = 0; i < countPlayers.Count; i++)
        {
            if (countPlayers[i].GetComponent<Player>())
            {
                countPlayers[i].GetComponent<Player>().OnInit();
            }
            else
            {
                countPlayers[i].GetComponent<Bot>().OnInit();
            }
        }
    }
    public void PlayAgain()
    {
        for (int i = 0; i < countPlayers.Count; i++)
        {
            if (!countPlayers[i].GetComponent<Player>())
            {
                Destroy(countPlayers[i]);
            }
        }
        countPlayers.Clear();
        playerPrebs.gameObject.SetActive(true);
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
