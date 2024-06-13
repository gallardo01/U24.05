using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickStacking : MonoBehaviour
{
    [SerializeField] Transform brickPrefab;
    [SerializeField] Transform playerModel;

    private float brickWight = 0.3f;
    private Player player;
    private Stack<Transform> brickStack = new Stack<Transform>();

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void AddBrick()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit ,5f, 1 << 8))
        {
            hit.collider.gameObject.SetActive(false);
            Transform newBrick = Instantiate(brickPrefab);
            brickStack.Push(newBrick);
            newBrick.SetParent(this.transform);
            newBrick.transform.localPosition = playerModel.transform.localPosition;
            playerModel.transform.localPosition += new Vector3(0, brickWight, 0);
        }
    }

    public void RemoveBrick()
    {

    }
}
