using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    public Bot botPrefab;
    public Player playerPrefab;
    public int numberOfBots = 10;
    public float spawnAreaSize = 50f;
    public TargetIndicator indicator;

    void Start()
    {
        SpawnBots();
    }

    void SpawnBots()
    {
        for (int i = 0; i < numberOfBots; i++)
        {
            Vector3 randomPosition = GetRandomNavMeshPosition();
            Instantiate(botPrefab, randomPosition, Quaternion.identity);
        }
    }
    
    
    Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnAreaSize;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, spawnAreaSize, 1);
        return hit.position;
    }
}
