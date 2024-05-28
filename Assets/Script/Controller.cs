using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject monsterPrefabs;
    public GameObject characterPrefabs;

    public List<Transform> path_1 = new List<Transform>();
    public List<Transform> path_2 = new List<Transform>();
    public List<Transform> respawnPlace;
    private List<int> characterSlot = new List<int>();
    public Dictionary <int,GameObject> characterDict = new Dictionary<int, GameObject>();
    public TMP_Text coinText;
    private int coin;
    // Start is called before the first frame update
    void Awake()
    {   // Invoke(nameof(CreateNewObject), 1f); => goi 1 lan duy nhat
        // InvokeRepeating(nameof(CreateNewObject), 1f, 1f); => goi nhieu lan
        coin = PlayerPrefs.GetInt("coin", 1000);
        DisplayCoin();
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
    private void CreateNewObject()
    {
        GameObject newObject = Instantiate(monsterPrefabs);
        if (Random.Range(0, 2) == 0)
        {
            newObject.GetComponent<Enemy>().SetPath(path_1);
        }
        else
        {
            newObject.GetComponent<Enemy>().SetPath(path_2);
        }
    }

    public void CreatCharacter()
    {
        int slot = Random.Range(0, 16);
        
        if (!characterSlot.Contains(slot) && coin > 50)
        {
            characterSlot.Add(slot);
            GameObject newCharacter = Instantiate(characterPrefabs, respawnPlace[slot].position, transform.rotation);
            //characterDict.Add(slot, newCharacter);
            coin -= 50;
            DisplayCoin();
        }
        else
        {
            CreatCharacter();
        }
    }

    private void DisplayCoin()
    {
        coinText.text = coin.ToString();
    }

    public void EarnCoin()
    {
        coin += 50;
        DisplayCoin();
    }
}
