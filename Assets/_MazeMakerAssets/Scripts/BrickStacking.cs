using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickStacking : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] AudioClip brickSound;

    private float brickWight = 0.3f;
    private Player player;
    private Stack<Transform> brickStack = new Stack<Transform>();

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void StackChecking()
    {
        AddBrick();
        RemoveBrick();
    }

    private void AddBrick()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit ,5f, 1 << 8))
        {
            hit.collider.enabled = false;

            Transform newBrick = hit.collider.transform;
            brickStack.Push(newBrick);
            newBrick.SetParent(this.transform);
            newBrick.transform.localPosition = body.transform.localPosition;
            body.transform.localPosition += new Vector3(0, brickWight, 0);
            GameController.Instance.ChangeScore();
            AudioSource.PlayClipAtPoint(brickSound, Camera.main.transform.position);
        }
    }

    private void RemoveBrick()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 5f, 1 << 9))
        {
            hit.collider.enabled=false;

            if(brickStack.Count > 0)
            {
                Transform removedBrick = brickStack.Pop();
                removedBrick.SetParent(null);
                removedBrick.transform.position = hit.point;
                body.transform.localPosition -= new Vector3(0, brickWight, 0);
            }
            else
            {
                Debug.Log("Game Over");
            }
        }
    }

    public void ClearBrick()
    {
        Transform[] brickArray = brickStack.ToArray();
        for(int i = 0; i < brickArray.Length; i++)
        {
            brickArray[i].gameObject.SetActive(false);
        }
        body.transform.localPosition = new Vector3(0f, -0.3f, 0f);
    }
}
