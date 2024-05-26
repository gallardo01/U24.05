using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] List<Transform> spawnSpots;
    [SerializeField] PlayerBehavior playerPrefab;
    [SerializeField] Button buyButton;
    [SerializeField] int cost = 10;

    private void Awake()
    {
        buyButton.onClick.AddListener(SpawnPlayer);
    }

    public void SpawnPlayer()
    {
        if (spawnSpots.Count == 0) return;

        int random = Random.Range(0, spawnSpots.Count);
        PlayerBehavior player = Instantiate(playerPrefab, spawnSpots[random].position, Quaternion.identity);
        spawnSpots.RemoveAt(random);
        GameController.Instance.PayGold(cost);
    }

}
