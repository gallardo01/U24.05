using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Transform> listBricksTransform = new List<Transform>();
    [SerializeField] GameObject brickPrefab;
    
    private List<GameObject> listBricks = new List<GameObject>();
    public List<int> randomColorIndices = new List<int>();

    
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnInit()
    {
        
        Player player = FindObjectOfType<Player>();
        randomColorIndices.Add(player.colorIndex);
        
        for (int i = 0; i < 2; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, Enum.GetValues(typeof(ColorChange.Color)).Length);
            } while (randomColorIndices.Contains(randomIndex)); 

            randomColorIndices.Add(randomIndex);
        }
        for (int i = 0; i < listBricksTransform.Count; i++)
        {
            GameObject brick = Instantiate(brickPrefab, listBricksTransform[i].transform);
            brick.transform.localPosition = Vector3.zero;
            Renderer renderer = brick.GetComponent<Renderer>();
            if (renderer != null)
            {
                int randomColorIndex = randomColorIndices[Random.Range(0, randomColorIndices.Count)];

                Material randomColor = ColorChange.Instance.GetMaterialColor(randomColorIndex);
                renderer.material = randomColor;
            }            
            listBricks.Add(brick);
        }
    }
}
