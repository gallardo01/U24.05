using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBrick : MonoBehaviour
{
    Player playerMovement;
    [SerializeField] Transform body;
    [SerializeField] Transform pointBrick;
    [SerializeField] LayerMask brickLayerMask;

    Stack<Transform> brickStack = new Stack<Transform>();

    private void Awake()
    {
        playerMovement = GetComponent<Player>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AddBrick), 0f, 1f);
    }

    public void AddBrick()
    {
        Collider[] avalableBricks = Physics.OverlapSphere(this.transform.position, 5f, brickLayerMask);
        for (int i = 0; i < avalableBricks.Length; i++)
        {
            avalableBricks[i].TryGetComponent<Brick>(out Brick newPlayerBrick);
            if (newPlayerBrick.BrickColor == playerMovement.ColorIndex)
            {
                SpawnBrick.Instance.SpawnNewBrick(newPlayerBrick);
                avalableBricks[i].enabled = false;

                newPlayerBrick.transform.SetParent(body);
                newPlayerBrick.transform.localPosition = pointBrick.localPosition;
                newPlayerBrick.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                pointBrick.localPosition += new Vector3(0f, 0.2f, 0f);

                brickStack.Push(newPlayerBrick.transform);
            }
        }
    }
}
