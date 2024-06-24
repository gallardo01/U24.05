using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeState : MonoBehaviour
{
    public GameObject brickPrefab;
    public int rows, columns;
    public Vector3 offset = new Vector3(1, 0, 1);
    private GameObject currentBrick;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Create a new brick instance
                GameObject brick = Instantiate(brickPrefab, transform);

                // Set the position of the brick based on the row and column indices
                brick.transform.position = new Vector3(j * offset.x, -0.9f, i * offset.z);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = JoystickControl.direct;
        if (direction != Vector3.zero)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Brick"))
                {
                    Debug.Log("Player is standing on a brick.");
                }
            }
        }

        void OnBrickPickedUp(GameObject brick)
        {
            // Destroy the brick
            Destroy(brick);

            // Start the coroutine to spawn a new brick after 5 seconds
            StartCoroutine(SpawnBrickAfterDelay(5));
        }

         IEnumerator SpawnBrickAfterDelay(float delay)
        {
            // Wait for the specified delay
            yield return new WaitForSeconds(delay);

            // Generate a random position for the new brick
            int randomRow = Random.Range(0, rows);
            int randomColumn = Random.Range(0, columns);
            Vector3 randomPosition = new Vector3(randomColumn * offset.x, -0.9f, randomRow * offset.z);

            // Create a new brick instance at the random position
            GameObject newBrick = Instantiate(brickPrefab, transform);
            newBrick.transform.position = randomPosition;

        }
    }
}
