using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageController : Singleton<StageController>
{
    // Start is called before the first frame update
    [SerializeField] private List<Transform> listBricksTransform = new List<Transform>();
    [SerializeField] GameObject brickPrefab;
    
    private List<GameObject> listBricks = new List<GameObject>();
    
    // Create a new list to store the transforms that do not contain a brick
    private List<Transform> availableTransforms = new List<Transform>();
    public List<int> randomColorIndices = new List<int>();

    
    void Start()
    {
        OnInit();
        
        // Initialize the availableTransforms list
        UpdateAvailableTransforms();
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
    
    public void RespawnBrick(GameObject brick, float delay)
    {
        StartCoroutine(RespawnBrickCoroutine(brick, delay));
    }

    
    private IEnumerator RespawnBrickCoroutine(GameObject brick, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (availableTransforms.Count == 0)
        {
            yield break;
        }

        Transform randomTransform = availableTransforms[Random.Range(0, availableTransforms.Count)];

        GameObject newBrick = Instantiate(brickPrefab, randomTransform.position, Quaternion.identity);
        Renderer renderer = newBrick.GetComponent<Renderer>();
        if (renderer != null)
        {
            int randomColorIndex = randomColorIndices[Random.Range(0, randomColorIndices.Count)];
            Material randomColor = ColorChange.Instance.GetMaterialColor(randomColorIndex);
            renderer.material = randomColor;
        }
        // Update the availableTransforms list
        UpdateAvailableTransforms();
        
        listBricks.Add(newBrick);

    }
    
    
    public void UpdateAvailableTransforms()
    {
        availableTransforms.Clear();

        foreach (Transform transform in listBricksTransform)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
            bool isBrickPresent = false;
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Brick"))
                {
                    isBrickPresent = true;
                    break;
                }
            }

            if (!isBrickPresent)
            {
                availableTransforms.Add(transform);
            }
        }
    }

}
