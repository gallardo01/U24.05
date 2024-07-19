using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform playerTF;
    [SerializeField] Vector3 offset;

    private void Start()
    {
        FindPlayer();
    }

    private void LateUpdate()
    {
        if (playerTF != null)
        {
            transform.position = Vector3.Lerp(transform.position, playerTF.position + offset, Time.deltaTime * 5f);
        }
        else FindPlayer();
    }
    private void FindPlayer()
    {
        playerTF = PlayersManager.Instance.player.transform;
    }
}
