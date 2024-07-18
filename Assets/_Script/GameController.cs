using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public List<Transform> summonPoint;
    [SerializeField] GameObject playerPrebs;
    [SerializeField] GameObject botPrefs;
    private int botNumber = 4;
    [SerializeField] List<GameObject> weaponList;

    public enum WeaponName
    {
        axe1,
        axe2,
        gun,
        boomerang,
        candy
    }

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

    public GameObject UseWeapon (WeaponName weaponNum)
    {
        switch (weaponNum)
        {
            case WeaponName.axe1:
                return playerPrebs.GetComponent<Character>().weaponPrefabs = weaponList[0];
            case WeaponName.axe2:
                return playerPrebs.GetComponent<Character>().weaponPrefabs = weaponList[1];
            case WeaponName.gun:
                return playerPrebs.GetComponent<Character>().weaponPrefabs = weaponList[2];
            case WeaponName.boomerang:
                return playerPrebs.GetComponent<Character>().weaponPrefabs = weaponList[3];
            case WeaponName.candy:
                return playerPrebs.GetComponent<Character>().weaponPrefabs = weaponList[4];
            default:
                return null;
        }
    }
}
