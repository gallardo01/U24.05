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
    private List<int> characterID = new List<int>();
    public Dictionary <int,GameObject> characterList = new Dictionary<int, GameObject>();
    public TMP_Text coinText;
    private int coin;
    // Start is called before the first frame update
    void Awake()
    {
        InvokeRepeating(nameof(CreateNewObject), 1f, 1f);
        coin = PlayerPrefs.GetInt("coin", 1000);
        DisplayCoin();
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
        
        if (!characterID.Contains(slot))
        {
            characterID.Add(slot);
            GameObject newCharacter = Instantiate(characterPrefabs, respawnPlace[slot].position, transform.rotation);
            characterList.Add(slot, newCharacter);
            coin -= 10;
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
}
