using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject monsterPrefabs;
    public GameObject characterPrefabs;
    public GameObject buffCharacterPrefabs;

    public List<Transform> path_1 = new List<Transform>();
    public List<Transform> path_2 = new List<Transform>();
    public List<Transform> respawnPlace;
    private List<int> characterSlot = new List<int>();
    public Dictionary <int,GameObject> characterDict = new Dictionary<int, GameObject>();

    // Start is called before the first frame update
    void Start()
    {   
        UImanager.instance.DisplayCoin();
        UImanager.instance.DisplayHero1Price();
        UImanager.instance.DisplayHero2Price();
        // => goi ham coroutine
        StartCoroutine(CreatNewbObjectCoroutin());
        
    }

    // Gọi repeat bằng Coroutine
    IEnumerator CreatNewbObjectCoroutin()
    {
        yield return new WaitForSeconds(3f); // => sau khi het 3s se bat dau chay
        GameObject newObject = Instantiate(monsterPrefabs);
        if (Random.Range(0, 2) == 0)
        {
            newObject.GetComponent<Enemy>().SetPath(path_1);
        }
        else
        {
            newObject.GetComponent<Enemy>().SetPath(path_2);
        }
        StartCoroutine(CreatNewbObjectCoroutin());
    }
    public void CreatCharacter1()
    {
        int slot = CheckAvailableSlot();
        int coin = UImanager.instance.DisplayCoin();
        if (coin >= 50)
        {
            characterSlot.Add(slot);
            GameObject newCharacter = Instantiate(characterPrefabs, respawnPlace[slot].position, transform.rotation);
            characterDict.Add(slot, newCharacter);
            UImanager.instance.BuyCharacter1();
        }
    }

    public void CreatCharacter2()
    {
        int slot = CheckAvailableSlot();
        int coin = UImanager.instance.DisplayCoin();
        if (coin >= 30)
        {
            characterSlot.Add(slot);
            GameObject newCharacter = Instantiate(buffCharacterPrefabs, respawnPlace[slot].position, transform.rotation);
            characterDict.Add(slot, newCharacter);
            UImanager.instance.BuyCharacter2();
        }
    }
    public int CheckAvailableSlot()
    {
        while (true)
        {
            int slot = Random.Range(0, 16);
            if (!characterSlot.Contains(slot))
            {
                return slot;
            }
        }
    }

    //Hàm Start và GameOver
    //private void GameStart()
    //{
    //    // => chạy hàm Start của game
    //}
    //private void GameOver() 
    //{
    //    // Dừng coroutine
    //    StopAllCoroutines();
    //    GameObject[] listMonster = GameObject.FindGameObjectsWithTag("monster");
    //    for (int i = 0; i < listMonster.Length; i++)
    //    {
    //        Destroy(listMonster[i]);
    //    }
    //    GameObject[] listBullet = GameObject.FindGameObjectsWithTag("bullet");
    //    for (int i = 0; i < listBullet.Length; i++)
    //    {
    //        Destroy(listBullet[i]);
    //    }
    //}
}
