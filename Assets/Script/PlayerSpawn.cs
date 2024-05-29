using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] List<Transform> spawnSpots;
    [SerializeField] Button buyButton;
    [SerializeField] int cost = 10;

    [SerializeField] List<Transform> playerPrefabs;
    private void Awake()
    {
        buyButton.onClick.AddListener(SpawnPlayer);
    }

    public void SpawnPlayer()
    {
        if (spawnSpots.Count == 0) return;
        Transform playerPrefab = playerPrefabs[Random.Range(0, playerPrefabs.Count)];
        int random = Random.Range(0, spawnSpots.Count);

        Transform player = Instantiate(playerPrefab, spawnSpots[random].position, Quaternion.identity);
        spawnSpots.RemoveAt(random);
        GameController.Instance.PayGold(cost);

    }

}
