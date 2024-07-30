using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] public List<Transform> summonPoint;
    [SerializeField] GameObject playerPrebs;
    [SerializeField] GameObject botPrefs;
    [SerializeField] TMP_Text numberAlive;
    [SerializeField] List<GameObject> weaponList;
    //[SerializeField] GameObject startButton;
    //[SerializeField] public GameObject tryANewGame;

    private int botNumber = 10;
    public int countWeaponSummon = 0;
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
        StartCoroutine(SummonWeapon());
        for (int i = 0; i < weaponList.Count; i++)
        {
            weaponTag.Add(weaponList[i].tag);
        }
    }

    private void Update()
    {
        CountPlayer(countPlayers.Count);
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
                playerPrebs.GetComponent<Player>().body.position = summonPoint[randomIndex].position;
                countPlayers.Add(playerPrebs);
                break;
            }
        }
    }
    public void StartGame()
    {
        for (int i = 0; i < countPlayers.Count; i++)
        {
            countPlayers[i].GetComponent<Character>().isDead = false;
            if (countPlayers[i].GetComponent<Player>())
            {
                countPlayers[i].GetComponent<Character>().OnInit();
            }
            else
            {
                countPlayers[i].GetComponent<Bot>().OnInit();
            }
        }
    }
    public void TryANewGame()
    {
        for (int i = 0; i < countPlayers.Count; i++)
        {
            if (!countPlayers[i].GetComponent<Player>())
            {
                Destroy(countPlayers[i]);
            }
        }
        countPlayers.Clear();
        playerPrebs.SetActive(true);
        CreatPlayerAndBot();
        StartCoroutine(SummonWeapon());
    }
    public void EndGame()
    {
        JoystickControl.instance.gameObject.SetActive(false);
        JoystickControl.direct = Vector3.zero;
    }
    public GameObject UseWeapon(string weaponName)
    {
        switch (weaponName)
        {
            case "axe1":
                return playerPrebs.GetComponent<Character>().weaponPrefabs = weaponList[0];
            case "axe2":
                return playerPrebs.GetComponent<Character>().weaponPrefabs = weaponList[1];
            case "gun":
                return playerPrebs.GetComponent<Character>().weaponPrefabs = weaponList[2];
            case "boomerang":
                return playerPrebs.GetComponent<Character>().weaponPrefabs = weaponList[3];
            case "candy":
                return playerPrebs.GetComponent<Character>().weaponPrefabs = weaponList[4];
            default:
                return null;
        }
    }
    public IEnumerator SummonWeapon()
    {
        while (countWeaponSummon < 6)
        {
            yield return new WaitForSeconds(5f);
            Vector3 summonPos = RandomNavSphere(Vector3.zero, 50f, 1) + new Vector3(0,3,0);
            Weapon weaponSummon = Instantiate(weaponList[Random.Range(0, weaponList.Count)], summonPos, Quaternion.identity).GetComponent<Weapon>();
            weaponSummon.GetComponent<Weapon>().enabled = false;
            countWeaponSummon++;
        }
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
